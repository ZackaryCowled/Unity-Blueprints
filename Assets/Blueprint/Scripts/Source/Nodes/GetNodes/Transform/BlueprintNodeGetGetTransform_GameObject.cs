using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetTransform_GameObject : BlueprintNodeGet
{
	//Returns the blueprint node get get transform output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Create reference to the input attribute
			GameObject inputAttribute = BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute() as GameObject;

			//If the input attribute is valid
			if (inputAttribute != null)
			{
				//Return output attribute
				return inputAttribute.transform;
			}
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get transform instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get transform
		connections.Add(new BlueprintConnectionAttributeInputGameObject());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeOutputTransform());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get transform title
	protected override string GetTitle()
	{
		//Return the blueprint node get get transform title
		return "GetTransform";
	}

	//Renders the blueprint node get get transform body components
	protected override void RenderBodyComponents()
	{
		//Render the gameobject label
		BeginSection(1);
			GUILayout.Label("GameObject : object", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}