using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class BlueprintNodeFunctionLoadScene_String : BlueprintNodeFunction
{
	//Executes the blueprint node function load scene instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Load the scene with matching name to the input string text
			SceneManager.LoadScene((string)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute());
		}

		//Perform base execution
		base.Execute();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node function load scene instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node function load scene
		connections.Add(new BlueprintConnectionAttributeInputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
	}

	//Returns the blueprint node function load scene title
	protected override string GetTitle()
	{
		//Return the blueprint node function load scene title
		return "LoadScene";
	}

	//Renders the blueprint node function load scene body components
	protected override void RenderBodyComponents()
	{
		//Render the scene name label
		BeginSection(1);
			GUILayout.Label("String : sceneName", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}