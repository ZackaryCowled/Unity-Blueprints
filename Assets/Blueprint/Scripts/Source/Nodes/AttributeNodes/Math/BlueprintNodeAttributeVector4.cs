//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttributeVector4 : BlueprintNodeAttribute
{
	//Public attributes
	public float attributeX = 0.0f;
	public float attributeY = 0.0f;
	public float attributeZ = 0.0f;
	public float attributeW = 0.0f;

	//Returns the blueprint node attribute vector4 output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return new Vector4(attributeX, attributeY, attributeZ, attributeW);
	}

	//Sets the blueprint node attribute vector4 output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node attribute vector4 output attribute to the specified input attribute
		attributeX = ((Vector4)inputAttribute).x;
		attributeY = ((Vector4)inputAttribute).y;
		attributeZ = ((Vector4)inputAttribute).z;
		attributeW = ((Vector4)inputAttribute).w;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private float floatFieldWidth = 20.0f;

	//Initializes the blueprint node attribute vector4 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node attribute vector4
		connections.Add(new BlueprintConnectionAttributeOutputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node attribute vector4 title
	protected override string GetTitle()
	{
		//Return the blueprint node attribute vector4 title
		return "Vector4";
	}

	//Renders the blueprint node attribute vector4 body components
	protected override void RenderBodyComponents()
	{
		//Render the x, y, z and w labels
		BeginSection(1);
			GUILayout.Label("x", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeX = EditorGUILayout.FloatField(attributeX, GUILayout.Width(floatFieldWidth));
			GUILayout.Label("y", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeY = EditorGUILayout.FloatField(attributeY, GUILayout.Width(floatFieldWidth));
			GUILayout.Label("z", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeZ = EditorGUILayout.FloatField(attributeZ, GUILayout.Width(floatFieldWidth));
			GUILayout.Label("w", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeW = EditorGUILayout.FloatField(attributeW, GUILayout.Width(floatFieldWidth));
		EndSection();
	}
#endif
}