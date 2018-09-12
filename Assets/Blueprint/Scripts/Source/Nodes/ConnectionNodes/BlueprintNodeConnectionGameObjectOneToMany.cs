using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeConnectionGameObjectOneToMany : BlueprintNodeConnection
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node connection gameobject one to many instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node connection gameobject one to many
		connections.Add(new BlueprintConnectionAttributeInputGameObject());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		AddOutputConnection<BlueprintConnectionAttributeOutputGameObject>();
	}

	//Returns the blueprint node connection gameobject one to many title
	protected override string GetTitle()
	{
		//Return the blueprint node connection gameobject one to many title
		return "GameObjectOneToMany";
	}

	//Renders the blueprint node connection gameobject one to many body components
	protected override void RenderBodyComponents()
	{
		//Render the blueprint node connection gameobject one to many body components for a blueprint connection attribute output gameobject
		RenderBodyComponentsOneToMany<BlueprintConnectionAttributeOutputGameObject>();
	}
#endif
}