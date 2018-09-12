using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeConnectionVector3OneToMany : BlueprintNodeConnection
{
	//Returns the blueprint node connection vector3 one to many default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node connection vector3 one to many default output attribute
		return new Vector3(0.0f, 0.0f, 0.0f);
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node connection vector3 one to many instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node connection vector3 one to many
		connections.Add(new BlueprintConnectionAttributeInputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		AddOutputConnection<BlueprintConnectionAttributeOutputVector3>();
	}

	//Returns the blueprint node connection vector3 one to many title
	protected override string GetTitle()
	{
		//Return the blueprint node connection vector3 one to many title
		return "Vector3OneToMany";
	}

	//Renders the blueprint node connection vector3 one to many body components
	protected override void RenderBodyComponents()
	{
		//Render the blueprint node connection vector3 one to many body components for a blueprint connection attribute output vector3
		RenderBodyComponentsOneToMany<BlueprintConnectionAttributeOutputVector3>();
	}
#endif
}