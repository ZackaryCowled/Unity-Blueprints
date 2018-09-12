using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetAudioClip : BlueprintNodeSet
{
	//Executes the blueprint node set set audio clip instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Create reference to the audio source input attribute
			AudioSource audioSourceInputAttribute = (AudioSource)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();

			//If the audio source input attribute is valid
			if (audioSourceInputAttribute != null)
			{
				//Set the audio source input attributes audio clip to the audio clip input attribute
				audioSourceInputAttribute.clip = (AudioClip)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute();
			}
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node set set audio clip instance
	public override void Initialize()
	{
		//Initialize the blueprint node set set audio clip instance
		inputExecutionConnection = new BlueprintConnectionInputExecution();
		inputExecutionConnection.Initialize(blueprintID, nodeID, 0, true);
		outputExecutionConnection = new BlueprintConnectionOutputExecution();
		outputExecutionConnection.Initialize(blueprintID, nodeID, 0, false);
		connections.Add(new BlueprintConnectionAttributeInputAudioSource());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
		connections.Add(new BlueprintConnectionAttributeInputAudioClip());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
	}

	//Returns the blueprint node set set audio clip title
	protected override string GetTitle()
	{
		//Return the blueprint node set set audio clip title
		return "SetAudioClip";
	}

	//Renders the blueprint node set set audio clip body components
	protected override void RenderBodyComponents()
	{
		//Render the audio source label
		BeginSection(1);
			GUILayout.Label("AudioSource : source", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the audio clip label
		BeginSection(2);
			GUILayout.Label("AudioClip : clip", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}