//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttributeFloat : BlueprintNodeAttribute
{
	//Public attributes
	public float attributeValue = 0.0f;

	//Returns the blueprint node attribute float output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return attributeValue;
	}

	//Sets the blueprint node attribute float output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node attribute float output attribute to the specified input attribute
		attributeValue = (float)inputAttribute;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node attribute float instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node attribute float
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node attribute float title
	protected override string GetTitle()
	{
		//Return the blueprint node attribute float title
		return "Float";
	}

	//Renders the blueprint node attribute float body components
	protected override void RenderBodyComponents()
	{
		//Render the value label
		BeginSection(1);
			GUILayout.Label("value", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeValue = EditorGUILayout.FloatField(attributeValue);
		EndSection();
	}
#endif
}