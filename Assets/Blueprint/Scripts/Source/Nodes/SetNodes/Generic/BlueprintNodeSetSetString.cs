using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetString : BlueprintNodeSet
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set string instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set string
		AddInputConnections<BlueprintConnectionAttributeInputString>();
	}

	//Returns the blueprint node set set string title
	protected override string GetTitle()
	{
		//Return the blueprint node set set string title
		return "SetString";
	}

	//Returns the blueprint node set set string type name
	protected override string GetTypeName()
	{
		//Return the blueprint node set set string type name
		return "String";
	}
#endif
}