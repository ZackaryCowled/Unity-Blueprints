using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetBlueprint : BlueprintNodeSet
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set blueprint instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set blueprint
		AddInputConnections<BlueprintConnectionAttributeInputBlueprint>();
	}

	//Returns the blueprint node set set blueprint title
	protected override string GetTitle()
	{
		//Return the blueprint node set set blueprint title
		return "SetBlueprint";
	}

	//Returns the blueprint node set set blueprint type name
	protected override string GetTypeName()
	{
		//Return the blueprint node set set blueprint type name
		return "Blueprint";
	}
#endif
}