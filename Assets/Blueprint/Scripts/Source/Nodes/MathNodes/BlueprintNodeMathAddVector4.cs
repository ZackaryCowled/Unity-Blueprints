using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMathAddVector4 : BlueprintNodeMath
{
	//Returns the blueprint node math add vector4 output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Return output attribute
			return ((Vector4)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute()) +
				   ((Vector4)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute());
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node math add vector4 default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node math add vector4 default output attribute
		return new Vector4(0.0f, 0.0f, 0.0f);
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node math add vector4 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node math add vector4
		connections.Add(new BlueprintConnectionAttributeInputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeOutputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node math add vector4 title
	protected override string GetTitle()
	{
		//Return the blueprint node math add vector4 title
		return "AddVector4";
	}

	//Renders the blueprint node math add vector4 body components
	protected override void RenderBodyComponents()
	{
		//Render the vector4 label
		BeginSection(1);
		GUILayout.Label("Vector4", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the vector4 label
		BeginSection(2);
		GUILayout.Label("Vector4", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}