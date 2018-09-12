using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlueprintMenustrip : Editor
{
	//Opens the blueprint editor
	[MenuItem("Tools/Blueprint Editor")]
	private static void OpenBlueprintEditor()
	{
		//Open the blueprint editor
		BlueprintEditor.Open();
	}
}