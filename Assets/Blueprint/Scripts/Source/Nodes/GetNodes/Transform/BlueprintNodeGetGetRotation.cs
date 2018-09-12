using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetRotation : BlueprintNodeGet
{
	//Returns the blueprint node get get rotation output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.rotation.eulerAngles;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get rotation instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get rotation
		connections.Add(new BlueprintConnectionAttributeOutputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get rotation title
	protected override string GetTitle()
	{
		//Return the blueprint node get get rotation title
		return "GetRotation";
	}

	//Return the blueprint node get get rotation body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector3 rotation label
		BeginSection(1);
		GUILayout.Label("Vector3 : rotation", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}