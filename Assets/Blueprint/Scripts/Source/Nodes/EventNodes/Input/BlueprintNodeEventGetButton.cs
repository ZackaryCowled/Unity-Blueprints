//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventGetButton : BlueprintNodeEventCondition
{
	//Public attributes
	public string buttonName = "";

	//Returns a flag indicating whether the blueprint node event get button condition is being met
	public override bool IsConditionMet()
	{
		//If the selected button is being pressed
		if (Input.GetButton(buttonName))
		{
			//The condition is being met
			return true;
		}

		//The condition is not being met
		return false;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node event get button instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node event get button button
		connections.Add(new BlueprintConnectionInvisible());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node event get button title
	protected override string GetTitle()
	{
		//Return the blueprint node event get button title
		return "GetButton";
	}

	//Renders the blueprint node event get button body components
	protected override void RenderBodyComponents()
	{
		//Render the button name label
		BeginSection(1);
			GUILayout.Label("button", BlueprintStyleHelper.GetNodeAttributeTextStyle());
			buttonName = EditorGUILayout.TextField(buttonName);
		EndSection();
	}
#endif
}