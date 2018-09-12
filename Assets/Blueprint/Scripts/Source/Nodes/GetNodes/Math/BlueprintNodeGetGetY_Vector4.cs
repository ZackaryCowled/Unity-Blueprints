using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetY_Vector4 : BlueprintNodeGet
{
	//Returns the blueprint node get get y output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Return output attribute
			return ((Vector4)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute()).y;
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node get get y default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node get get y default output attribute
		return 0.0f;
	}

	//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get y instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get y
		connections.Add(new BlueprintConnectionAttributeInputVector4());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeOutputFloat());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get y title
	protected override string GetTitle()
	{
		//Return the blueprint node get get y title
		return "GetY";
	}

	//Return the blueprint node get get y body components
	protected override void RenderBodyComponents()
	{
		//Render the Vector4 vector label
		BeginSection(1);
			GUILayout.Label("Vector4 : vector", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}