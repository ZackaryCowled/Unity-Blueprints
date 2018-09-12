using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetPosition : BlueprintNodeGet
{
	//Returns the blueprint node get get position output attribute
	public override object GetAttribute()
	{
		//Return output attribute
		return BlueprintInstanceManager.GetBlueprintParentAt(blueprintID).transform.position;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get position instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get position
		connections.Add(new BlueprintConnectionAttributeOutputVector3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get position title
	protected override string GetTitle()
	{
		//Return the blueprint node get get position title
		return "GetPosition";
	}

	//Return the blueprint node get get position body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector3 position label
		BeginSection(1);
			GUILayout.Label("Vector3 : position", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}