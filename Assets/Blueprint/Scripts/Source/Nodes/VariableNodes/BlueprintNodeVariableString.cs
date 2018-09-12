//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeVariableString : BlueprintNodeVariable
{
	//Public attributes
	public string attributeValue = "";

	//Returns the blueprint node variable string output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return attributeValue;
	}

	//Sets the blueprint node variable string output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node variable string output attribute to the specified input attribute
		attributeValue = inputAttribute.ToString();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node variable string instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node variable string
		connections.Add(new BlueprintConnectionAttributeOutputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Generates and returns a valid input connection for the blueprint node variable string
	public override BlueprintConnection GenerateInputConnection()
	{
		//Return a valid input connection for the blueprint node variable string
		return new BlueprintConnectionAttributeInputString();
	}

	//Returns the blueprint node variable string title
	protected override string GetTitle()
	{
		//Return the blueprint node variable string title
		return "String";
	}

	//Renders the blueprint node variable string body components
	protected override void RenderBodyComponents()
	{
		//Perform base render body components
		base.RenderBodyComponents();

		//Render the text label
		BeginSection(2);
		GUILayout.Label("text", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		attributeValue = EditorGUILayout.TextField(attributeValue);
		EndSection();
	}

	//Renders the blueprint node variable string inspector and returns true if changes to any attribute occurred
	public override bool RenderInspector()
	{
		//Create intermediary attributes
		string tempValue = "";

		//Render text field
		tempValue = EditorGUILayout.TextField(variableName, attributeValue);

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

	//Renders the blueprint node variable string type name information
	public override void RenderTypeNameInformation()
	{
		//Render the blueprint node variable string type name information
		GUILayout.Label("String : " + variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}