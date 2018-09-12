using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMathAcos : BlueprintNodeMath
{
	//Returns the blueprint node math acos output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Return output attribute
			return Mathf.Acos((float)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute());
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node math acos default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node math acos default output attribute
		return 0.0f;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node math acos instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node math acos
		connections.Add(new BlueprintConnectionAttributeInputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node math acos title
	protected override string GetTitle()
	{
		//Return the blueprint node math acos title
		return "Acos";
	}

	//Renders the blueprint node math acos body components
	protected override void RenderBodyComponents()
	{
		//Render the value label
		BeginSection(1);
			GUILayout.Label("Float : value", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}