using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetScale : BlueprintNodeGet
{
	//Returns the blueprint node get get scale output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.lossyScale;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get scale instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get scale position
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
		//Render the Vector3 scale label
		BeginSection(1);
			GUILayout.Label("Vector3 : scale", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}