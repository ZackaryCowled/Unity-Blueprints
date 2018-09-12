using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetBool : BlueprintNodeSet
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set bool instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set bool
		AddInputConnections<BlueprintConnectionAttributeInputBool>();
	}

	//Returns the blueprint node set set bool title
	protected override string GetTitle()
	{
		//Return the blueprint node set set bool title
		return "SetBool";
	}

	//Returns the blueprint node set set bool type name
	protected override string GetTypeName()
	{
		//Return the blueprint node set set bool type name
		return "Bool";
	}
#endif
}