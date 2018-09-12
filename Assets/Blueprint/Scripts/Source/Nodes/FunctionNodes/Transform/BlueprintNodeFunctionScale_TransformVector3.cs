using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionScale_TransformVector3 : BlueprintNodeFunction
{
	//Executes the blueprint node function scale instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Create reference to input attributes
			Transform transformInputAttribute = BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute() as Transform;
			Vector3 scale = (Vector3)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute();

			//If the transform input attribute is valid
			if (transformInputAttribute != null)
			{
				//Scale the transform input attribute by the vector3 input attribute translation
				transformInputAttribute.localScale = new Vector3(transformInputAttribute.localScale.x * scale.x, transformInputAttribute.localScale.y * scale.y, transformInputAttribute.localScale.z * scale.z);
			}
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
		connections.Add(new BlueprintConnectionAttributeInputTransform());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
	}

	//Returns the blueprint node function scale title
	protected override string GetTitle()
	{
		//Return the blueprint node function scale title
		return "Scale";
	}

	//Renders the blueprint node function scale body components
	protected override void RenderBodyComponents()
	{
		//Renders the transform label
		BeginSection(1);
			GUILayout.Label("Transform : transform", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Renders the Vector3 scale label
		BeginSection(2);
			GUILayout.Label("Vector3 : scale", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}