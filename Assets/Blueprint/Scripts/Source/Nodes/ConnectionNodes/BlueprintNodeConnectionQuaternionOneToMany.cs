using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeConnectionQuaternionOneToMany : BlueprintNodeConnection
{
	//Returns the blueprint node connection Quaternion one to many default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node connection Quaternion one to many default output attribute
		return Quaternion.identity;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node connection Quaternion one to many instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node connection Quaternion one to many
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		AddOutputConnection<BlueprintConnectionAttributeOutputQuaternion>();
	}

	//Returns the blueprint node connection Quaternion one to many title
	protected override string GetTitle()
	{
		//Return the blueprint node connection Quaternion one to many title
		return "QuaternionOneToMany";
	}

	//Renders the blueprint node connection Quaternion one to many body components
	protected override void RenderBodyComponents()
	{
		//Render the blueprint node connection Quaternion one to many body components for a blueprint connection output int
		RenderBodyComponentsOneToMany<BlueprintConnectionAttributeOutputQuaternion>();
	}
#endif
}