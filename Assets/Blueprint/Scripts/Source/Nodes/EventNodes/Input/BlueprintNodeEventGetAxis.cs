//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventGetAxis : BlueprintNodeEventCondition
{
	//Public attributes
	public string axisName = "";
	public float deadzone = 0.0f;

	//Returns a flag indicating whether the blueprint node event get axis condition is being met
	public override bool IsConditionMet()
	{
		//If the selected axis is past the deadzone
		if (Mathf.Abs(Input.GetAxis(axisName)) > deadzone)
		{
			//The condition is being met
			return true;
		}

		//The condition is not being met
		return false;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node event get axis instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node event get axis button
		connections.Add(new BlueprintConnectionInvisible());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Returns the blueprint node event get axis title
	protected override string GetTitle()
	{
		//Return the blueprint node event get axis title
		return "GetAxis";
	}

	//Renders the blueprint node event get axis body components
	protected override void RenderBodyComponents()
	{
		//Render the axis name label
		BeginSection(1);
			GUILayout.Label("axis", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			axisName = EditorGUILayout.TextField(axisName);
		EndSection();

		//Render the deadzone label
		BeginSection(2);
			GUILayout.Label("deadzone", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			deadzone = EditorGUILayout.FloatField(deadzone);
		EndSection();
	}
#endif
}