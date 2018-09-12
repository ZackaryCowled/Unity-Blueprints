using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(BlueprintManager))]
public class BlueprintManagerInspector : Editor
{
	//Private attributes
	private BlueprintManager blueprintManager = null;
	private Blueprint inspectorBlueprint = null;

	//Draws and updates the blueprint manager inspector
	public override void OnInspectorGUI()
	{
		//Update reference to the blueprint manager
		blueprintManager = (BlueprintManager) target;

		//If the blueprint has not been selected
		if (blueprintManager.blueprintFilepath == "")
		{
			//Draw and update the select blueprint button
			ProcessSelectBlueprintButton();
		}
		//Otherwise
		else
		{
			//Draw and update the blueprint manager inspector
			ProcessBlueprintManagerInspector();
		}
	}

	//Enables saving changes to the scene (Call after value has changed)
	private void EnableSavingChanges()
	{
		//If the application is not playing
		if (!Application.isPlaying)
		{
			//Mark the current scene as dirty
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
	}

	//Draws and updates the select blueprint button
	private void ProcessSelectBlueprintButton()
	{
		//If the developer presses the select blueprint button
		if (GUILayout.Button("Select Blueprint"))
		{
			//Get blueprint filepath from the developer
			string inspectorBlueprintFilepath = EditorUtility.OpenFilePanel("Select Blueprint", Application.dataPath + "/Resources/Blueprints", "txt");

			//If the inspector blueprint filepath is valid
			if (inspectorBlueprintFilepath != "" && File.Exists(inspectorBlueprintFilepath))
			{
				//Create the inspector blueprint
				inspectorBlueprint = new Blueprint(inspectorBlueprintFilepath);
				inspectorBlueprint.Load();

				//Update the blueprint filepath string
				blueprintManager.blueprintFilepath = inspectorBlueprintFilepath;

				//Update the blueprint JSON string
				blueprintManager.blueprintJSON = inspectorBlueprint.ConvertToJSON();

				//Enable saving changes
				EnableSavingChanges();
			}
		}
	}

	//Draws and updates the blueprint manager inspector
	private void ProcessBlueprintManagerInspector()
	{
		//Render the blueprints title
		EditorGUILayout.Separator();
		GUILayout.Label(BlueprintPathHelper.GetNameFromPath(blueprintManager.blueprintFilepath), BlueprintStyleHelper.GetBlueprintHeaderTextStyle());
		EditorGUILayout.Separator();

		//If the inspector blueprint is not valid
		if (inspectorBlueprint == null)
		{
			//Create the inspector blueprint
			inspectorBlueprint = new Blueprint();
			inspectorBlueprint.Load(blueprintManager.blueprintJSON);

			//Create temporary blueprint
			Blueprint tempBlueprint = new Blueprint(blueprintManager.blueprintFilepath);
			tempBlueprint.Load();

			//If the blueprints are different
			if (!BlueprintComparisonHelper.BlueprintsMatch(inspectorBlueprint, tempBlueprint))
			{
				//Restore applicable variables
				BlueprintVariableHelper.RestoreVariables(inspectorBlueprint, ref tempBlueprint);

				//Update the blueprint JSON strings
				blueprintManager.blueprintJSON = tempBlueprint.ConvertToJSON();

				//Reload the inspector blueprint
				inspectorBlueprint.Load(blueprintManager.blueprintJSON);

				//Enable saving changes
				EnableSavingChanges();
			}
		}

		//For each blueprint node in the inspector blueprint
		for (int blueprintNodeIndex = 0; blueprintNodeIndex < inspectorBlueprint.GetBlueprintNodeCount(); blueprintNodeIndex++)
		{
			//If the blueprint node is a variable
			if (inspectorBlueprint.GetBlueprintNodeAt(blueprintNodeIndex) as BlueprintNodeVariable != null)
			{
				//Render the variable inspector
				if ((inspectorBlueprint.GetBlueprintNodeAt(blueprintNodeIndex) as BlueprintNodeVariable).RenderInspector())
				{
					//Update the blueprint JSON strings
					blueprintManager.blueprintJSON = inspectorBlueprint.ConvertToJSON();

					//Enable saving changes
					EnableSavingChanges();
				}
			}
		}
	}
}