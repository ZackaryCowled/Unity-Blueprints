using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeConnectionVariableComponentOneToMany<T1, T2> : BlueprintNodeConnection where T1 : BlueprintConnectionAttribute, new() where T2 : BlueprintConnectionAttribute, new()
{
	//Public attributes
	public string componentName = "";

	//Sets the blueprint node connection variable component one to many component name
	public void SetComponentName(string name)
	{
		//Set the blueprint node connection variable component one to many component name
		componentName = name;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node connection variable component one to many instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node connection variable component one to many
		connections.Add(new T1());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		AddOutputConnection<T2>();
	}

	//Returns the blueprint node connection variable component one to many title
	protected override string GetTitle()
	{
		//Return the blueprint node connection variable component one to many title
		return componentName + "OneToMany";
	}

	//Renders the blueprint node connection variable component one to many body components
	protected override void RenderBodyComponents()
	{
		//Renders the blueprint node connection variable component one to many body components for a blueprint connection attribute output
		RenderBodyComponentsOneToMany<T2>();
	}
#endif
}