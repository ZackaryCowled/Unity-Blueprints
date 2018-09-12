using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionLookAt_Transform : BlueprintNodeFunction
{
	//Executes the blueprint node function look at instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Rotate the blueprint parents GameObject to look at the transform input attribute
			BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.LookAt((Transform)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute());
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
	}

	//Returns the blueprint node function look at title
	protected override string GetTitle()
	{
		//Return the blueprint node function llok at title
		return "LookAt";
	}

	//Renders the blueprint node function look at body components
	protected override void RenderBodyComponents()
	{
		//Render the Transform target label
		BeginSection(1);
			GUILayout.Label("Transform : target", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}