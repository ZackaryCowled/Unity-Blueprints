using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeTimeRunTime : BlueprintNodeTime
{
	//Returns the bluepring node output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return Time.time;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initialize the blueprint node time run time instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node time run time
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node time run time title
	protected override string GetTitle()
	{
		//Return the blueprint node time run time title
		return "RunTime";
	}

	//Renders the blueprint node time run time body components
	protected override void RenderBodyComponents()
	{
		//Render the run time label
		BeginSection(1);
			GUILayout.Label("Float : runTime", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}