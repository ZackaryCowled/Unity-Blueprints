using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventOnCollisionEnter : BlueprintNodeEvent
{
	//Start initializes the blueprint node event on collision enter instance
	public override void StartInitialize()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Create reference to the GameObject input attribute
			GameObject gameObjectInputAttribute = (GameObject)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();

			//If the GameObject input attribute is valid
			if (gameObjectInputAttribute != null)
			{
				//Create reference to the GameObject input attributes blueprint collision event handler
				BlueprintCollisionEventHandler collisionEventHandler = gameObjectInputAttribute.GetComponent<BlueprintCollisionEventHandler>();

				//If the collision event handler is not valid
				if (collisionEventHandler == null)
				{
					//Create blueprint collision event handler for the GameObject input attribute
					collisionEventHandler = gameObjectInputAttribute.AddComponent<BlueprintCollisionEventHandler>();
				}

				//Setup the collision event handler to call the execute function on collision enter
				collisionEventHandler.onCollisionEnterEvent.AddListener(Execute);
			}
		}
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node event on collision enter instance
	public override void Initialize()
	{
		//Perform base initiailzation
		base.Initialize();

		//Initialize the blueprint node event on collision enter
		connections.Add(new BlueprintConnectionAttributeInputGameObject());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);

		//TODO: add collision output connection and other code to the node...
	}

	//Returns the blueprint node event on collision enter title
	protected override string GetTitle()
	{
		//Return the blueprint node event on collision enter title
		return "OnCollisionEnter";
	}

	//Renders the blueprint node event on collision enter body components
	protected override void RenderBodyComponents()
	{
		//Render the gameobject label
		BeginSection(1);
			GUILayout.Label("GameObject : object", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}