using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeConnectionBlueprintOneToMany : BlueprintNodeConnection
{
	//Returns the blueprint node connection blueprint one to many default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node connection blueprint one to many default output attribute
		return -1;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node connection blueprint one to many instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node connection blueprint one to many
		connections.Add(new BlueprintConnectionAttributeInputBlueprint());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		AddOutputConnection<BlueprintConnectionAttributeOutputBlueprint>();
	}

	//Returns the blueprint node connection blueprint one to many title
	protected override string GetTitle()
	{
		//Return the blueprint node connection blueprint one to many title
		return "BlueprintOneToMany";
	}

	//Renders the blueprint node connection blueprint one to many body components
	protected override void RenderBodyComponents()
	{
		//Render the blueprint noce connection blueprint one to many body components for a blueprint connection attribute output blueprint
		RenderBodyComponentsOneToMany<BlueprintConnectionAttributeOutputBlueprint>();
	}
#endif
}