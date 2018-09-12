using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetQuaternion_Vector3 : BlueprintNodeSet
{
	//Executes the blueprint node set set text instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Set the Quaternion input attribute to the Vector3 input attribute as a Quaternion
			BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).SetAttribute(Quaternion.Euler((Vector3)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute()));
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set Quaternion instance
	public override void Initialize()
	{
		//Initialize the blueprint node set set Quaternion instance
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
	}

	//Returns the blueprint node set set Quaternion title
	protected override string GetTitle()
	{
		//Return the blueprint node set set Quaternion title
		return "SetQuaternion";
	}

	//Renders the blueprint node set set text body components
	protected override void RenderBodyComponents()
	{
		//Render the Quaternion label
		BeginSection(1);
			GUILayout.Label("Quaternion : quaternion", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the Vector3 label
		BeginSection(2);
			GUILayout.Label("Vector3 : rotation", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}