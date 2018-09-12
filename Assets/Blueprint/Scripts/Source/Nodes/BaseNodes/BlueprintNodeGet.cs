using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGet : BlueprintNode
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns the blueprint node get header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node get header color
		return new Color(0.0f, 1.0f, 0.75f);
	}
#endif
}