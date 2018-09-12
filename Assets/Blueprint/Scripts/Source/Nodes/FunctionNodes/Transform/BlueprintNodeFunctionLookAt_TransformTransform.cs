using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionLookAt_TransformTransform : BlueprintNodeFunction
{
	//Executes the blueprint node function look at instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Rotate the transform input attribute to look at the other transform input attribute
			((Transform)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute()).LookAt((Transform)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute());
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
		connections.Add(new BlueprintConnectionAttributeInputTransform());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputTransform());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
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
		//Render the Transform transform label
		BeginSection(1);
			GUILayout.Label("Transform : transform", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the Transform target label
		BeginSection(2);
			GUILayout.Label("Transform : target", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}