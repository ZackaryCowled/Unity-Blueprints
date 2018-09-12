using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionRotate_TransformQuaternion : BlueprintNodeFunction
{
	//Executes the blueprint node function rotate instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Create reference to the transform input attribute
			Transform transformInputAttribute = BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute() as Transform;

			//If the transform input attribute is valid
			if (transformInputAttribute != null)
			{
				//Rotate the transform input attribute by the Quaternion input attribute rotation
				transformInputAttribute.rotation *= (Quaternion)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute();
			}
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
		connections.Add(new BlueprintConnectionAttributeInputTransform());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
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
		//Renders the transform label
		BeginSection(1);
			GUILayout.Label("Transform : transform", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Renders the Quaternion rotation label
		BeginSection(2);
			GUILayout.Label("Quaternion : rotation", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}