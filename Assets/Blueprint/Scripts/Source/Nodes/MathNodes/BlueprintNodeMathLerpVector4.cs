using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMathLerpVector4 : BlueprintNodeMath
{
	//Returns the blueprint node math lerp vector4 output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1 && connections[2].connectionNodeID > -1)
		{
			//Return output attribute
			return Vector4.Lerp((Vector4)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute(),
								(Vector4)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute(),
								(float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute());
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node math lerp vector4 default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node math lerp vector4 default output attribute
		return new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node math lerp vector4 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node math lerp vector4
		connections.Add(new BlueprintConnectionAttributeInputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 3, true);
		connections.Add(new BlueprintConnectionAttributeOutputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node math lerp vector4 title
	protected override string GetTitle()
	{
		//Return the blueprint node math lerp vector4 title
		return "LerpVector4";
	}

	//Renders the blueprint node math lerp vector4 body components
	protected override void RenderBodyComponents()
	{
		//Render the start vector4 label
		BeginSection(1);
			GUILayout.Label("Vector4 : start", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the end vector4 label
		BeginSection(2);
			GUILayout.Label("Vector4 : end", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the percent float label
		BeginSection(3);
			GUILayout.Label("Float : percent", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}