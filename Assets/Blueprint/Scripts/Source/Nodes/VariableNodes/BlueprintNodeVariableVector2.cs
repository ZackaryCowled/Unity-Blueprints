//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeVariableVector2 : BlueprintNodeVariable
{
	//Public attributes
	public float attributeX = 0.0f;
	public float attributeY = 0.0f;

	//Returns the blueprint node variable vector2 output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return new Vector2(attributeX, attributeY);
	}

	//Sets the blueprint node variable vector2 output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node variable vector2 output attribute to the specified input attribute
		attributeX = ((Vector2)inputAttribute).x;
		attributeY = ((Vector2)inputAttribute).y;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private float floatFieldWidth = 60.0f;

	//Initializes the blueprint node variable vector2 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node variable vector2
		connections.Add(new BlueprintConnectionAttributeOutputVector2());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Generates and returns a valid input connection for the blueprint node variable vector2
	public override BlueprintConnection GenerateInputConnection()
	{
		//Return a valid input connection for the blueprint node variable vector2
		return new BlueprintConnectionAttributeInputVector2();
	}

	//Returns the blueprint node variable vector2 title
	protected override string GetTitle()
	{
		//Return the blueprint node variable vector2 title
		return "Vector2";
	}

	//Renders the blueprint node variable vector2 body components
	protected override void RenderBodyComponents()
	{
		//Perform base render body components
		base.RenderBodyComponents();

		//Render the x and y labels
		BeginSection(2);
			GUILayout.Label("x", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeX = EditorGUILayout.FloatField(attributeX, GUILayout.Width(floatFieldWidth));
			GUILayout.Label("y", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeY = EditorGUILayout.FloatField(attributeY, GUILayout.Width(floatFieldWidth));
		EndSection();
	}

	//Renders the blueprint node variable vector2 inspector and returns true if changes to any attributes occurred
	public override bool RenderInspector()
	{
		//Render vector2 field
		Vector2 tempValue = EditorGUILayout.Vector2Field(variableName, new Vector2(attributeX, attributeY));

		//If the attribute value has changed
		if (tempValue != new Vector2(attributeX, attributeY))
		{
			//Update the attribute values
			attributeX = tempValue.x;
			attributeY = tempValue.y;

			//Changes occurred
			return true;
		}

		//No changes occurred
		return false;
	}

	//Renders the blueprint node variable vector2 type name information
	public override void RenderTypeNameInformation()
	{
		//Render the blueprint node variable vector2 type name information
		GUILayout.Label("Vector2 : " + variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}