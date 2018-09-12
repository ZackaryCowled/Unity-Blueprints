//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeVariableInt : BlueprintNodeVariable
{
	//Public attributes
	public int attributeValue = 0;

	//Returns the blueprint node variable int output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return attributeValue;
	}

	//Sets the blueprint node variable int output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node variable int output attribute to the specified input attribute
		attributeValue = (int)inputAttribute;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node variable int instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node variable int
		connections.Add(new BlueprintConnectionAttributeOutputInt());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Generates and returns a valid input connection for the blueprint node variable int
	public override BlueprintConnection GenerateInputConnection()
	{
		//Return a valid input connection for the blueprint node variable int
		return new BlueprintConnectionAttributeInputInt();
	}

	//Returns the blueprint node variable int title
	protected override string GetTitle()
	{
		//Return the blueprint node variable int title
		return "Int";
	}

	//Renders the blueprint node variable int body components
	protected override void RenderBodyComponents()
	{
		//Perform base render body components
		base.RenderBodyComponents();

		//Render the value label
		BeginSection(2);
			GUILayout.Label("value", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeValue = EditorGUILayout.IntField(attributeValue);
		EndSection();
	}

	//Renders the blueprint node variable int inspector and returns true if changes to any attribute occurred
	public override bool RenderInspector()
	{
		//Render int field
		int tempValue = EditorGUILayout.IntField(variableName, attributeValue);

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

	//Renders the blueprint node variable int type name information
	public override void RenderTypeNameInformation()
	{
		//Renders the blueprint node variable int type name information
		GUILayout.Label("Int : " + variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}