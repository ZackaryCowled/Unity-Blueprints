using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintConnectionInvisible : BlueprintConnection
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint connection
	public override void Initialize(int blueprintID, int nodeID, int sectionID, bool isInput)
	{
		//Perform base initialization
		base.Initialize(blueprintID, nodeID, sectionID, isInput);

		//Initialize the blueprint connection invisible
		isVisible = false;
	}
#endif
}