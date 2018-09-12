using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlueprintEditorManager
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Public attributes
	public static Blueprint blueprint = null;
	public static BlueprintConnection connection = null;
#endif
}