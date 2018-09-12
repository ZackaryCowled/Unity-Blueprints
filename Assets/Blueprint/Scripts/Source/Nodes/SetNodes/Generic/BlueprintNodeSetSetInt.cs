using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetInt : BlueprintNodeSet
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set int instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set int
		AddInputConnections<BlueprintConnectionAttributeInputInt>();
	}

	//Returns the blueprint node set set int title
	protected override string GetTitle()
	{
		//Return the blueprint node set set int title
		return "SetInt";
	}

	//Returns the blueprint node set set int type name
	protected override string GetTypeName()
	{
		//Return the blueprint node set set int type name
		return "Int";
	}
#endif
}