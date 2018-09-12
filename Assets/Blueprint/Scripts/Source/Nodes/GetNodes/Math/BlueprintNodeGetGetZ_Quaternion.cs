using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetZ_Quaternion : BlueprintNodeGet
{
	//Returns the blueprint node get get z output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Return output attribute
			return ((Quaternion)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute()).z;
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node get get z default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node get get z default output attribute
		return 0.0f;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get z instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get z
		connections.Add(new BlueprintConnectionAttributeInputQuaternion());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get z title
	protected override string GetTitle()
	{
		//Return the blueprint node get get z title
		return "GetZ";
	}

	//Return the blueprint node get get z body components
	protected override void RenderBodyComponents()
	{
		//Render the Quaternion quaternion label
		BeginSection(1);
			GUILayout.Label("Quaternion : quaternion", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}