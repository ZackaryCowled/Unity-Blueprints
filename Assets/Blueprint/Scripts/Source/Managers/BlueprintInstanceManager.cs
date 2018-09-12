using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlueprintInstanceManager
{
	//Private attributes
	private static List<Blueprint> blueprints = new List<Blueprint>();
	private static List<GameObject> parents = new List<GameObject>();
	private static List<int> nullIDs = new List<int>();

	//Creates a blueprint instance from the specified blueprint JSON string and returns a unique ID for the instance
	public static int CreateBlueprint(string blueprintFilepath, string blueprintJSON, GameObject parent)
	{
		//If the blueprint JSON is valid
		if (blueprintJSON != "")
		{
			//Create intermediary attributes
			int blueprintID = 0;

			//If there are any null ID's
			if (nullIDs.Count > 0)
			{
				//Re-use ID and remove it from the null ID's list
				blueprintID = nullIDs[0];
				nullIDs.Remove(0);

				//Add blueprint
				blueprints[blueprintID] = new Blueprint();

				//Add parent
				parents[blueprintID] = parent;
			}
			//Otherwise
			else
			{
				//Generate new blueprint ID
				blueprintID = blueprints.Count;

				//Add blueprint
				blueprints.Add(new Blueprint());

				//Add parent
				parents.Add(parent);
			}

			//Load the blueprint
			blueprints[blueprintID].LoadFromResources(blueprintFilepath);

			//Initialize temporary blueprint
			Blueprint tempBlueprint = new Blueprint();
			tempBlueprint.Load(blueprintJSON);

			//For each temporary blueprint node
			for (int tempNodeIndex = 0; tempNodeIndex < tempBlueprint.GetBlueprintNodeCount(); tempNodeIndex++)
			{
				//If the temporary blueprint node is a variable
				if (tempBlueprint.GetBlueprintNodeAt(tempNodeIndex) as BlueprintNodeVariable != null)
				{
					//For each blueprint node in the blueprint
					for (int nodeIndex = 0; nodeIndex < blueprints[blueprintID].GetBlueprintNodeCount(); nodeIndex++)
					{
						//If the blueprint node is the same type as the variable node from temporary blueprint
						if (blueprints[blueprintID].GetBlueprintNodeAt(nodeIndex).GetType() == tempBlueprint.GetBlueprintNodeAt(tempNodeIndex).GetType())
						{
							//If the temporary blueprint node and the blueprint node have matching variable names
							if (((BlueprintNodeVariable)tempBlueprint.GetBlueprintNodeAt(tempNodeIndex)).variableName == ((BlueprintNodeVariable)blueprints[blueprintID].GetBlueprintNodeAt(nodeIndex)).variableName)
							{
								//Set the blueprint nodes output attribute value to the temporary nodes output attribute value
								blueprints[blueprintID].GetBlueprintNodeAt(nodeIndex).SetAttribute(tempBlueprint.GetBlueprintNodeAt(tempNodeIndex).GetAttribute());
								break;
							}
						}
					}
				}
			}

			//Set the blueprints unique ID
			blueprints[blueprintID].SetID(blueprintID);

			//For each of the blueprints blueprint nodes
			for (int nodeIndex = 0; nodeIndex < blueprints[blueprintID].GetBlueprintNodeCount(); nodeIndex++)
			{
				//Runtime initialize the blueprints blueprint node
				blueprints[blueprintID].GetBlueprintNodeAt(nodeIndex).RuntimeInitialize();
			}

			//Return a unique ID for the blueprint instance
			return blueprintID;
		}
		//Otherwise
		else
		{
			//Return a invalid blueprint ID
			return -1;
		}
	}

	//Removes the blueprint instance with the specified ID
	public static void RemoveBlueprint(int id)
	{
		//For each node in the specified blueprint
		for(int nodeIndex = 0; nodeIndex < blueprints[id].GetBlueprintNodeCount(); nodeIndex++)
		{
			//If the blueprint node is a blueprint node blueprint
			if(blueprints[id].GetBlueprintNodeAt(nodeIndex) as BlueprintNodeBlueprint != null)
			{
				//Remove the blueprint
				RemoveBlueprint(((BlueprintNodeBlueprint)blueprints[id].GetBlueprintNodeAt(nodeIndex)).GetCustomBlueprintID());
			}
		}

		//Remove the blueprint instance with the specified ID
		blueprints[id] = null;

		//Add the specified ID to the null ID's list
		nullIDs.Add(id);
	}

	//Returns the blueprint at the specified index
	public static Blueprint GetBlueprintAt(int index)
	{
		//Return the blueprint at the specified index
		return blueprints[index];
	}

	//Returns the number of blueprint instances
	public static int GetBlueprintCount()
	{
		//Return the number of blueprint instances
		return blueprints.Count;
	}

	//Returns the blueprint parent at the specified index
	public static GameObject GetBlueprintParentAt(int index)
	{
		//Return the blueprint parent at the specified index
		return parents[index];
	}

	//Returns the number of blueprint parents
	public static int GetBlueprintParentCount()
	{
		//Return the number of blueprint parents
		return parents.Count;
	}
}