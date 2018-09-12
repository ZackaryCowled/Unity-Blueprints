using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlueprintStyleHelper
{
//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private static GUIStyle blueprintHeaderTextStyle = null;
	private static GUIStyle nodeAttributeTextStyle = null;
	private static GUIStyle nodeHeaderTextStyle = null;
	private static Texture2D panelBackgroundTexture = null;
	private static float nodeConnectionBoxWidth = 14.0f;
	private static float nodeConnectionBoxHeight = 14.0f;
	private static float nodeConnectionLinkWidth = 3.0f;
	private static float nodeSectionWidth = 160.0f;
	private static float nodeSectionHeight = 20.0f;
	private static float margin = 3.0f;

	//Returns the blueprint header text style
	public static GUIStyle GetBlueprintHeaderTextStyle()
	{
		//If the blueprint header text style is not valid
		if(blueprintHeaderTextStyle == null)
		{
			//Initialize the blueprint header text style
			blueprintHeaderTextStyle = new GUIStyle();
			blueprintHeaderTextStyle.normal.textColor = new Color(0.0f, 0.0f, 0.0f);
			blueprintHeaderTextStyle.alignment = TextAnchor.MiddleCenter;
			blueprintHeaderTextStyle.fontStyle = FontStyle.Bold;
		}

		//Return the blueprint header text style
		return blueprintHeaderTextStyle;
	}

	//Returns the node attribute text style
	public static GUIStyle GetNodeAttributeTextStyle()
	{
		//If the node attribute text style is not valid
		if(nodeAttributeTextStyle == null)
		{
			//Initialize the node attribute text style
			nodeAttributeTextStyle = new GUIStyle();
			nodeAttributeTextStyle.normal.textColor = new Color(0.0f, 0.0f, 0.0f);
			nodeAttributeTextStyle.alignment = TextAnchor.MiddleLeft;
			nodeAttributeTextStyle.fontStyle = FontStyle.Bold;
		}

		//Return the node attribute text style
		return nodeAttributeTextStyle;
	}

	//Returns the node header text style
	public static GUIStyle GetNodeHeaderTextStyle()
	{
		//If the node header text style is not valid
		if(nodeHeaderTextStyle == null)
		{
			//Initialize the node header text style
			nodeHeaderTextStyle = new GUIStyle();
			nodeHeaderTextStyle.normal.textColor = new Color(1.0f, 1.0f, 1.0f);
			nodeHeaderTextStyle.alignment = TextAnchor.MiddleCenter;
			nodeHeaderTextStyle.fontStyle = FontStyle.Bold;
		}

		//Return the node header text style
		return nodeHeaderTextStyle;
	}

	//Returns the node connection box width
	public static float GetNodeConnectionBoxWidth()
	{
		//Return the node connection box width
		return nodeConnectionBoxWidth;
	}

	//Returns the node connection box height
	public static float GetNodeConnectionBoxHeight()
	{
		//Return the node connection box height
		return nodeConnectionBoxHeight;
	}

	//Returns the node connection box size
	public static Vector2 GetNodeConnectionBoxSize()
	{
		//Return the node connection box size
		return new Vector2(nodeConnectionBoxWidth, nodeConnectionBoxHeight);
	}

	//Returns the node connection link width
	public static float GetNodeConnectionLinkWidth()
	{
		//Return the node connection link width
		return nodeConnectionLinkWidth;
	}

	//Returns the node section width
	public static float GetNodeSectionWidth()
	{
		//Return the node section width
		return nodeSectionWidth;
	}

	//Returns the node section height
	public static float GetNodeSectionHeight()
	{
		//Return the node section height
		return nodeSectionHeight;
	}

	//Returns the node section size
	public static Vector2 GetNodeSectionSize()
	{
		//Return the node section size
		return new Vector2(nodeSectionWidth, nodeSectionHeight);
	}

	//Returns the margin
	public static float GetMargin()
	{
		//Return the margin
		return margin;
	}

	//Returns the editor panel background texture
	public static Texture2D GetPanelBackgroundTexture()
	{
		//If the editor panel background texture is not valid
		if(panelBackgroundTexture == null)
		{
			//Initialize the editor panel background texture
			panelBackgroundTexture = Resources.Load<Texture2D>("BlueprintToolAssets/BlueprintGridTile");
		}

		//Return the editor panel background texture
		return panelBackgroundTexture;
	}
#endif
}