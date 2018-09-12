using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSet : BlueprintNode
{
	//Executes the blueprint node set instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Set the output attribute to the input attribute
			BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).SetAttribute(BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute());
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
	}

	//Executes the blueprint node set instances output execution node
	protected void ExecuteOutputExecutionNode()
	{
		//If the output execution connection connection node is valid
		if (outputExecutionConnection.connectionNodeID > -1)
		{
			//Execute the output execution node
			BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(outputExecutionConnection.connectionNodeID).Execute();
		}
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set instance
	public override void Initialize()
	{
		//Initialize the blueprint node set
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);
	}

	//Adds the input connections for the blueprint node set instance using the specified type of blueprint connection
	protected void AddInputConnections<T>() where T : BlueprintConnection, new()
	{
		//Add the blueprint node set input connections
		connections.Add(new T());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, connections.Count, true);
		connections.Add(new T());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, connections.Count, true);
	}

	//Returns the blueprint node set header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node set header color
		return new Color(0.75f, 0.0f, 0.75f);
	}

	//Returns the blueprint node set type name
	protected virtual string GetTypeName()
	{
		//Return default blueprint node set type name
		return "Any";
	}

	//Renders the blueprint node set body components
	protected override void RenderBodyComponents()
	{
		//Render the input label
		BeginSection(1);
			GUILayout.Label(GetTypeName() + " : input", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the output label
		BeginSection(2);
			GUILayout.Label(GetTypeName() + " : output", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}