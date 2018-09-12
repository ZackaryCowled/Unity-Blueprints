using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMathMultiplyInt : BlueprintNodeMath
{
	//Returns the blueprint node math multiply int output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Return output attribute
			return ((int)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute()) *
				   ((int)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute());
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node math multiply int default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node math multiply int default output attribute
		return 0;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node math multiply int instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node math multiply int
		connections.Add(new BlueprintConnectionAttributeInputInt());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputInt());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeOutputInt());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node math multiply int title
	protected override string GetTitle()
	{
		//Return the blueprint node math multiply int title
		return "MultiplyInt";
	}

	//Renders the blueprint node math multiply int body components
	protected override void RenderBodyComponents()
	{
		//Render the int label
		BeginSection(1);
		GUILayout.Label("Int", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the int label
		BeginSection(2);
		GUILayout.Label("Int", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}