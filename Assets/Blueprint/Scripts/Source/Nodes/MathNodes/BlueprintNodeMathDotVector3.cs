using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeMathDotVector3 : BlueprintNodeMath
{
	//Returns the blueprint node math dot vector3 output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Return output attribute
			return Vector3.Dot((Vector3)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute(),
							   (Vector3)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute());
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node math dot vector3 default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node math dot vector3 default output attribute
		return new Vector3(0.0f, 0.0f, 0.0f);
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node math dot vector3 instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node math dot vector3
		connections.Add(new BlueprintConnectionAttributeInputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeOutputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node math dot vector3 title
	protected override string GetTitle()
	{
		//Return the blueprint node math dot vector3 title
		return "DotVector3";
	}

	//Renders the blueprint node math dot vector3 body components
	protected override void RenderBodyComponents()
	{
		//Render the lhs vector3 label
		BeginSection(1);
			GUILayout.Label("Vector3 : lhs");
		EndSection();

		//Render the rhs vector3 label
		BeginSection(2);
			GUILayout.Label("Vector3 : rhs");
		EndSection();
	}
#endif
}