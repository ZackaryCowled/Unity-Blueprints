using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventGetAnyKey : BlueprintNodeEventCondition
{
	//Returns a flag indicating whether the blueprint node event get any key condition is being met
	public override bool IsConditionMet()
	{
		//If any key is being pressed
		if (Input.anyKey)
		{
			//The condition is being met
			return true;
		}

		//The condition is not being met
		return false;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns the blueprint node event get any key title
	protected override string GetTitle()
	{
		//Return the blueprint node event get any key title
		return "GetAnyKey";
	}
#endif
}