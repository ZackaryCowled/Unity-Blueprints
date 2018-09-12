//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeEventGetMouseButtonDown_GameObject : BlueprintNodeEvent
{
	//Public attributes
	public int selectedMouseButton = 0;

	//Start initializes the blueprint node event get mouse button down instance
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
				//Create reference to the GameObject input attributes blueprint input event handler
				BlueprintInputEventHandler inputEventHandler = gameObjectInputAttribute.GetComponent<BlueprintInputEventHandler>();

				//If the collision input handler is not valid
				if (inputEventHandler == null)
				{
					//Create blueprint input event handler for the GameObject input attribute
					inputEventHandler = gameObjectInputAttribute.AddComponent<BlueprintInputEventHandler>();
				}

				//Setup the input event handler to call the execute function on collision enter
				inputEventHandler.onMouseOverEvent.AddListener(Execute);
			}
		}
	}

	//Executes the blueprint node event get mouse button down instance
	public override void Execute()
	{
		//If the selected mouse button is being pressed down this frame
		if (Input.GetMouseButtonDown(selectedMouseButton))
		{
			//If the output execution connection is valid
			if (outputExecutionConnection.connectionNodeID > -1)
			{
				//Execute the output execution connection node
				BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(outputExecutionConnection.connectionNodeID).Execute();
			}
		}
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private static readonly string[] mouseButtons = new string[]
	{
		"Mouse 0",
		"Mouse 1",
		"Mouse 2",
		"Mouse 3",
		"Mouse 4",
		"Mouse 5",
		"Mouse 6"
	};

	//Initializes the blueprint node event get mouse button down instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node event get mouse down button
		connections.Add(new BlueprintConnectionAttributeInputGameObject());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
	}

	//Returns the blueprint node event get mouse button down title
	protected override string GetTitle()
	{
		//Return the blueprint node event get mouse button down title
		return "GetMouseButtonDown";
	}

	//Renders the blueprint node event get mouse button down body components
	protected override void RenderBodyComponents()
	{
		//Render the selected key dropdown menu
		BeginSection(1);
			selectedMouseButton = EditorGUILayout.Popup(selectedMouseButton, mouseButtons);
		EndSection();

		//Render the GameObject label
		BeginSection(2);
			GUILayout.Label("GameObject : object", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}