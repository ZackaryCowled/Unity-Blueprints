﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintConnectionAttributeInputAnimator : BlueprintConnectionAttributeInput
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns a flag indicating whether the specified blueprint connection can be connected to this blueprint connection
	protected override bool IsConnectable(BlueprintConnection blueprintConnection)
	{
		//If the specified blueprint connection is of type blueprint connection attribute output animator
		if (blueprintConnection as BlueprintConnectionAttributeOutputAnimator != null)
		{
			//The specified blueprint connection can be connected to this blueprint connection
			return true;
		}

		//The specified blueprint connection can not be connected to this blueprint connection
		return false;
	}
#endif
}