using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeCompareStrings : BlueprintNodeCompare
{
	//Executes the blueprint node compare strings instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[2].connectionNodeID > -1 && connections[3].connectionNodeID > -1)
		{
			//Depending on the mode
			switch (mode)
			{
				//a == b
				case 0:
				{
					//Set the result to the condition result of if attribute a is equal to attribute b
					result = (string)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute() == (string)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[3].connectionNodeID).GetAttribute();
					break;
				}

				//a != b
				case 1:
				{
					//Set the result to the condition result of if attribute a is not equal to attribute b
					result = (string)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute() != (string)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[3].connectionNodeID).GetAttribute();
					break;
				}
			}
		}

		//Perform base initialization
		base.Execute();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node compare strings instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Enable lite mode
		liteMode = true;

		//Initialize the blueprint node compare strings
		connections.Add(new BlueprintConnectionAttributeInputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 4, true);
		connections.Add(new BlueprintConnectionAttributeInputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 5, true);
	}

	//Returns the blueprint node compare strings title
	protected override string GetTitle()
	{
		//Return the blueprint node compare strings title
		return "CompareStrings";
	}

	//Returns the blueprint node compare strings type name
	protected override string GetTypeName()
	{
		//Return the blueprint node compare strings type name
		return "String";
	}
#endif
}