using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionLogWarning : BlueprintNodeFunction
{
	//Executes the blueprint node function log warning instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Log warning the attribute to the console
			Debug.LogWarning(BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute().ToString());
		}

		//Perform base execution
		base.Execute();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node function log warning instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node function log warning
		connections.Add(new BlueprintConnectionAttributeInputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
	}

	//Returns the blueprint node function log warning title
	protected override string GetTitle()
	{
		//Return the blueprint node function log warning title
		return "LogWarning";
	}

	//Renders the blueprint node function log warning body components
	protected override void RenderBodyComponents()
	{
		//Render the message label
		BeginSection(1);
			GUILayout.Label("Any: message", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}