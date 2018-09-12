using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BlueprintNodeSetSetText : BlueprintNodeSet
{
	//Executes the blueprint node set set text instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Create reference to the text input attribute
			Text textInputAttribute = (Text)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();

			//If the text input attribute is valid
			if (textInputAttribute != null)
			{
				//Set the text input attributes text value to the string input attribute
				textInputAttribute.text = (string)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute();
			}
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set text instance
	public override void Initialize()
	{
		//Initialize the blueprint node set set text instance
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);
		connections.Add(new BlueprintConnectionAttributeInputText());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
	}

	//Returns the blueprint node set set text title
	protected override string GetTitle()
	{
		//Return the blueprint node set set text title
		return "SetText";
	}

	//Renders the blueprint node set set text body components
	protected override void RenderBodyComponents()
	{
		//Render the text label
		BeginSection(1);
			GUILayout.Label("Text : component", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the string label
		BeginSection(2);
			GUILayout.Label("String : text", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}