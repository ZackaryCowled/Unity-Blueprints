using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetY_Vector4 : BlueprintNodeSet
{
	//Executes the blueprint node set set y instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Create a copy of the input attribute
			Vector4 inputAttribute = (Vector4)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();

			//Set the y coordinate of the input attribute to the float input attribute
			inputAttribute.y = (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute();

			//Set the input attribute to the modified input attribute
			BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).SetAttribute(inputAttribute);
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set y instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set y
		connections.Add(new BlueprintConnectionAttributeInputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
	}

	//Returns the blueprint node set set y title
	protected override string GetTitle()
	{
		//Return the blueprint node set set y title
		return "SetY";
	}

	//Renders the blueprint node set set x body components
	protected override void RenderBodyComponents()
	{
		//Render the vector label
		BeginSection(1);
			GUILayout.Label("Vector4 : vector");
		EndSection();

		//Render the float value label
		BeginSection(2);
			GUILayout.Label("Float : value");
		EndSection();
	}
#endif
}