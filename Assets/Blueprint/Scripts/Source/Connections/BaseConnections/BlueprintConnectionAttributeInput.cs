using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintConnectionAttributeInput : BlueprintConnectionAttribute
{
	//Returns the blueprint connection attribute input color
	protected override Color GetBlueprintConnectionBoxColor()
	{
		//Returns the blueprint connection attribute input color
		return new Color(0.0f, 1.0f, 0.0f);
	}
}