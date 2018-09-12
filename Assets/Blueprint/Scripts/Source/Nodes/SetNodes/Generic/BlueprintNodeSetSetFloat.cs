using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetFloat : BlueprintNodeSet
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set float instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set float
		AddInputConnections<BlueprintConnectionAttributeInputFloat>();
	}

	//Returns the blueprint node set set float title
	protected override string GetTitle()
	{
		//Return the blueprint node set set float title
		return "SetFloat";
	}

	//Returns the blueprint node set set float type name
	protected override string GetTypeName()
	{
		//Return the blueprint node set set float type name
		return "Float";
	}
#endif
}