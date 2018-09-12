using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMathMagnitudeVector4 : BlueprintNodeMath
{
	//Returns the blueprint node math magnitude vector4 output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Return output attribute
			return Vector4.Magnitude((Vector4)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute());
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node math magnitude vector4 default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node math magnitude vector4 default output attribute
		return new Vector4(0.0f, 0.0f, 0.0f);
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node math magnitude vector4 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node math magnitude vector4
		connections.Add(new BlueprintConnectionAttributeInputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Returns the blueprint node math magnitude vector4 title
	protected override string GetTitle()
	{
		//Return the blueprint node math magnitude vector4 title
		return "Magnitude";
	}

	//Renders the blueprint node math magnitude vector4 body components
	protected override void RenderBodyComponents()
	{
		//Render the input vector4 label
		BeginSection(1);
			GUILayout.Label("Vector4 : input", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the output float label
		BeginSection(2);
			GUILayout.Label("Float : output", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}