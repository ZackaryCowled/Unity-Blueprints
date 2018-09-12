//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventGetMouseButtonUp : BlueprintNodeEventCondition
{
	//Public attributes
	public int selectedMouseButton = 0;

	//Returns a flag indicating whether the blueprint node event get mouse button up condition is being met
	public override bool IsConditionMet()
	{
		//If the selected mouse button is being released up this frame
		if (Input.GetMouseButtonUp(selectedMouseButton))
		{
			//The condition is being met
			return true;
		}

		//The condition is not being met
		return false;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private static readonly string[] mouseButtons = new string[]
	{
		"Mouse 0",
		"Mouse 1",
		"Mouse 2",
		"Mouse 3",
		"Mouse 4",
		"Mouse 5",
		"Mouse 6"
	};

	//Initializes the blueprint node event get mouse button up instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node event get mouse up button
		connections.Add(new BlueprintConnectionInvisible());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node event get mouse button up title
	protected override string GetTitle()
	{
		//Return the blueprint node event get mouse button up title
		return "GetMouseButtonUp";
	}

	//Renders the blueprint node event get mouse button up body components
	protected override void RenderBodyComponents()
	{
		//Render the selected key dropdown menu
		BeginSection(1);
			selectedMouseButton = EditorGUILayout.Popup(selectedMouseButton, mouseButtons);
		EndSection();
	}
#endif
}