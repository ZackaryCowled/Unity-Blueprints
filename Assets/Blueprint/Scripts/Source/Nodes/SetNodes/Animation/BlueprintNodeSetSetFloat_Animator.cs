﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetFloat_Animator : BlueprintNodeSet
{
	//Executes the blueprint node set set float instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Create reference to the animator input attribute
			Animator animatorInputAttribute = (Animator)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();

			//If the animator input attribute is valid
			if (animatorInputAttribute != null)
			{
				//Set the animator input attribute
				animatorInputAttribute.SetFloat((string)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute(),
											    (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute());
			}
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set float instance
	public override void Initialize()
	{
		//Initialize the blueprint node set set float
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);
		connections.Add(new BlueprintConnectionAttributeInputAnimator());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 3, true);
	}

	//Returns the blueprint node set set float title
	protected override string GetTitle()
	{
		//Return the blueprint node set set float title
		return "SetFloat";
	}

	//Renders the blueprint node set set float title
	protected override void RenderBodyComponents()
	{
		//Render the animator label
		BeginSection(1);
			GUILayout.Label("Animator : animator", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the string label
		BeginSection(2);
			GUILayout.Label("String : name", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the float label
		BeginSection(3);
			GUILayout.Label("Float : value", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}