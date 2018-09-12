using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeTimeDeltaTime : BlueprintNodeTime
{
	//Returns the blueprint node output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return Time.deltaTime;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initialize the blueprint node time delta time instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node time delta time
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node time delta time title
	protected override string GetTitle()
	{
		//Return the blueprint node time delta time title
		return "DeltaTime";
	}

	//Renders the blueprint node time delta time body components
	protected override void RenderBodyComponents()
	{
		//Render the delta time label
		BeginSection(1);
			GUILayout.Label("Float : deltaTime", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}