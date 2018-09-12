//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttributeString : BlueprintNodeAttribute
{
	//Public attributes
	public string attributeValue = "";

	//Returns the blueprint node attribute string output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return attributeValue;
	}

	//Sets the blueprint node attribute string output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node attribute string output attribute to the specified inptu attribute
		attributeValue = inputAttribute.ToString();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node attribute string instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node attribute string
		connections.Add(new BlueprintConnectionAttributeOutputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node attribute string title
	protected override string GetTitle()
	{
		//Return the blueprint node attribute string title
		return "String";
	}

	//Renders the blueprint node attribute string body components
	protected override void RenderBodyComponents()
	{
		//Render the text label
		BeginSection(1);
			GUILayout.Label("text", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			attributeValue = EditorGUILayout.TextField(attributeValue);
		EndSection();
	}
#endif
}