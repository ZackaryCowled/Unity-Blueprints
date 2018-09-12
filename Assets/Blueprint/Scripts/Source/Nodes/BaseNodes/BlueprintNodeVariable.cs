using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeVariable : BlueprintNode
{
	//Public attributes
	public string variableName = "";

//If running in the Unity Editor
#if UNITY_EDITOR
	//Generates and returns a valid input connection
	public virtual BlueprintConnection GenerateInputConnection()
	{
		//Return default blueprint connection
		return new BlueprintConnection();
	}

	//Returns the blueprint node variable header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node variable header color
		return new Color(0.0f, 0.75f, 0.0f);
	}

	//Renders the blueprint node variable body components
	protected override void RenderBodyComponents()
	{
		//Render the variable name
		BeginSection(1);
			GUILayout.Label("Name", BlueprintStyleHelper.GetNodeAttributeTextStyle(), GUILayout.Width(37.0f));
			variableName = GUILayout.TextField(variableName, GUILayout.Width(113.0f));
		EndSection();
	}

	//Renders the blueprint node variable inspector and returns true if changes to any attributes occurred
	public virtual bool RenderInspector()
	{
		//No changes occurred
		return false;
	}

	//Renders the blueprint node variable type name information
	public virtual void RenderTypeNameInformation()
	{
		//Render the default blueprint node variable type name information
		GUILayout.Label(variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}