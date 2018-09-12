using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlueprintComparisonHelper
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns a flag indicating whether the specified blueprints match
	public static bool BlueprintsMatch(Blueprint blueprintA, Blueprint blueprintB)
	{
		//If both blueprints do not have the same number of blueprint nodes
		if (blueprintA.GetBlueprintNodeCount() != blueprintB.GetBlueprintNodeCount())
		{
			//The blueprints do not match
			return false;
		}

		//For each blueprint node in the blueprints
		for (int blueprintNodeIndex = 0; blueprintNodeIndex < blueprintA.GetBlueprintNodeCount(); blueprintNodeIndex++)
		{
			//If the blueprint nodes do not match
			if (!BlueprintNodesMatch(blueprintA.GetBlueprintNodeAt(blueprintNodeIndex), blueprintB.GetBlueprintNodeAt(blueprintNodeIndex)))
			{
				//The blueprints do not match
				return false;
			}
		}

		//The blueprints match
		return true;
	}

	//Returns a flag indicating whether the specified blueprint nodes match
	public static bool BlueprintNodesMatch(BlueprintNode blueprintNodeA, BlueprintNode blueprintNodeB)
	{
		//If the blueprint nodes are not the same type
		if(blueprintNodeA.GetType() != blueprintNodeB.GetType())
		{
			//The blueprint nodes do not match
			return false;
		}

		//If the blueprint nodes input execution connections do not match
		if(!BlueprintConnectionsMatch(blueprintNodeA.inputExecutionConnection, blueprintNodeB.inputExecutionConnection))
		{
			//The blueprint nodes do not match
			return false;
		}

		//If the blueprint nodes output execution connections do not match
		if(!BlueprintConnectionsMatch(blueprintNodeA.outputExecutionConnection, blueprintNodeB.outputExecutionConnection))
		{
			//The blueprint nodes do not match
			return false;
		}

		//If the blueprint nodes do not have the same number of blueprint connections
		if(blueprintNodeA.connections.Count != blueprintNodeB.connections.Count)
		{
			//The blueprint nodes do not match
			return false;
		}

		//For each blueprint connection in the blueprint nodes
		for(int blueprintConnectionIndex = 0; blueprintConnectionIndex < blueprintNodeA.connections.Count; blueprintConnectionIndex++)
		{
			//If the blueprint connections do not match
			if(!BlueprintConnectionsMatch(blueprintNodeA.connections[blueprintConnectionIndex], blueprintNodeB.connections[blueprintConnectionIndex]))
			{
				//The blueprint nodes do not match
				return false;
			}
		}

		//The blueprint nodes match
		return true;
	}

	//Returns a flag indicating whether the specified blueprint connections match
	public static bool BlueprintConnectionsMatch(BlueprintConnection blueprintConnectionA, BlueprintConnection blueprintConnectionB)
	{
		//If the blueprint connections are not matching being invalid or valid
		if ((blueprintConnectionA != null) != (blueprintConnectionB != null))
		{
			//The blueprint connections do not match
			return false;
		}

		//If the blueprint connections are valid
		if (blueprintConnectionA != null)
		{
			//If the blueprint connections are not the same type
			if (blueprintConnectionA.GetType() != blueprintConnectionB.GetType())
			{
				//The blueprint connections do not match
				return false;
			}

			//If the blueprint connections do not match
			if (blueprintConnectionA.nodeID != blueprintConnectionB.nodeID ||
			    blueprintConnectionA.sectionID != blueprintConnectionB.sectionID ||
				blueprintConnectionA.isInput != blueprintConnectionB.isInput ||
				blueprintConnectionA.connectionNodeID != blueprintConnectionB.connectionNodeID ||
				blueprintConnectionA.connectionSectionID != blueprintConnectionB.connectionSectionID ||
				blueprintConnectionA.connectionIsInput != blueprintConnectionB.connectionIsInput)
			{
				//The blueprint connections do not match
				return false;
			}
		}

		//The blueprint connections match
		return true;
	}
#endif
}