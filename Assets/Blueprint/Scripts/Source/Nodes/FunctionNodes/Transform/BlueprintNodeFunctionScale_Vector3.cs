using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionScale_Vector3 : BlueprintNodeFunction
{
	//Executes the blueprint node function scale instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Create reference to input attributes
			GameObject gameObject = BlueprintInstanceManager.GetBlueprintParentAt(blueprintID);
			Vector3 scale = (Vector3)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();

			//Scale the blueprints parent GameObject by the Vector3 input attribute
			gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * scale.x, gameObject.transform.localScale.y * scale.y, gameObject.transform.localScale.z * scale.z);
		}

		//Perform base execution
		base.Execute();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node function scale instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node function scale
		connections.Add(new BlueprintConnectionAttributeInputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
	}

	//Returns the blueprint node transform scale title
	protected override string GetTitle()
	{
		//Return the blueprint node function scale title
		return "Scale";
	}

	//Renders the blueprint node function scale body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector3 scale label
		BeginSection(1);
			GUILayout.Label("Vector3 : scale", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}