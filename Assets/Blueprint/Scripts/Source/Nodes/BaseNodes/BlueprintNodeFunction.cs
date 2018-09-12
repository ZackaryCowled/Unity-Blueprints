using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunction : BlueprintNode
{
	//Executes the blueprint node function instance
	public override void Execute()
	{
		//If the output execution connection node is valid
		if (outputExecutionConnection.connectionNodeID > -1)
		{
			//Execute the output execution connection node
			BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(outputExecutionConnection.connectionNodeID).Execute();
		}
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint function instance
	public override void Initialize()
	{
		//Initialize the blueprint node function
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);
	}
	
	//Returns the blueprint node function header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node function header color
		return new Color(0.0f, 0.0f, 0.5f);
	}
#endif
}