using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeAttribute : BlueprintNode
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns the blueprint node attribute header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node attribute header color
		return new Color(0.5f, 0.5f, 1.0f);
	}
#endif
}