using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetVector3 : BlueprintNodeSet
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set vector3 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set vector3
		AddInputConnections<BlueprintConnectionAttributeInputVector3>();
	}

	//Returns the blueprint node set set vector3 title
	protected override string GetTitle()
	{
		//Return the blueprint node set set vector3 title
		return "SetVector3";
	}

	//Returns the blueprint node set set vector3 type name
	protected override string GetTypeName()
	{
		//Return the blueprint node set set vector3 type name
		return "Vector3";
	}
#endif
}