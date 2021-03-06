﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionTranslate_Vector3 : BlueprintNodeFunction
{
	//Executes the blueprint node function translate instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Translate the blueprints parent GameObject by the Vector3 input attribute translation
			BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.Translate((Vector3)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute());
		}

		//Perform base execution
		base.Execute();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node function translate instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node function translate
		connections.Add(new BlueprintConnectionAttributeInputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
	}

	//Returns the blueprint node function translate title
	protected override string GetTitle()
	{
		//Return the blueprint node function translate title
		return "Translate";
	}

	//Renders the blueprint node function translate body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector3 translation label
		BeginSection(1);
			GUILayout.Label("Vector3 : translation", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}