﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetScale_Transform : BlueprintNodeGet
{
	//Returns the blueprint node get get scale output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Return output attribute
			return ((Transform)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute()).lossyScale;
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node get get scale default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node get get scale default output attribute
		return new Vector3(0.0f, 0.0f, 0.0f);
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get scale instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get scale
		connections.Add(new BlueprintConnectionAttributeInputTransform());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeOutputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get scale title
	protected override string GetTitle()
	{
		//Return the blueprint node get get scale title
		return "GetScale";
	}

	//Return the blueprint node get get scale body components
	protected override void RenderBodyComponents()
	{
		//Render the Transform transform label
		BeginSection(1);
		GUILayout.Label("Transform : transform", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}