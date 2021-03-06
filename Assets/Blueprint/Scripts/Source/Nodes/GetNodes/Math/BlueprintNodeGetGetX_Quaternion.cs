﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetX_Quaternion : BlueprintNodeGet
{
	//Returns the blueprint node get get x output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Return output attribute
			return ((Quaternion)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute()).x;
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node get get x default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node get get x default output attribute
		return 0.0f;
	}

	//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get x instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get x
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get x title
	protected override string GetTitle()
	{
		//Return the blueprint node get get x title
		return "GetX";
	}

	//Return the blueprint node get get x body components
	protected override void RenderBodyComponents()
	{
		//Render the Quaternion quaternion label
		BeginSection(1);
			GUILayout.Label("Quaternion : quaternion", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}