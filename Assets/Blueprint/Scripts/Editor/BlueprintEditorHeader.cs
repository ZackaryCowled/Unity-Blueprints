using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class BlueprintEditorHeader
{
	//Renders the blueprint editor header and updates EditorGUILayout and GUILayout components inside the blueprint editor header
	public static void Render()
	{
		//Render the active blueprints name
		GUILayout.Label(BlueprintEditor.GetBlueprint().GetName(), BlueprintStyleHelper.GetBlueprintHeaderTextStyle(), GUILayout.Width(Screen.width), GUILayout.Height(25.0f));

		//Begin area for drawing the blueprint editor header toolbar
		GUILayout.BeginArea(new Rect(Screen.width * 0.5f - 100.0f, 25.0f, Screen.width * 0.5f + 100.0f, 30.0f));

		//If the developer presses the save blueprint button
		if(GUILayout.Button("Save Blueprint", GUILayout.Width(200.0f), GUILayout.Height(30.0f)))
		{
			//Save the blueprint
			BlueprintEditor.SaveBlueprint();
		}

		//End area for drawing the blueprint editor header toolbar
		GUILayout.EndArea();
	}
}