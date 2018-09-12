//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttributeBool : BlueprintNodeAttribute
{
	//Public attributes
	public bool attributeValue = false;

	//Returns the blueprint node attribute bool output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return attributeValue;
	}

	//Sets the blueprint node attribute bool output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node attribute bool output attribute to the specified input attribute
		attributeValue = (bool)inputAttribute;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node attribute bool instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node attribute bool
		connections.Add(new BlueprintConnectionAttributeOutputBool());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node attribute bool title
	protected override string GetTitle()
	{
		//Return the blueprint node attribute bool title
		return "Bool";
	}

	//Renders the blueprint node attribute bool body components
	protected override void RenderBodyComponents()
	{
		//Render the value label
		BeginSection(1);
			GUILayout.Label("value", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeValue = EditorGUILayout.Toggle(attributeValue);
		EndSection();
	}
#endif
}