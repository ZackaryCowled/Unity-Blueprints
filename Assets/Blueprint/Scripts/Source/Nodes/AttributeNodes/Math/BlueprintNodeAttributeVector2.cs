//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttributeVector2 : BlueprintNodeAttribute
{
	//Public attributes
	public float attributeX = 0.0f;
	public float attributeY = 0.0f;

	//Returns the blueprint node attribute vector2 output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return new Vector2(attributeX, attributeY);
	}

	//Sets the blueprint node attribute vector2 output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node attribute vector2 output attribute to the specified input attribute
		attributeX = ((Vector2)inputAttribute).x;
		attributeY = ((Vector2)inputAttribute).y;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private float floatFieldWidth = 60.0f;

	//Initializes the blueprint node attribute vector2 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node attribute vector2
		connections.Add(new BlueprintConnectionAttributeOutputVector2());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node attribute vector2 title
	protected override string GetTitle()
	{
		//Return the blueprint node attribute vector2 title
		return "Vector2";
	}

	//Renders the blueprint node attribute vector2 body components
	protected override void RenderBodyComponents()
	{
		//Render the x and y labels
		BeginSection(1);
			GUILayout.Label("x", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeX = EditorGUILayout.FloatField(attributeX, GUILayout.Width(floatFieldWidth));
			GUILayout.Label("y", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeY = EditorGUILayout.FloatField(attributeY, GUILayout.Width(floatFieldWidth));
		EndSection();
	}
#endif
}