using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventStart : BlueprintNodeEvent
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns the blueprint node event starts title
	protected override string GetTitle()
	{
		//Return the blueprint node event starts title
		return "Start";
	}
#endif
}