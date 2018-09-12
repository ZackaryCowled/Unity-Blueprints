//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeCompare : BlueprintNode
{
	//Public attributes
	public int mode = 0;
	public bool liteMode = false;

	//Protected attributes
	protected bool result = false;

	//Executes the blueprint node compare instance
	public override void Execute()
	{
		//If the result is true
		if (result)
		{
			//If the true output execution connection is valid
			if (connections[0].connectionNodeID > -1)
			{
				//Execute the true output execution node
				BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).Execute();
			}
		}
		//Otherwise
		else
		{
			//If the false output execution connection is valid
			if (connections[1].connectionNodeID > -1)
			{
				//Execute the false output execution node
				BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).Execute();
			}
		}
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Modes
	private static readonly string[] modes = new string[]
	{
		"If a is more than b",
		"If a is a less than b",
		"If a is equal to b",
		"If a is more or equal to b",
		"If a is less or equal to b",
		"If a is not equal to b"
	};

	//Modes lite
	private static readonly string[] modesLite = new string[]
	{
		"If a is equal to b",
		"If a is not equal to b"
	};

	//Initializes the blueprint node compare instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node compare
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		connections.Add(new BlueprintConnectionOutputExecution());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
		connections.Add(new BlueprintConnectionOutputExecution());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 3, false);
	}

	//Returns the blueprint node compare header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node compare header color
		return new Color(0.75f, 1.0f, 0.0f);
	}

	//Returns the blueprint node compare type name
	protected virtual string GetTypeName()
	{
		//Return the default type name
		return "Any";
	}

	//Renders the blueprint node compare body components
	protected override void RenderBodyComponents()
	{
		//Render the mode dropdown menu
		BeginSection(1);
			mode = EditorGUILayout.Popup(mode, liteMode ? modesLite : modes);
		EndSection();

		//Render the true output label
		BeginSection(2);
		GUILayout.Label("Execute True", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the b label
		BeginSection(3);
		GUILayout.Label("Execute False", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the a label
		BeginSection(4);
			GUILayout.Label(GetTypeName() + " : a", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the b label
		BeginSection(5);
			GUILayout.Label(GetTypeName() + " : b", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}