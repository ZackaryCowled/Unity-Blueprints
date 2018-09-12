using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintManager : MonoBehaviour
{
	//Public attributes
	public string blueprintFilepath = "";
	public string blueprintJSON = "";

	//Private attributes
	private int blueprintID = -1;

	//Returns the blueprint managers blueprint ID
	public int GetBlueprintID()
	{
		//Return the blueprint managers blueprint ID
		return blueprintID;
	}

	//Called on initialization
	private void Awake()
	{
		//Create blueprint
		blueprintID = BlueprintInstanceManager.CreateBlueprint(blueprintFilepath, blueprintJSON, gameObject);

		//TODO: Execute all awake events
		return;
	}

	//Called on start
	private void Start()
	{
		//If the blueprint is valid
		if (blueprintID > -1)
		{
			//For each blueprint node
			for (int nodeIndex = 0; nodeIndex < BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeCount(); nodeIndex++)
			{
				//Start initialize the blueprint node
				BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(nodeIndex).StartInitialize();
			}

			//For each blueprint node
			for (int nodeIndex = 0; nodeIndex < BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeCount(); nodeIndex++)
			{
				//If the blueprint node is a event start node
				if (BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(nodeIndex) as BlueprintNodeEventStart != null)
				{
					//Execute the blueprint node
					BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(nodeIndex).Execute();
				}
			}
		}
	}

	//Called once per frame
	private void Update()
	{
		//If the blueprint is valid
		if (blueprintID > -1)
		{
			//For each blueprint node
			for (int nodeIndex = 0; nodeIndex < BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeCount(); nodeIndex++)
			{
				//If the blueprint node is a event condition node
				if (BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(nodeIndex) as BlueprintNodeEventCondition != null)
				{
					//If the blueprint nodes condition is being met
					if (((BlueprintNodeEventCondition)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(nodeIndex)).IsConditionMet())
					{
						//Execute the blueprint node
						BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(nodeIndex).Execute();
					}
				}
			}

			//For each blueprint node
			for (int nodeIndex = 0; nodeIndex < BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeCount(); nodeIndex++)
			{
				//If the blueprint node is a event update node
				if (BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(nodeIndex) as BlueprintNodeEventUpdate != null)
				{
					//Execute the blueprint node
					BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(nodeIndex).Execute();
				}
			}
		}
	}

	//Called once per physics frame
	private void FixedUpdate()
	{
		//TODO: Execute all fixed udpate events
	}

	//Called on blueprint destruction
	private void OnDestroy()
	{
		//If the blueprint is valid
		if (blueprintID > -1)
		{
			//TODO: Execute all OnDestroy events

			//Remove the blueprint from the blueprint instance manager
			BlueprintInstanceManager.RemoveBlueprint(blueprintID);
		}
	}
}