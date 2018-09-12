using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionLookAt_Vector3 : BlueprintNodeFunction
{
	//Executes the blueprint node function look at instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Rotate the blueprints parents Gameobject to look at the Vector3 input attribute location
			BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.LookAt((Vector3)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute());
		}

		//Perform base execution
		base.Execute();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node function look at instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node function look at
		connections.Add(new BlueprintConnectionAttributeInputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
	}

	//Returns the blueprint node function look at title
	protected override string GetTitle()
	{
		//Return the blueprint node function look at title
		return "LookAt";
	}

	//Renders the blueprint node function look at body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector3 location label
		BeginSection(1);
			GUILayout.Label("Vector3 : location", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}