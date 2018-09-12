using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionInstantiate_GameObject : BlueprintNodeFunction
{
	//Private attributes
	private GameObject gameObject = null;

	//Executes the blueprint node function instantiate instance
	public override void Execute()
	{
		//If the dependent input attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Instantiate input gameobject attribute
			gameObject = GameObject.Instantiate((GameObject)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute());
		}

		//Perform base execution
		base.Execute();
	}

	//Returns the blueprint node gameobject instantiate output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return gameObject;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node function instantiate instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node function instantiate
		connections.Add(new BlueprintConnectionAttributeInputGameObject());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeOutputGameObject());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node function instantiate title
	protected override string GetTitle()
	{
		//Return the blueprint node function instantiate title
		return "Instantiate";
	}

	//Renders the blueprint node function instantiate body components
	protected override void RenderBodyComponents()
	{
		//Renders the GameObject object label
		BeginSection(1);
			GUILayout.Label("GameObject : object", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}