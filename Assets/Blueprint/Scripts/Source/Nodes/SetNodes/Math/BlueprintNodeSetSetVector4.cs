using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetVector4 : BlueprintNodeSet
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set vector4 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set vector4
		AddInputConnections<BlueprintConnectionAttributeInputVector4>();
	}

	//Returns the blueprint node set set vector4 title
	protected override string GetTitle()
	{
		//Return the blueprint node set set vector4 title
		return "SetVector4";
	}

	//Returns the blueprint node set set vector4 type name
	protected override string GetTypeName()
	{
		//Return the blueprint node set set vector4 type name
		return "Vector4";
	}
#endif
}