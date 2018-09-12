using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeBlueprint : BlueprintNode
{
	//Public attributes
	public string blueprintFilepath = "";
	public string blueprintJSON = "";

	//Private attributes
	private int customBlueprintID = -1;

	//Initializes the blueprint node blueprint for runtime
	public override void RuntimeInitialize()
	{
		//Runtime initialize the blueprint node blueprint
		customBlueprintID = BlueprintInstanceManager.CreateBlueprint(blueprintFilepath, blueprintJSON, BlueprintInstanceManager.GetBlueprintParentAt(blueprintID));
	}

	//Start initializes the blueprint node blueprint
	public override void StartInitialize()
	{
		//For each custom blueprint node
		for (int nodeIndex = 0; nodeIndex < BlueprintInstanceManager.GetBlueprintAt(customBlueprintID).GetBlueprintNodeCount(); nodeIndex++)
		{
			//Start initialize the blueprint node
			BlueprintInstanceManager.GetBlueprintAt(customBlueprintID).GetBlueprintNodeAt(nodeIndex).StartInitialize();
		}
	}

//Executes the blueprint node blueprint instance
public override void Execute()
	{
		//If the custom blueprint is valid
		if(customBlueprintID > -1)
		{
			//Create intermediary attributes
			int attributeIndex = 0;

			//For each custom blueprint node
			for(int nodeIndex = 0; nodeIndex < BlueprintInstanceManager.GetBlueprintAt(customBlueprintID).GetBlueprintNodeCount(); nodeIndex++)
			{
				//If the custom blueprint node is a variable
				if (BlueprintInstanceManager.GetBlueprintAt(customBlueprintID).GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariable != null)
				{
					//If the connection node ID is valid for the variable
					if (connections.Count > attributeIndex && connections[attributeIndex].connectionNodeID > -1)
					{
						//Set the custom blueprint nodes output attribute to the linked input attribute
						BlueprintInstanceManager.GetBlueprintAt(customBlueprintID).GetBlueprintNodeAt(nodeIndex).SetAttribute(BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[attributeIndex].connectionNodeID).GetAttribute());	
					}

					//Increment the attribute index
					attributeIndex++;
				}
			}

			//For each custom blueprint node
			for (int nodeIndex = 0; nodeIndex < BlueprintInstanceManager.GetBlueprintAt(customBlueprintID).GetBlueprintNodeCount(); nodeIndex++)
			{
				//If the custom blueprint node is an event start node
				if(BlueprintInstanceManager.GetBlueprintAt(customBlueprintID).GetBlueprintNodeAt(nodeIndex) as BlueprintNodeEventStart != null)
				{
					//Execute the custom blueprint event start node
					BlueprintInstanceManager.GetBlueprintAt(customBlueprintID).GetBlueprintNodeAt(nodeIndex).Execute();
				}
			}
		}

		//If the output execution connection connection node is valid
		if(outputExecutionConnection.connectionNodeID > -1)
		{
			//Execute the output execution node
			BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(outputExecutionConnection.connectionNodeID).Execute();
		}
	}

	//Returns the blueprint node blueprints custom blueprint ID
	public int GetCustomBlueprintID()
	{
		//Return the blueprint node blueprints custom blueprint ID
		return customBlueprintID;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private Blueprint editorBlueprint = null;

	//Initialize the blueprint node blueprint
	public override void Initialize()
	{
		//Initialize the blueprint node blueprint input and output execution connections
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);

		//If the blueprint JSON is valid
		if (blueprintJSON != "")
		{
			//Initialize the blueprint node blueprint from the blueprint filepath
			editorBlueprint = new Blueprint(blueprintFilepath);
			editorBlueprint.Load();
		}
		//Otherwise
		else
		{
			//Initialize the blueprint node blueprint from the blueprint filepath
			editorBlueprint = new Blueprint(blueprintFilepath);
			editorBlueprint.Load();
		}

		//For each node in the blueprint
		for (int nodeIndex = 0; nodeIndex < editorBlueprint.GetBlueprintNodeCount(); nodeIndex++)
		{
			//If the blueprint node is a variable
			if (editorBlueprint.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariable != null)
			{
				//Add input connection
				connections.Add(((BlueprintNodeVariable)editorBlueprint.GetBlueprintNodeAt(nodeIndex)).GenerateInputConnection());
				connections[connections.Count - 1].Initialize(blueprintID, nodeID, connections.Count, true);
			}
		}

		//Update the blueprint JSON string
		blueprintJSON = editorBlueprint.ConvertToJSON();
	}

	//Returns the blueprint node blueprint header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node blueprint header color
		return new Color(1.0f, 0.5f, 0.0f);
	}

	//Returns the blueprint node blueprint title
	protected override string GetTitle()
	{
		//Return the blueprint node blueprint title
		return BlueprintPathHelper.GetNameFromPath(blueprintFilepath);
	}

	//Renders the blueprint node blueprint body components
	protected override void RenderBodyComponents()
	{
		//If the editor blueprint node is not valid
		if (editorBlueprint == null)
		{
			//Initialize temporary blueprint
			Blueprint tempBlueprint = new Blueprint();
			tempBlueprint.Load(blueprintJSON);

			//Initialize the editor blueprint
			editorBlueprint = new Blueprint(blueprintFilepath);
			editorBlueprint.Load();

			//Attribute storing the number of variables detected
			int variablesDetected = 0;

			//For each blueprint node in the editor blueprint
			for (int nodeIndex = 0; nodeIndex < editorBlueprint.GetBlueprintNodeCount(); nodeIndex++)
			{
				//If the blueprint node is a variable
				if (editorBlueprint.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariable != null)
				{
					//If enough connections already exist
					if (connections.Count > variablesDetected)
					{
						//If the connection types are not compatible
						if (connections[variablesDetected].GetType() != ((BlueprintNodeVariable)editorBlueprint.GetBlueprintNodeAt(nodeIndex)).GenerateInputConnection().GetType())
						{
							//Destroy the connection
							connections[variablesDetected].DestroyConnection();

							//Replace the connection with a compatible connection
							connections[variablesDetected] = ((BlueprintNodeVariable)editorBlueprint.GetBlueprintNodeAt(nodeIndex)).GenerateInputConnection();
							connections[variablesDetected].Initialize(blueprintID, nodeID, variablesDetected + 1, true);
						}
					}
					//Otherwise
					else
					{
						//Add a connection
						connections.Add(((BlueprintNodeVariable)editorBlueprint.GetBlueprintNodeAt(nodeIndex)).GenerateInputConnection());
						connections[connections.Count - 1].Initialize(blueprintID, nodeID, connections.Count, true);
					}

					//Increment the variables detected attribute
					variablesDetected += 1;
				}
			}

			//While there are more connections then needed
			while (connections.Count > variablesDetected)
			{
				//Destroy the last connection
				connections[connections.Count - 1].DestroyConnection();

				//Remove the last connection
				connections.RemoveAt(connections.Count - 1);
			}

			//Restore variables from the temp blueprint in the editor blueprint
			BlueprintVariableHelper.RestoreVariables(tempBlueprint, ref editorBlueprint);

			//Update the blueprint JSON string
			blueprintJSON = editorBlueprint.ConvertToJSON();
		}

		//Attribute for keeping track of the section in the blueprint node to render the variable type name information
		int variableSection = 1;

		//For each node in the editor blueprint
		for (int nodeIndex = 0; nodeIndex < editorBlueprint.GetBlueprintNodeCount(); nodeIndex++)
		{
			//If the editor blueprint node is a variable
			if (editorBlueprint.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariable != null)
			{
				//Render the blueprint node variables type name information
				BeginSection(variableSection);
				((BlueprintNodeVariable)editorBlueprint.GetBlueprintNodeAt(nodeIndex)).RenderTypeNameInformation();
				EndSection();

				//Increment the variable section
				variableSection += 1;
			}
		}
	}
#endif
}