//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttributeInt : BlueprintNodeAttribute
{
	//Public attributes
	public int attributeValue = 0;

	//Returns the blueprint node attribute int output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return attributeValue;
	}

	//Sets the blueprint node attribute int output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node attribute int output attribute to the specified input attribute
		attributeValue = (int)inputAttribute;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node attribute int instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node attribute int
		connections.Add(new BlueprintConnectionAttributeOutputInt());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node attribute int title
	protected override string GetTitle()
	{
		//Return the blueprint node attribute int title
		return "Int";
	}

	//Renders the blueprint node attribute int body components
	protected override void RenderBodyComponents()
	{
		//Render the value label
		BeginSection(1);
			GUILayout.Label("value", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeValue = EditorGUILayout.IntField(attributeValue);
		EndSection();
	}
#endif
}