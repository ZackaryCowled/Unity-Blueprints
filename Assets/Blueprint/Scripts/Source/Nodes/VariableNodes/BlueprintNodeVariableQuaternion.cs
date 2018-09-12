//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeVariableQuaternion : BlueprintNodeVariable
{
	//Public attributes
	public float attributeX = 0.0f;
	public float attributeY = 0.0f;
	public float attributeZ = 0.0f;
	public float attributeW = 1.0f;

	//Returns the blueprint node variable Quaternion output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return new Quaternion(attributeX, attributeY, attributeZ, attributeW);
	}

	//Sets the blueprint node variable Quaternion output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node variable Quaternion output attribute to the specified input attribute
		attributeX = ((Quaternion)inputAttribute).x;
		attributeY = ((Quaternion)inputAttribute).y;
		attributeZ = ((Quaternion)inputAttribute).z;
		attributeW = ((Quaternion)inputAttribute).w;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private float floatFieldWidth = 20.0f;

	//Initializes the blueprint node variable Quaternion instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node variable Quaternion
		connections.Add(new BlueprintConnectionAttributeOutputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Returns the blueprint node variable Quaternion title
	protected override string GetTitle()
	{
		//Return the blueprint node variable Quaternion title
		return "Quaternion";
	}

	//Renders the blueprint node attribute variable body components
	protected override void RenderBodyComponents()
	{
		//Perform base render body components
		base.RenderBodyComponents();

		//Render the x, y, z and w labels
		BeginSection(2);
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

	//Renders the blueprint node variable Quaternion inspector and returns true if changes to any attribute occurred
	public override bool RenderInspector()
	{
		//Render Quaternion field
		Vector4 tempValue = EditorGUILayout.Vector4Field(variableName, new Vector4(attributeX, attributeY, attributeZ, attributeW));

		//If the attribute value has changed
		if (tempValue != new Vector4(attributeX, attributeY, attributeZ, attributeW))
		{
			//Update the attribute values
			attributeX = tempValue.x;
			attributeY = tempValue.y;
			attributeZ = tempValue.z;
			attributeW = tempValue.w;

			//Changes occurred
			return true;
		}

		//No changes occurred
		return false;
	}

	//Renders the blueprint node variable vector4 type name information
	public override void RenderTypeNameInformation()
	{
		//Render the blueprint node variable vector4 type name information
		GUILayout.Label("Quaternion : " + variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}