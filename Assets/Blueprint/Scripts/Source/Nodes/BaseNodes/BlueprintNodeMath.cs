using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMath : BlueprintNode
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns the blueprint node math header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node math header color
		return new Color(0.0f, 1.0f, 1.0f);
	}
#endif
}