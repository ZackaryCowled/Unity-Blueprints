using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetQuaternion_Quaternion : BlueprintNodeSet
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set Quaternion instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set Quaternion
		AddInputConnections<BlueprintConnectionAttributeInputQuaternion>();
	}

	//Returns the blueprint node set set Quaternion title
	protected override string GetTitle()
	{
		//Return the blueprint node set set Quaternion title
		return "SetQuaternion";
	}

	//Returns the blueprint node set set Quaternion type name
	protected override string GetTypeName()
	{
		//Return the blueprint node set set Quaternion type name
		return "Quaternion";
	}
#endif
}