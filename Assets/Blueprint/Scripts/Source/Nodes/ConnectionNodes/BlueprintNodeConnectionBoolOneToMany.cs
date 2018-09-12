using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeConnectionBoolOneToMany : BlueprintNodeConnection
{
	//Returns the blueprint node connection bool one to many default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node connection bool one to many default output attribute
		return false;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node connection bool one to many instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node connection bool one to many
		connections.Add(new BlueprintConnectionAttributeInputBool());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		AddOutputConnection<BlueprintConnectionAttributeOutputBool>();
	}

	//Returns the blueprint node connection bool one to many title
	protected override string GetTitle()
	{
		//Return the blueprint node connection bool one to many title
		return "BoolOneToMany";
	}

	//Renders the blueprint node connection bool one to many body components
	protected override void RenderBodyComponents()
	{
		//Render the blueprint noce connection bool one to many body components for a blueprint connection attribute output bool
		RenderBodyComponentsOneToMany<BlueprintConnectionAttributeOutputBool>();
	}
#endif
}