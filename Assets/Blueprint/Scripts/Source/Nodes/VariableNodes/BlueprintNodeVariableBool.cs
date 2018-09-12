//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeVariableBool : BlueprintNodeVariable
{
	//Public attributes
	public bool attributeValue = false;

	//Returns the blueprint node variable bool output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return attributeValue;
	}

	//Sets the blueprint node variable bool output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node variable bool output attribute to the specified input attribute
		attributeValue = (bool)inputAttribute;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node variable bool instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node variable bool
		connections.Add(new BlueprintConnectionAttributeOutputBool());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Generates and returns a valid input connection for the blueprint node variable bool
	public override BlueprintConnection GenerateInputConnection()
	{
		//Return a valid input connection for the blueprint node variable bool
		return new BlueprintConnectionAttributeInputBool();
	}

	//Returns the blueprint node variable bool title
	protected override string GetTitle()
	{
		//Return the blueprint node variable bool title
		return "Bool";
	}

	//Renders the blueprint node variable bool body components
	protected override void RenderBodyComponents()
	{
		//Perform base render body components
		base.RenderBodyComponents();

		//Render the value label
		BeginSection(2);
			GUILayout.Label("value", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeValue = EditorGUILayout.Toggle(attributeValue);
		EndSection();
	}

	//Renders the blueprint node variable bool inspector and return true if changes to any attribute occurred
	public override bool RenderInspector()
	{
		//Render bool field
		bool tempValue = EditorGUILayout.Toggle(variableName, attributeValue);

		//If the attribute value has changed
		if (tempValue != attributeValue)
		{
			//Update the attribute value
			attributeValue = tempValue;

			//Changes occurred
			return true;
		}

		//No changes occurred
		return false;
	}

	//Renders the blueprint node variable bool type name information
	public override void RenderTypeNameInformation()
	{
		//Render the blueprint node variable bool type name information
		GUILayout.Label("Bool : " + variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}
