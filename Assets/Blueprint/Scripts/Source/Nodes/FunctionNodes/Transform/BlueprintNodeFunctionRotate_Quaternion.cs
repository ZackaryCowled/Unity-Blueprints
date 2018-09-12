using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionRotate_Quaternion : BlueprintNodeFunction
{
	//Executes the blueprint node function rotate instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Rotate the blueprints parents GameObject by the Vector3 input attribute rotation
			BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.rotation *= (Quaternion)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();
		}

		//Perform base execution
		base.Execute();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node function rotate instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node function rotate
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
	}

	//Returns the blueprint node function rotate title
	protected override string GetTitle()
	{
		//Return the blueprint node function rotate title
		return "Rotate";
	}

	//Renders the blueprint node function rotate body components
	protected override void RenderBodyComponents()
	{
		//Render the Quaternion rotation label
		BeginSection(1);
			GUILayout.Label("Quaternion : rotation", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}