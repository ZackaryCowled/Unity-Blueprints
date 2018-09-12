using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetVector2 : BlueprintNodeSet
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set vector2 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set vector2 instance
		AddInputConnections<BlueprintConnectionAttributeInputVector2>();
	}

	//Returns the blueprint node set set vector2 title
	protected override string GetTitle()
	{
		//Return the blueprint node set set vector2 title
		return "SetVector2";
	}

	//Returns the blueprint node set set vector2 title
	protected override string GetTypeName()
	{
		//Return the blueprint node set set type name
		return "Vector2";
	}
#endif
}