﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetLocalRotation_Quaternion : BlueprintNodeSet
{
	//Executes the blueprint node set set local rotation instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Set the blueprints parents GameObjects local rotation to the attribute Quaternion rotation
			BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.localRotation = (Quaternion)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
	}

	//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set local rotation instance
	public override void Initialize()
	{
		//Initialize the blueprint node set set local rotation
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
	}

	//Returns the blueprint node set set local rotation title
	protected override string GetTitle()
	{
		//Return the blueprint node set set local rotation title
		return "SetLocalRotation";
	}

	//Renders the blueprint node set set local rotation body components
	protected override void RenderBodyComponents()
	{
		//Render the Quaternion rotation label
		BeginSection(1);
			GUILayout.Label("Quaternion : rotation", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}