using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMathLerpFloat : BlueprintNodeMath
{
	//Returns the blueprint node math lerp float output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1 && connections[2].connectionNodeID > -1)
		{
			//Return output attribute
			return Mathf.Lerp((float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute(),
							  (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute(),
							  (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute());
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node math lerp float default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node math lerp float default output attribute
		return 0.0f;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node math lerp float instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node math lerp float
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 3, true);
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node math lerp float title
	protected override string GetTitle()
	{
		//Return the blueprint node math lerp float title
		return "LerpFloat";
	}

	//Renders the blueprint node math lerp float body components
	protected override void RenderBodyComponents()
	{
		//Render the start float label
		BeginSection(1);
			GUILayout.Label("Float : start", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the end float label
		BeginSection(2);
			GUILayout.Label("Float : end", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the percent float label
		BeginSection(3);
			GUILayout.Label("Float : percent", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}