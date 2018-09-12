using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeTime : BlueprintNode
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns the blueprint node time header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node time header color
		return new Color(0.6f, 0.25f, 1.0f);
	}
#endif
}