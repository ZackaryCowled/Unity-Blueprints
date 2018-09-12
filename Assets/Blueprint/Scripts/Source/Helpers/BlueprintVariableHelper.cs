using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlueprintVariableHelper
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Restores applicable variables from blueprint A into blueprint B
	public static void RestoreVariables(Blueprint blueprintA, ref Blueprint blueprintB)
	{
		//For each blueprint node in blueprint A
		for(int blueprintNodeIndexA = 0; blueprintNodeIndexA < blueprintA.GetBlueprintNodeCount(); blueprintNodeIndexA++)
		{
			//If the blueprint node is a variable
			if(blueprintA.GetBlueprintNodeAt(blueprintNodeIndexA) as BlueprintNodeVariable != null)
			{
				//For each blueprint node in blueprint B
				for(int blueprintNodeIndexB = 0; blueprintNodeIndexB < blueprintB.GetBlueprintNodeCount(); blueprintNodeIndexB++)
				{
					//If the blueprintB node is the same type as the variable node from blueprintA
					if(blueprintB.GetBlueprintNodeAt(blueprintNodeIndexB).GetType() == blueprintA.GetBlueprintNodeAt(blueprintNodeIndexA).GetType())
					{
						//If the blueprintA node and blueprintB node have matching variable names
						if(((BlueprintNodeVariable)blueprintA.GetBlueprintNodeAt(blueprintNodeIndexA)).variableName == ((BlueprintNodeVariable)blueprintB.GetBlueprintNodeAt(blueprintNodeIndexB)).variableName)
						{
							//Set blueprintB nodes output attribute value to blueprintA nodes output attribute value
							blueprintB.GetBlueprintNodeAt(blueprintNodeIndexB).SetAttribute(blueprintA.GetBlueprintNodeAt(blueprintNodeIndexA).GetAttribute());
							break;
						}
					}
				}
			}
		}
	}
#endif
}