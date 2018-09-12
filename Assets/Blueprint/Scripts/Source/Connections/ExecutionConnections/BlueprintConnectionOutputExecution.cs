using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintConnectionOutputExecution : BlueprintConnection
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Returns the blueprint connection output execution color
	protected override Color GetBlueprintConnectionBoxColor()
	{
		//Return the blueprint connection output execution color
		return new Color(1.0f, 0.0f, 0.0f);
	}

	//Returns a flag indicating whether the specified blueprint connection can be connected to this blueprint connection
	protected override bool IsConnectable(BlueprintConnection blueprintConnection)
	{
		//If the specified blueprint connection is of type blueprint connection input execution
		if(blueprintConnection as BlueprintConnectionInputExecution != null)
		{
			//The specified blueprint connection can be connected to this blueprint connection
			return true;
		}

		//The specified blueprint connection can not be connected to this blueprint connection
		return false;
	}
#endif
}