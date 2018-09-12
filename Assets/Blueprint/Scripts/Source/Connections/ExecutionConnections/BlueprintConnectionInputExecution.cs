using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintConnectionInputExecution : BlueprintConnection
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns the blueprint connection input execution color
	protected override Color GetBlueprintConnectionBoxColor()
	{
		//Return the blueprint connection input execution color
		return new Color(0.0f, 1.0f, 0.0f);
	}

	//Returns a flag indicating whether the specified blueprint connection can be connected to this blueprint connection
	protected override bool IsConnectable(BlueprintConnection blueprintConnection)
	{
		//If the specified blueprint connection is of type blueprint connection output execution
		if(blueprintConnection as BlueprintConnectionOutputExecution != null)
		{
			//The specified blueprint connection can be connected to this blueprint connection
			return true;
		}

		//The specified blueprint connection can not be connected to this blueprint connection
		return false;
	}
#endif
}