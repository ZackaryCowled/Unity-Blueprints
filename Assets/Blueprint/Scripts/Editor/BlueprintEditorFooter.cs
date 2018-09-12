using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintEditorFooter : MonoBehaviour
{
	//Renders the blueprint editor footer and updates EditorGUILayout and GUILayout components inside the blueprint editor footer
	public static void Render()
	{
		//Begin area for drawing the blueprint editor footer
		GUILayout.BeginArea(new Rect(Screen.width * 0.5f - 100.0f, Screen.height - 52.0f, Screen.width * 0.5f + 100.0f, 52.0f));

		//If the developer presses the close blueprint button
		if (GUILayout.Button("Close Blueprint", GUILayout.Width(200.0f), GUILayout.Height(30.0f)))
		{
			//Close the blueprint
			BlueprintEditor.CloseBlueprint();
		}

		//End area for drawing the blueprint editor footer
		GUILayout.EndArea();
	}
}