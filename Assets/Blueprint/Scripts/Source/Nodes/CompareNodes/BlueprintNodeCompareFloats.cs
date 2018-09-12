using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeCompareFloats : BlueprintNodeCompare
{
	//Executes the blueprint node compare float instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[2].connectionNodeID > -1 && connections[3].connectionNodeID > -1)
		{
			//Depending on the mode
			switch (mode)
			{
				//a > b
				case 0:
				{
					//Set the result to the condition result of if attribute a is more than attribute b
					result = (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute() > (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[3].connectionNodeID).GetAttribute();
					break;
				}

				//a < b
				case 1:
				{
					//Set the result to the condition result of if attribute a is less than attribute b
					result = (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute() < (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[3].connectionNodeID).GetAttribute();
					break;
				}

				//a == b
				case 2:
				{
					//Set the result to the condition result of if attribute a is equal to attribute b
					result = (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute() == (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[3].connectionNodeID).GetAttribute();
					break;
				}

				//a >= b
				case 3:
				{
					//Set the result to the condition result of if attribute a is more or equal to attribute b
					result = (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute() >= (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[3].connectionNodeID).GetAttribute();
					break;
				}

				//a <= b
				case 4:
				{
					//Set the result to the condition result of if attribute a is less or equal to attribute b
					result = (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute() <= (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[3].connectionNodeID).GetAttribute();
					break;
				}

				//a != b
				case 5:
				{
					//Set the result to the condition result of if attribute a is not equal to attribute b
					result = (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute() != (float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[3].connectionNodeID).GetAttribute();
					break;
				}
			}
		}

		//Perform base execution
		base.Execute();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node compare floats instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node compare floats
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 4, true);
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 5, true);
	}

	//Returns the blueprint node compare floats title
	protected override string GetTitle()
	{
		//Return the blueprint node compare floats title
		return "CompareFloats";
	}

	//Returns the blueprint node compare floats type name
	protected override string GetTypeName()
	{
		//Return the blueprint node compare floats type name
		return "Float";
	}
#endif
}