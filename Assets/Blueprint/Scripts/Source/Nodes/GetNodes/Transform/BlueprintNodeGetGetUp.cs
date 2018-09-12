using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetUp : BlueprintNodeGet
{
	//Returns the blueprint node get get up output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.up;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get up instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get up
		connections.Add(new BlueprintConnectionAttributeOutputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get up title
	protected override string GetTitle()
	{
		//Return the blueprint node get get up title
		return "GetUp";
	}

	//Return the blueprint node get get up body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector3 forward label
		BeginSection(1);
			GUILayout.Label("Vector3 : up", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}