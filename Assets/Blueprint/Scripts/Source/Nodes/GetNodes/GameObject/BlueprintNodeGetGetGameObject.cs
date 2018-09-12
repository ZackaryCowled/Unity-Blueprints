using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintNodeGetGetGameObject : BlueprintNodeGet
{
	//Returns the blueprint node get get gameobject output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return BlueprintInstanceManager.GetBlueprintParentAt(blueprintID);
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get gameobject instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get gameobject
		connections.Add(new BlueprintConnectionAttributeOutputGameObject());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get gameobject title
	protected override string GetTitle()
	{
		//Return the blueprint node get get gameobject title
		return "GetGameObject";
	}

	//Renders the blueprint node get get gameobject body components
	protected override void RenderBodyComponents()
	{
		//Render the gameobject label
		BeginSection(1);
			GUILayout.Label("GameObject : object", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}