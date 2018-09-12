using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttributeBlueprint : BlueprintNodeAttribute
{
	//Private attributes
	private int linkedBlueprintID = -1;

	//Returns the blueprint node attribute blueprint output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return linkedBlueprintID;
	}

	//Sets the blueprint node attribute blueprint output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//Set the blueprint node attribute blueprint output attribtue to the specified input attribute
		linkedBlueprintID = (int)inputAttribute;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node query blueprint instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node query blueprint
		connections.Add(new BlueprintConnectionAttributeOutputBlueprint());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node attribute blueprint title
	protected override string GetTitle()
	{
		//Return the blueprint node attribute blueprint title
		return "Blueprint";
	}

	//Renders the blueprint node attribute blueprint body components
	protected override void RenderBodyComponents()
	{
		//Render the Blueprint blueprint label
		BeginSection(1);
			GUILayout.Label("Blueprint : blueprint", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}