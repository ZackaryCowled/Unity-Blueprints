//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttributeGameObject : BlueprintNodeAttribute
{
	//Public attributes
	public GameObject attributeValue = null;

	//Returns the blueprint node attribute gameobject output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return attributeValue;
	}

	//Sets the blueprint node attribute gameobject output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node attribute gameobject output attribute to the specified input attribute
		attributeValue = (GameObject)inputAttribute;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node attribute gameobject instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node attribute gameobject
		connections.Add(new BlueprintConnectionAttributeOutputGameObject());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node attribute gameobject title
	protected override string GetTitle()
	{
		//Return the blueprint node attribute gameobject title
		return "GameObject";
	}

	//Renders the blueprint node attribute gameobject body components
	protected override void RenderBodyComponents()
	{
		//Render the gameobject label
		BeginSection(1);
			GUILayout.Label("GameObject : object", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}