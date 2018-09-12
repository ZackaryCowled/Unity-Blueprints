//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttributeQuaternion : BlueprintNodeAttribute
{
	//Public attributes
	public float attributeX = 0.0f;
	public float attributeY = 0.0f;
	public float attributeZ = 0.0f;
	public float attributeW = 1.0f;

	//Returns the blueprint node attribute Quaternion output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return new Quaternion(attributeX, attributeY, attributeZ, attributeW);
	}

	//Sets the blueprint node attribute Quaternion output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node attribute Quaternion output attribute to the specified input attribute
		attributeX = ((Quaternion)inputAttribute).x;
		attributeY = ((Quaternion)inputAttribute).y;
		attributeZ = ((Quaternion)inputAttribute).z;
		attributeW = ((Quaternion)inputAttribute).w;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private float floatFieldWidth = 20.0f;

	//Initializes the blueprint node attribute Quaternion instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node attribute Quaternion
		connections.Add(new BlueprintConnectionAttributeOutputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node attribute Quaternion title
	protected override string GetTitle()
	{
		//Return the blueprint node attribute Quaternion title
		return "Quaternion";
	}

	//Renders the blueprint node attribute Quaternion body components
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