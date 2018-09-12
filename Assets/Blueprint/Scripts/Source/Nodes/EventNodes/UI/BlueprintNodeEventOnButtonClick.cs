using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BlueprintNodeEventOnButtonClick : BlueprintNodeEvent
{
	//Start initializes the blueprint node event on button click instance
	public override void StartInitialize()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Create reference to the button input attribute
			Button buttonInputAttribute = (Button)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();

			//If the button input attribute is valid
			if (buttonInputAttribute != null)
			{
				//Setup the buttons on click event to call the execute function
				buttonInputAttribute.onClick.AddListener(Execute);
			}
		}
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node event on button click instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node event on button click
		connections.Add(new BlueprintConnectionAttributeInputButton());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
	}

	//Returns the blueprint node event on button click title
	protected override string GetTitle()
	{
		//Return the blueprint node event on button click title
		return "OnButtonClick";
	}

	//Renders the blueprint node event on button click body components
	protected override void RenderBodyComponents()
	{
		//Render the button label
		BeginSection(1);
			GUILayout.Label("Button : button", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}