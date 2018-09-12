using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEvent : BlueprintNode
{
	//Executes the blueprint node event instance
	public override void Execute()
	{
		//If the output execution connection connection node is valid
		if (outputExecutionConnection.connectionNodeID > -1)
		{
			//Execute the output execution node
			BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(outputExecutionConnection.connectionNodeID).Execute();
		}
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node event instance
	public override void Initialize()
	{
		//Initialize the blueprint node event
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);
	}

	//Returns the blueprint node events header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node events header color
		return new Color(0.75f, 0.0f, 0.0f);
	}
#endif
}