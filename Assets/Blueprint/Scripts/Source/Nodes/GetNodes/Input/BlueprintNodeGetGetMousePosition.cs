using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetMousePosition : BlueprintNodeGet
{
	//Returns the blueprint node get get mouse position output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get mouse position instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get mouse position position
		connections.Add(new BlueprintConnectionAttributeOutputVector2());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get mouse position title
	protected override string GetTitle()
	{
		//Return the blueprint node get get mouse position title
		return "GetMousePosition";
	}

	//Return the blueprint node get get mouse position body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector2 mousePosition label
		BeginSection(1);
			GUILayout.Label("Vector2 : position", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}