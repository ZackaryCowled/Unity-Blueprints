using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMathDivideFloat : BlueprintNodeMath
{
	//Returns the blueprint node math divide float output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Return output attribute
			return ((float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute()) /
				   ((float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute());
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node math divide float default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node math divide float default output attribute
		return 0.0f;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node math divide float instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node math divide float
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node math divide float title
	protected override string GetTitle()
	{
		//Return the blueprint node math divide float title
		return "DivideFloat";
	}

	//Renders the blueprint node math divide float body components
	protected override void RenderBodyComponents()
	{
		//Render the float label
		BeginSection(1);
		GUILayout.Label("Float", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the float label
		BeginSection(2);
		GUILayout.Label("Float", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}