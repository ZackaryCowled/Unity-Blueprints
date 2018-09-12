using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeConnectionVector4OneToMany : BlueprintNodeConnection
{
	//Returns the blueprint node connection vector4 one to many default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node connection vector4 one to many default output attribute
		return new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node connection vector4 one to many instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node connection vector4 one to many
		connections.Add(new BlueprintConnectionAttributeInputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		AddOutputConnection<BlueprintConnectionAttributeOutputVector4>();
	}

	//Returns the blueprint node connection vector4 one to many title
	protected override string GetTitle()
	{
		//Return the blueprint node connection vector4 one to many title
		return "Vector4OneToMany";
	}

	//Renders the blueprint node connection vector4 one to many body components
	protected override void RenderBodyComponents()
	{
		//Render the blueprint node connection vector4 one to many body components for a blueprint connection attribute output vector4
		RenderBodyComponentsOneToMany<BlueprintConnectionAttributeOutputVector4>();
	}
#endif
}