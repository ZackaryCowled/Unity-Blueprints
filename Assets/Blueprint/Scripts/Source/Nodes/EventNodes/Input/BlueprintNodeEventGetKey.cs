//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventGetKey : BlueprintNodeEventCondition
{
	//Public attributes
	public KeyCode selectedKey = KeyCode.None;

	//Returns a flag indicating whether the blueprint node event get key condition is being met
	public override bool IsConditionMet()
	{
		//If the selected key is being pressed
		if (Input.GetKey(selectedKey))
		{
			//The condition is being met
			return true;
		}

		//The condition is not being met
		return false;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node event get key instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node event get key
		connections.Add(new BlueprintConnectionInvisible());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node event get key title
	protected override string GetTitle()
	{
		//Return the blueprint node event get key title
		return "GetKey";
	}

	//Renders the blueprint node event get key body components
	protected override void RenderBodyComponents()
	{
		//Render the selected key dropdown menu
		BeginSection(1);
			selectedKey = (KeyCode)EditorGUILayout.EnumPopup(selectedKey);
		EndSection();
	}
#endif
}