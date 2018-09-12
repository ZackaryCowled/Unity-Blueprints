//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class BlueprintNodeVariableVector3 : BlueprintNodeVariable
{
	//Public attributes
	public float attributeX = 0.0f;
	public float attributeY = 0.0f;
	public float attributeZ = 0.0f;

	//Returns the blueprint node variable vector3 output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return new Vector3(attributeX, attributeY, attributeZ);
	}

	//Sets the blueprint node variable vector3 output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node variable vector3 output attribute to the specified input attribute
		attributeX = ((Vector3)inputAttribute).x;
		attributeY = ((Vector3)inputAttribute).y;
		attributeZ = ((Vector3)inputAttribute).z;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private float floatFieldWidth = 35.0f;

	//Initializes the blueprint node variable vector3 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node variable vector3
		connections.Add(new BlueprintConnectionAttributeOutputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Generates and returns a valid input connection for the blueprint node variable vector3
	public override BlueprintConnection GenerateInputConnection()
	{
		//Return a valid input connection for the blueprint node variable vector3
		return new BlueprintConnectionAttributeInputVector3();
	}

	//Returns the blueprint node variable vector3 title
	protected override string GetTitle()
	{
		//Return the blueprint node variable vector3 title
		return "Vector3";
	}

	//Renders the blueprint node variable vector3 body components
	protected override void RenderBodyComponents()
	{
		//Perform base render body components
		base.RenderBodyComponents();

		//Render the x, y and z labels
		BeginSection(2);
			GUILayout.Label("x", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeX = EditorGUILayout.FloatField(attributeX, GUILayout.Width(floatFieldWidth));
			GUILayout.Label("y", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeY = EditorGUILayout.FloatField(attributeY, GUILayout.Width(floatFieldWidth));
			GUILayout.Label("z", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeZ = EditorGUILayout.FloatField(attributeZ, GUILayout.Width(floatFieldWidth));
		EndSection();
	}

	//Renders the blueprint node variable vector3 inspector and returns true if changes to any attributes occurred
	public override bool RenderInspector()
	{
		//Render Vector3 field
		Vector3 tempValue = EditorGUILayout.Vector3Field(variableName, new Vector3(attributeX, attributeY, attributeZ));
		
		//If the attribute value has changed
		if(tempValue != new Vector3(attributeX, attributeY, attributeZ))
		{
			//Update the attribute values
			attributeX = tempValue.x;
			attributeY = tempValue.y;
			attributeZ = tempValue.z;

			//Changes occurred
			return true;
		}

		//No changes occurred
		return false;
	}

	//Renders the blueprint node variable vector3 type name information
	public override void RenderTypeNameInformation()
	{
		//Render the blueprint node variable vector3 type name information
		GUILayout.Label("Vector3 : " + variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}