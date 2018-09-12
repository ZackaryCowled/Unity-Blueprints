using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeFunctionPlay_AudioSource : BlueprintNodeFunction
{
	//Executes the blueprint node function play instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1)
		{
			//Create reference to the audio source input attribute
			AudioSource audioSourceInputAttribute = (AudioSource)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();

			//If the audio source input attribute is valid
			if (audioSourceInputAttribute != null)
			{
				//Play the audio source input attribute
				audioSourceInputAttribute.Play();
			}
		}

		//Perform base execution
		base.Execute();
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node function play instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node function play
		connections.Add(new BlueprintConnectionAttributeInputAudioSource());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, true);
	}

	//Returns the blueprint node function play title
	protected override string GetTitle()
	{
		//Return the blueprint node function play title
		return "Play";
	}

	//Renders the blueprint node function play body components
	protected override void RenderBodyComponents()
	{
		//Render the audio source label
		BeginSection(1);
			GUILayout.Label("AudioSource : source", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}
#endif
}