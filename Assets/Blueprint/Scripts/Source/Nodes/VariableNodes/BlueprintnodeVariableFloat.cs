//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeVariableFloat : BlueprintNodeVariable
{
	//Public attributes
	public float attributeValue = 0.0f;

	//Returns the blueprint node variable float output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return attributeValue;
	}

	//Sets the blueprint node variable float output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node variable float output attribute to the specified input attribute
		attributeValue = (float)inputAttribute;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node variable float instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node variable float
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Generates and returns a valid input connection for the blueprint node variable float
	public override BlueprintConnection GenerateInputConnection()
	{
		//Return a valid input connection for the blueprint node variable float
		return new BlueprintConnectionAttributeInputFloat();
	}

	//Returns the blueprint node variable float title
	protected override string GetTitle()
	{
		//Return the blueprint node variable float title
		return "Float";
	}

	//Renders the blueprint node variable float body components
	protected override void RenderBodyComponents()
	{
		//Perform base render body components
		base.RenderBodyComponents();

		//Render the value label
		BeginSection(2);
			GUILayout.Label("value", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeValue = EditorGUILayout.FloatField(attributeValue);
		EndSection();
	}

	//Renders the blueprint node variable float inspector and returns true if changes to any attribute occurred
	public override bool RenderInspector()
	{
		//Render float field
		float tempValue = EditorGUILayout.FloatField(variableName, attributeValue);

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

	//Renders the blueprint node variable float type name information
	public override void RenderTypeNameInformation()
	{
		//Render the blueprint node variable float type name information
		GUILayout.Label("Float : " + variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}