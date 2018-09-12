using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintNodeGetGetMouseScrollDelta : BlueprintNodeGet
{
	//Returns the blueprint node get get mouse scroll delta output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return Input.mouseScrollDelta;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get mouse scroll delta instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get mouse scroll delta position
		connections.Add(new BlueprintConnectionAttributeOutputVector2());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get mouse scroll delta title
	protected override string GetTitle()
	{
		//Return the blueprint node get get mouse scroll delta title
		return "GetMouseScrollDelta";
	}

	//Return the blueprint node get get mouse scroll delta body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector2 scrollDelta label
		BeginSection(1);
			GUILayout.Label("Vector2 : scrollDelta", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}