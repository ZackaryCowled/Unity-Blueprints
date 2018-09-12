using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetBlueprint : BlueprintNodeGet
{
	//Returns the blueprint node get get blueprint output attribute
	public override object GetAttribute()
	{
		//If dependent connection attributes are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Create reference to the GameObject input attribute
			GameObject gameObjectInputAttribute = (GameObject)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();

			//If the GameObject input attribute is valid
			if (gameObjectInputAttribute != null)
			{
				//Create reference to the GameObject input attributes blueprint manager
				BlueprintManager blueprintManager = gameObjectInputAttribute.GetComponent<BlueprintManager>();

				//If the blueprint manager is valid
				if (blueprintManager != null)
				{
					//Return output attribute
					return blueprintManager.GetBlueprintID();
				}
			}
		}

		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node get get blueprint default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Return the blueprint node get get blueprint default output attribute
		return -1;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node get get blueprint instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get blueprint
		connections.Add(new BlueprintConnectionAttributeInputGameObject());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeOutputBlueprint());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Returns the blueprint node get get blueprint title
	protected override string GetTitle()
	{
		//Return the blueprint node get get blueprint title
		return "GetBlueprint";
	}

	//Renders the blueprint node get get blueprint body components
	protected override void RenderBodyComponents()
	{
		//Render the GameObject object label
		BeginSection(1);
			GUILayout.Label("GameObject : object", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}