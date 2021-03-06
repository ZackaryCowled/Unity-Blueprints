﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetForward : BlueprintNodeGet
{
	//Returns the blueprint node get get forward output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.forward;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get forward instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get forward
		connections.Add(new BlueprintConnectionAttributeOutputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get forward title
	protected override string GetTitle()
	{
		//Return the blueprint node get get forward title
		return "GetForward";
	}

	//Renders the blueprint node get get forward body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector3 forward label
		BeginSection(1);
			GUILayout.Label("Vector3 : forward", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}