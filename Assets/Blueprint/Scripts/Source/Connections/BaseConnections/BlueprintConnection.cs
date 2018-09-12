//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintConnection
{
	//Public attributes
	public int blueprintID = -1;
	public int nodeID = -1;
	public int connectionNodeID = -1;
	public int sectionID = 0;
	public int connectionSectionID = 0;
	public bool isInput = false;
	public bool connectionIsInput = false;
	public bool isVisible = true;

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private bool shouldShowContextMenu = false;

	//Initializes the blueprint connection
	public virtual void Initialize(int blueprintID, int nodeID, int sectionID, bool isInput)
	{
		//Initialize the blueprint connection
		this.blueprintID = blueprintID;
		this.nodeID = nodeID;
		this.sectionID = sectionID;
		this.isInput = isInput;
	}

	//Returns the color for the blueprint connection box
	protected virtual Color GetBlueprintConnectionBoxColor()
	{
		//Return default blueprint connection box color
		return new Color(0.0f, 0.0f, 0.0f);
	}

	//Returns the Rect for the blueprint connection box
	private Rect GetBlueprintConnectionBoxRect()
	{
		//Create rect attribute
		Rect rect = new Rect();

		//If the blueprint connection is an input
		if(isInput)
		{
			//Configure the rect x attribute
			rect.x = BlueprintEditorManager.blueprint.GetBlueprintNodeAt(nodeID).x - BlueprintStyleHelper.GetNodeConnectionBoxWidth();
		}
		//Otherwise
		else
		{
			//Configure the rect x attribute
			rect.x = BlueprintEditorManager.blueprint.GetBlueprintNodeAt(nodeID).x + BlueprintStyleHelper.GetNodeSectionWidth();
		}

		//Configure generic rect attributes
		rect.y = BlueprintEditorManager.blueprint.GetBlueprintNodeAt(nodeID).y + BlueprintStyleHelper.GetMargin() + BlueprintStyleHelper.GetNodeSectionHeight() * sectionID;
		rect.width = BlueprintStyleHelper.GetNodeConnectionBoxWidth();
		rect.height = BlueprintStyleHelper.GetNodeConnectionBoxHeight();

		//Return the rect attribute
		return rect;
	}

	//Returns the position for the blueprint connection link
	private Vector3 GetBlueprintConnectionLinkPosition()
	{
		//Return the position for the blueprint connection link
		return new Vector3(isInput ? BlueprintEditorManager.blueprint.GetBlueprintNodeAt(nodeID).x - BlueprintStyleHelper.GetNodeConnectionBoxWidth() * 0.5f : BlueprintEditorManager.blueprint.GetBlueprintNodeAt(nodeID).x + BlueprintStyleHelper.GetNodeSectionWidth() + BlueprintStyleHelper.GetNodeConnectionBoxWidth() * 0.5f,
						   BlueprintEditorManager.blueprint.GetBlueprintNodeAt(nodeID).y + BlueprintStyleHelper.GetMargin() + BlueprintStyleHelper.GetNodeSectionHeight() * sectionID + BlueprintStyleHelper.GetNodeConnectionBoxHeight() * 0.5f,
						   0.0f);
	}

	//Returns the tangent for the blueprint connection link
	private Vector3 GetBlueprintConnectionLinkTangent()
	{
		//Return the tangent for the blueprint connection link
		return GetBlueprintConnectionLinkPosition() + new Vector3(isInput ? 1.0f : -1.0f, 0.0f, 0.0f);
	}

	//Handles events for the blueprint connection
	public void HandleEvents()
	{
		//If the mouse position is inside the blueprint connection box
		if (GetBlueprintConnectionBoxRect().Contains(Event.current.mousePosition))
		{
			//If the current event is a mouse down event
			if (Event.current.type == EventType.MouseDown)
			{
				//Handle mouse down events
				HandleMouseDownEvents();
			}
			//Otherwise, if the current event is a mouse up event
			else if(Event.current.type == EventType.MouseUp)
			{
				//Handle mouse up events
				HandleMouseUpEvents();
			}
		}
		//Otherwise, if the mouse is outside the blueprint connection and a connection is pending
		else if(BlueprintEditorManager.connection == this)
		{
			//Handle pending connection events
			HandlePendingConnectionEvents();
		}
	}

	//Handles mouse down events for the blueprint connection
	private void HandleMouseDownEvents()
	{
		//If the left mouse button triggered the mouse down event
		if (Event.current.button == 0)
		{
			//Start connection
			BlueprintEditorManager.connection = this;
		}
		//Otherwise, if the right mouse button triggered the mouse down event
		else if(Event.current.button == 1)
		{
			//Set the should show context menu to true
			shouldShowContextMenu = true;
		}

		//Process event
		Event.current.Use();
	}

	//Handles mouse up events for the blueprint connection
	private void HandleMouseUpEvents()
	{
		//If the left mouse button triggered the mouse up event
		if (Event.current.button == 0)
		{
			//If a blueprint connection has started and the blueprint connection is not this blueprint connection
			if (BlueprintEditorManager.connection != null && BlueprintEditorManager.connection != this)
			{
				//If the blueprint connections can be connected together
				if (IsConnectable(BlueprintEditorManager.connection))
				{
					//Destroy local blueprint connections pre-existing connection
					DestroyConnection();

					//Destroy external blueprint connections pre-existing connection
					BlueprintEditorManager.connection.DestroyConnection();

					//Make connection
					MakeConnection(BlueprintEditorManager.connection);
				}
				//Otherwise
				else
				{
					//Cancel the connection
					BlueprintEditorManager.connection = null;
				}

				//Process event
				Event.current.Use();
			}
		}
	}

	//Handles pending connection events for the blueprint connection
	private void HandlePendingConnectionEvents()
	{
		//Render the blueprint connection link
		Handles.DrawBezier(GetBlueprintConnectionLinkPosition(), Event.current.mousePosition, GetBlueprintConnectionLinkTangent(), new Vector3(Event.current.mousePosition.x, Event.current.mousePosition.y, 0.0f) + Vector3.Normalize(new Vector3(Event.current.mousePosition.x, Event.current.mousePosition.y, 0.0f) - GetBlueprintConnectionLinkPosition()), new Color(1.0f, 1.0f, 1.0f), Texture2D.whiteTexture, BlueprintStyleHelper.GetNodeConnectionLinkWidth());
	}

	//Returns a flag indicating whether the blueprint connection is visible
	public bool IsVisible()
	{
		//Return the is visible flag
		return isVisible;
	}

	//Destroys the blueprint connections connection
	public void DestroyConnection()
	{
		//Cache the local connection
		int cacheConnectionNodeID = connectionNodeID;
		int cacheConnectionSectionID = connectionSectionID;
		bool cacheConnectionIsInput = connectionIsInput;

		//Destroy the local connection
		connectionNodeID = -1;
		connectionSectionID = -1;
		connectionIsInput = false;

		//If a connection exists
		if (cacheConnectionNodeID > -1 && cacheConnectionSectionID > -1)
		{
			//If the external connection is connected to something
			if (BlueprintEditorManager.blueprint.GetBlueprintNodeAt(cacheConnectionNodeID).GetConnection(cacheConnectionSectionID, cacheConnectionIsInput).connectionNodeID > -1)
			{
				//Destroy external connection
				BlueprintEditorManager.blueprint.GetBlueprintNodeAt(cacheConnectionNodeID).GetConnection(cacheConnectionSectionID, cacheConnectionIsInput).DestroyConnection();
			}
		}
	}

	//Returns a flag indicating whether the specified blueprint connection is connectable with this blueprint connection
	protected virtual bool IsConnectable(BlueprintConnection blueprintConnection)
	{
		//Return default result
		return true;
	}

	//Makes a connection between the specified blueprint connection
	public void MakeConnection(BlueprintConnection blueprintConnection)
	{
		//Make local connection
		connectionNodeID = BlueprintEditorManager.connection.nodeID;
		connectionSectionID = BlueprintEditorManager.connection.sectionID;
		connectionIsInput = BlueprintEditorManager.connection.isInput;

		//Make external connection
		BlueprintEditorManager.connection.connectionNodeID = nodeID;
		BlueprintEditorManager.connection.connectionSectionID = sectionID;
		BlueprintEditorManager.connection.connectionIsInput = isInput;

		//Finalize connection
		BlueprintEditorManager.connection = null;
	}
	
	//Renders the blueprint connection
	public void Render()
	{
		//Render the blueprint connection box
		RenderBlueprintConnectionBox();

		//Render the blueprint connection link
		RenderBlueprintConnectionLink();
	}

	//Renders the blueprint connection box
	private void RenderBlueprintConnectionBox()
	{
		//Store the GUI background color
		Color originalBackgroundColor = GUI.backgroundColor;

		//Set the GUI background color
		GUI.backgroundColor = GetBlueprintConnectionBoxColor();

		//Render the blueprint connection box
		GUI.Box(GetBlueprintConnectionBoxRect(), "");

		//Restore the GUI background color
		GUI.backgroundColor = originalBackgroundColor;
	}

	//Renders the blueprint connection link
	private void RenderBlueprintConnectionLink()
	{
		//If a connection link is possible
		if(connectionNodeID > -1)
		{
			//Query connection link
			BlueprintConnection connectionLink = BlueprintEditorManager.blueprint.GetBlueprintNodeAt(connectionNodeID).GetConnection(connectionSectionID, connectionIsInput);

			//If the connection link is valid
			if(connectionLink != null)
			{
				//Render the blueprint connection link
				Handles.DrawBezier(GetBlueprintConnectionLinkPosition(), connectionLink.GetBlueprintConnectionLinkPosition(), GetBlueprintConnectionLinkTangent(), connectionLink.GetBlueprintConnectionLinkTangent(), new Color(1.0f, 1.0f, 1.0f), Texture2D.whiteTexture, BlueprintStyleHelper.GetNodeConnectionLinkWidth());
			}
		}
	}

	//Returns a flag indicating whether the blueprint connections context menu should be shown
	public bool ShouldShowContextMenu()
	{
		//Return the should show context menu flag
		return shouldShowContextMenu;
	}

	//Shows the blueprint connections context menu
	public void ShowContextMenu()
	{
		//Create context menu
		GenericMenu contextMenu = new GenericMenu();
		contextMenu.AddItem(new GUIContent("Delete"), false, DestroyConnection);

		//Show context menu
		contextMenu.ShowAsContext();

		//Set the should show context menu flag to false
		shouldShowContextMenu = false;
	}
#endif
}