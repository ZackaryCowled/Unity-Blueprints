using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintNodeConnectionStringOneToMany : BlueprintNodeConnection
{
	//Returns the blueprint node connection string one to many default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node connection string one to many default output attribute
		return "";
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node connection string one to many instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node connection string one to many
		connections.Add(new BlueprintConnectionAttributeInputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		AddOutputConnection<BlueprintConnectionAttributeOutputString>();
	}

	//Returns the blueprint node connection string one to many title
	protected override string GetTitle()
	{
		//Return the blueprint node connection string one to many title
		return "StringOneToMany";
	}

	//Renders the blueprint node connection string one to many body components
	protected override void RenderBodyComponents()
	{
		//Render the blueprint node connection string one to many body components for a blueprint connection attribute output string
		RenderBodyComponentsOneToMany<BlueprintConnectionAttributeOutputString>();
	}
#endif
}