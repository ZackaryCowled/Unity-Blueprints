using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventCondition : BlueprintNodeEvent
{
	//Returns a flag indicating whether the blueprint event condition nodes condition is being met
	public virtual bool IsConditionMet()
	{
		//Return default condition result
		return false;
	}
}