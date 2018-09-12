using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintNodeGetGetLocalScale : BlueprintNodeGet
{
	//Returns the blueprint node get get local scale output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.localScale;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get local scale instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get local scale position
		connections.Add(new BlueprintConnectionAttributeOutputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get local scale title
	protected override string GetTitle()
	{
		//Return the blueprint node get get local scale title
		return "GetLocalScale";
	}

	//Return the blueprint node get get local scale body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector3 scale label
		BeginSection(1);
		GUILayout.Label("Vector3 : scale", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}