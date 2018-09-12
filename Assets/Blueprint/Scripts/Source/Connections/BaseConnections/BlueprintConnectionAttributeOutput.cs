using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintConnectionAttributeOutput : BlueprintConnectionAttribute
{
	//Returns the blueprint connection attribute output color
	protected override Color GetBlueprintConnectionBoxColor()
	{
		//Return the blueprint connection attribute output color
		return new Color(1.0f, 0.0f, 0.0f);
	}
}