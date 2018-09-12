using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventUpdate : BlueprintNodeEvent
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns the blueprint node event updates title
	protected override string GetTitle()
	{
		//Return the blueprint node event updates title
		return "Update";
	}
#endif
}