using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventGetAnyKeyDown : BlueprintNodeEventCondition
{
	//Returns a flag indicating whether the blueprint node event get any key down condition is being met
	public override bool IsConditionMet()
	{
		//If any key is being pressed down this frame
		if (Input.anyKeyDown)
		{
			//The condition is being met
			return true;
		}

		//The condition is not being met
		return false;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns the blueprint node event get any key down title
	protected override string GetTitle()
	{
		//Return the blueprint node event get any key down title
		return "GetAnyKeyDown";
	}
#endif
}