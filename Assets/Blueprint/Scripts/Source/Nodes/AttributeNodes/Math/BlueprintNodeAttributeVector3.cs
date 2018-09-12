//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttributeVector3 : BlueprintNodeAttribute
{
	//Public attributes
	public float attributeX = 0.0f;
	public float attributeY = 0.0f;
	public float attributeZ = 0.0f;

	//Returns the blueprint node attribute vector3 output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return new Vector3(attributeX, attributeY, attributeZ);
	}

	//Sets the blueprint node attribute vector3 output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node attribute vector3 output attribute to the specified input attribute
		attributeX = ((Vector3)inputAttribute).x;
		attributeY = ((Vector3)inputAttribute).y;
		attributeZ = ((Vector3)inputAttribute).z;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private float floatFieldWidth = 35.0f;

	//Initializes the blueprint node attribute vector3 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node attribute vector3
		connections.Add(new BlueprintConnectionAttributeOutputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node attribute vector3 title
	protected override string GetTitle()
	{
		//Return the blueprint node attribute vector3 title
		return "Vector3";
	}

	//Renders the blueprint node attribute vector3 body components
	protected override void RenderBodyComponents()
	{
		//Render the x, y and z labels
		BeginSection(1);
			GUILayout.Label("x", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeX = EditorGUILayout.FloatField(attributeX, GUILayout.Width(floatFieldWidth));
			GUILayout.Label("y", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeY = EditorGUILayout.FloatField(attributeY, GUILayout.Width(floatFieldWidth));
			GUILayout.Label("z", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeZ = EditorGUILayout.FloatField(attributeZ, GUILayout.Width(floatFieldWidth));
		EndSection();
	}
#endif
}