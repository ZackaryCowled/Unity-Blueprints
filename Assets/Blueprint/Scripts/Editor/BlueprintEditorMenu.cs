using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class BlueprintEditorMenu
{
	//Renders the blueprint editors menu and updates EditorGUILayout and GUILayout componens inside the blueprint editor menu
	public static void Render()
	{
		//Begin area for drawing the blueprint editor menu
		GUILayout.BeginArea(new Rect(Screen.width * 0.5f - 200.0f, Screen.height * 0.5f - 30.0f, Screen.width * 0.5f + 200.0f, Screen.height * 0.5f + 30.0f));
		GUILayout.BeginHorizontal();

		//If the developer presses the create blueprint button
		if (GUILayout.Button("Create Blueprint", GUILayout.Width(200.0f), GUILayout.Height(30.0f)))
		{
			//Create a blueprint at the filepath the developer selected using the create blueprint dialog
			BlueprintEditor.CreateBlueprint(EditorUtility.SaveFilePanel("Create Blueprint", Application.dataPath + "/Resources/Blueprints", "Untitled", "txt"));
		}

		//If the developer presses the open blueprint button
		if (GUILayout.Button("Open Blueprint", GUILayout.Width(200.0f), GUILayout.Height(30.0f)))
		{
			//Load the blueprint at the filepath the developer selected using the open blueprint dialog
			BlueprintEditor.LoadBlueprint(EditorUtility.OpenFilePanel("Open Blueprint", Application.dataPath + "/Resources/Blueprints", "txt"));
		}

		//End area for drawing the blueprint editor menu
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}