//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetAxis : BlueprintNodeGet
{
	//Public attributes
	public string axisName = "";
	public float deadzone = 0.0f;

	//Returns the blueprint node get get axis output attribute
	public override object GetAttribute()
	{
		//Query axis value
		float axisValue = Input.GetAxis(axisName);

		//If the value is outside the deadzone
		if (Mathf.Abs(axisValue) > deadzone)
		{
			//Return the axis value
			return axisValue;
		}

		//Return default output attribute
		return 0.0f;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get axis instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get axis position
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
		connections.Add(new BlueprintConnectionInvisible());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Returns the blueprint node get get axis title
	protected override string GetTitle()
	{
		//Return the blueprint node get get axis title
		return "GetAxis";
	}

	//Return the blueprint node get get axis body components
	protected override void RenderBodyComponents()
	{
		//Render the axis label
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