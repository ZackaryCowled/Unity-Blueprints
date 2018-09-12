using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetRotation_TransformVector3 : BlueprintNodeSet
{
	//Executes the blueprint node set set rotation instance
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
				//Set the rotation of the transform input attribute to the vector3 rotation input attribute
				transformInputAttribute.rotation = Quaternion.Euler((Vector3)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute());
			}
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set rotation instance
	public override void Initialize()
	{
		//Initialize the blueprint node set set rotation
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);
		connections.Add(new BlueprintConnectionAttributeInputTransform());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
	}

	//Returns the blueprint node set set rotation title
	protected override string GetTitle()
	{
		//Return the blueprint node set set rotation title
		return "SetRotation";
	}

	//Renders the blueprint node set set rotation body components
	protected override void RenderBodyComponents()
	{
		//Render the Transform transform label
		BeginSection(1);
			GUILayout.Label("Transform : transform", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the Vector3 rotation label
		BeginSection(2);
			GUILayout.Label("Vector3 : rotation", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}