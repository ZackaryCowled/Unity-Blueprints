using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintConnectionAttributeInputInt : BlueprintConnectionAttributeInput
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns a flag indicating whether the specified blueprint connection can be connected to this blueprint connection
	protected override bool IsConnectable(BlueprintConnection blueprintConnection)
	{
		//If the specified blueprint connections is of type blueprint connection attribute output int
		if (blueprintConnection as BlueprintConnectionAttributeOutputInt != null)
		{
			//The specified blueprint connection can be connected to this blueprint connection
			return true;
		}

		//The specified blueprint connection can not be connected to this blueprint connection
		return false;
	}
#endif
}