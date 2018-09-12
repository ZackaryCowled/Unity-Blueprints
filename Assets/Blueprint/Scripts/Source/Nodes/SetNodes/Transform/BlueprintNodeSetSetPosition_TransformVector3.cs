using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetPosition_TransformVector3 : BlueprintNodeSet
{
	//Executes the blueprint node set set position instance
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
				//Set the position of the transform input attribute to the vector3 position input attribute
				transformInputAttribute.position = (Vector3)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute();
			}
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set position instance
	public override void Initialize()
	{
		//Initialize the blueprint node set set position
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);
		connections.Add(new BlueprintConnectionAttributeInputTransform());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
	}

	//Returns the blueprint node set set position title
	protected override string GetTitle()
	{
		//Return the blueprint node set set position title
		return "SetPosition";
	}

	//Renders the blueprint node set set position body components
	protected override void RenderBodyComponents()
	{
		//Render the Transform transform label
		BeginSection(1);
			GUILayout.Label("Transform : transform", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the Vector3 position label
		BeginSection(2);
			GUILayout.Label("Vector3 : position", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}