using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetLocalRotation : BlueprintNodeGet
{
	//Returns the blueprint node get get local rotation output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.localRotation.eulerAngles;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get local rotation instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get local rotation
		connections.Add(new BlueprintConnectionAttributeOutputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get local rotation title
	protected override string GetTitle()
	{
		//Return the blueprint node get get local rotation title
		return "GetLocalRotation";
	}

	//Return the blueprint node get get local rotation body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector3 rotation label
		BeginSection(1);
		GUILayout.Label("Vector3 : rotation", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}