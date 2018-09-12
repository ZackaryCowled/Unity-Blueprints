using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMathLerpQuaternion : BlueprintNodeMath
{
	//Returns the blueprint node math lerp Quaternion output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1 && connections[2].connectionNodeID > -1)
		{
			//Return output attribute
			return Quaternion.Lerp((Quaternion)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute(),
								(Quaternion)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute(),
								(float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute());
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node math lerp Quaternion default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node math lerp Quaternion default output attribute
		return Quaternion.identity;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node math lerp Quaternion instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node math lerp Quaternion
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 3, true);
		connections.Add(new BlueprintConnectionAttributeOutputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node math lerp Quaternion title
	protected override string GetTitle()
	{
		//Return the blueprint node math lerp Quaternion title
		return "LerpQuaternion";
	}

	//Renders the blueprint node math lerp Quaternion body components
	protected override void RenderBodyComponents()
	{
		//Render the start Quaternion label
		BeginSection(1);
			GUILayout.Label("Quaternion : start", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the end Quaternion label
		BeginSection(2);
			GUILayout.Label("Quaternion : end", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the percent float label
		BeginSection(3);
			GUILayout.Label("Float : percent", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}