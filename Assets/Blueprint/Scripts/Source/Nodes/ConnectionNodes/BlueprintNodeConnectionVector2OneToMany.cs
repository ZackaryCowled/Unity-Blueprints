using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeConnectionVector2OneToMany : BlueprintNodeConnection
{
	//Returns the blueprint node connection vector2 one to many default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node connection vector2 one to many default output attribute
		return new Vector2(0.0f, 0.0f);
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node connection vector2 one to many instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node connection vector2 one to many
		connections.Add(new BlueprintConnectionAttributeInputVector2());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		AddOutputConnection<BlueprintConnectionAttributeOutputVector2>();
	}

	//Returns the blueprint node connection vector2 one to many title
	protected override string GetTitle()
	{
		//Return the blueprint node connection vector2 one to many title
		return "Vector2OneToMany";
	}

	//Renders the blueprint node connection vector2 one to many body components
	protected override void RenderBodyComponents()
	{
		//Render the blueprint node connection vector2 one to manu body components for a blueprint connection attribute output vector2
		RenderBodyComponentsOneToMany<BlueprintConnectionAttributeOutputVector2>();
	}
#endif
}