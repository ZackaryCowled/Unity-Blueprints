using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMathMultiplyQuaternion : BlueprintNodeMath
{
	//Returns the blueprint node math multiply Quaternion output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Return output attribute
			return (Quaternion)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute() *
			       (Quaternion)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute();
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node math multiply Quaternion default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node math multiply Quaternion default output attribute
		return Quaternion.identity;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node math multiply Quaternion instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node math multiply Quaternion
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeOutputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node math multiply Quaternion title
	protected override string GetTitle()
	{
		//Return the blueprint node math multiply Quaternion title
		return "MultiplyQuaternion";
	}

	//Renders the blueprint node math multiply Quaternion body components
	protected override void RenderBodyComponents()
	{
		//Render the Quaternion label
		BeginSection(1);
			GUILayout.Label("Quaternion", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the Quaternion label
		BeginSection(2);
			GUILayout.Label("Quaternion", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}