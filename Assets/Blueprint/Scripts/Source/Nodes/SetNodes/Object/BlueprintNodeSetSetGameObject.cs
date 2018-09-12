using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetGameObject : BlueprintNodeSet
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set gameobject instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set gameobject
		AddInputConnections<BlueprintConnectionAttributeInputGameObject>();
	}

	//Returns the blueprint node set set gameobject title
	protected override string GetTitle()
	{
		//Return the blueprint node set set gameobject title
		return "SetGameObject";
	}

	//Renders the blueprint node set set gameobject type name
	protected override string GetTypeName()
	{
		//Return the blueprint node set set bool type name
		return "GameObject";
	}
#endif
}