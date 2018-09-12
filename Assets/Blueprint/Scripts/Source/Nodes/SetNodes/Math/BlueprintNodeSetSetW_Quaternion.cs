using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetW_Quaternion : BlueprintNodeSet
{
	//Executes the blueprint node set set w instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Create a copy of the input attribute
			Quaternion inputAttribute = (Quaternion)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();

			//Set the w coordinate of the input attribute to the float input attribute
			inputAttribute.x = (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute();

			//Set the input attribute to the modified input attribute
			BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).SetAttribute(inputAttribute);
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set w instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set w
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
	}

	//Returns the blueprint node set set w title
	protected override string GetTitle()
	{
		//Return the blueprint node set set w title
		return "SetW";
	}

	//Renders the blueprint node set set w body components
	protected override void RenderBodyComponents()
	{
		//Render the quaternion label
		BeginSection(1);
			GUILayout.Label("Quaternion : quaternion");
		EndSection();

		//Render the float value label
		BeginSection(2);
			GUILayout.Label("Float : value");
		EndSection();
	}
#endif
}