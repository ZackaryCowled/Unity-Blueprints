//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNode
{
	//Public attributes
	public int blueprintID = -1;
	public int nodeID = -1;
	public float x = 0.0f;
	public float y = 0.0f;
	public BlueprintConnectionInputExecution inputExecutionConnection = null;
	public BlueprintConnectionOutputExecution outputExecutionConnection = null;
	public List<BlueprintConnection> connections = new List<BlueprintConnection>();

	//Initializes the blueprint node for runtime
	public virtual void RuntimeInitialize() { }

	//Start initializes the blueprint node event
	public virtual void StartInitialize() { }

	//Executes the blueprint node
	public virtual void Execute() { }

	//Returns the blueprint nodes output attribute
	public virtual object GetAttribute()
	{
		//Return default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint nodes default output attribute
	public virtual object GetDefaultOutputAttribute()
	{
		//Return the default output attribute
		return null;
	}

	//Sets the blueprint nodes output attribute to the specified input attribute
	public virtual void SetAttribute(object inputAttribute) { }

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private bool _isDraggable = false;
	private bool _shouldShowContextMenu = false;

	//Initializes the blueprint node
	public virtual void Initialize() { }

	//Performs begin tasks for rendering the body section at the specified index
	protected void BeginSection(int sectionIndex)
	{
		//Begin area for rendering the section
		GUILayout.BeginArea(new Rect(BlueprintStyleHelper.GetMargin(),
									 sectionIndex * BlueprintStyleHelper.GetNodeSectionHeight() + BlueprintStyleHelper.GetMargin(),
									 BlueprintStyleHelper.GetNodeSectionWidth() - BlueprintStyleHelper.GetMargin() * 2.0f,
									 BlueprintStyleHelper.GetNodeSectionHeight() - BlueprintStyleHelper.GetMargin() * 2.0f));

		//Begin horizontal area
		GUILayout.BeginHorizontal();
	}

	//Ends the current section
	protected void EndSection()
	{
		//End horizontal area
		GUILayout.EndHorizontal();

		//End area for rendering the section
		GUILayout.EndArea();
	}

	//Deletes the blueprint node
	private void Delete()
	{
		//If the input execution connection is valid
		if(inputExecutionConnection != null)
		{
			//Destroy input execution connection
			inputExecutionConnection.DestroyConnection();
		}

		//If the output execution connection is valid
		if(outputExecutionConnection != null)
		{
			//Destroy output execution connection
			outputExecutionConnection.DestroyConnection();
		}

		//For each connection
		for(int connectionIndex = 0; connectionIndex < connections.Count; connectionIndex++)
		{
			//Destroy connections connection
			connections[connectionIndex].DestroyConnection();
		}

		//Destroy the blueprint node
		BlueprintEditorManager.blueprint.DestroyBlueprintNode(this);
	}

	//Returns the connection with matching properties in the blueprint node
	public BlueprintConnection GetConnection(int sectionID, bool isInput)
	{
		//If the input execution connection is valid
		if(inputExecutionConnection != null)
		{
			//If the input execution connection matches the specified connection
			if(inputExecutionConnection.sectionID == sectionID && inputExecutionConnection.isInput == isInput)
			{
				//Return the input execution connection
				return inputExecutionConnection;
			}
		}

		//If the output execution connection is valid
		if(outputExecutionConnection != null)
		{
			//If the output execution connection matches the specified connection
			if(outputExecutionConnection.sectionID == sectionID && outputExecutionConnection.isInput == isInput)
			{
				//Return the output execution connection
				return outputExecutionConnection;
			}
		}

		//For each connection
		for(int connectionIndex = 0; connectionIndex < connections.Count; connectionIndex++)
		{
			//If the connection matches the specified connection
			if(connections[connectionIndex].sectionID == sectionID && connections[connectionIndex].isInput == isInput)
			{
				//Return the connection
				return connections[connectionIndex];
			}
		}

		//The connection could not be found
		return null;
	}

	//Returns the blueprint nodes header color
	protected virtual Color GetHeaderColor()
	{
		//Return the default blueprint node header color
		return new Color(0.0f, 0.0f, 0.0f);
	}

	//Returns the highest section ID of the available connections in the blueprint node
	protected int GetHighestSectionID()
	{
		//Attribute storing the highest section ID
		int highestSectionID = 0;

		//For each connection
		for(int connectionIndex = 0; connectionIndex < connections.Count; connectionIndex++)
		{
			//If the connections section ID is higher than the highest section ID
			if(connections[connectionIndex].sectionID > highestSectionID)
			{
				//Set the highest section ID to the connections section ID
				highestSectionID = connections[connectionIndex].sectionID;
			}
		}

		//Return the highest section ID
		return highestSectionID;
	}

	//Returns the blueprint nodes title
	protected virtual string GetTitle()
	{
		//Return the default blueprint node title
		return "Untitled";
	}

	//Handles events for the blueprint node
	private void HandleEvents()
	{
		//If the current event type is a mouse down event
		if (Event.current.type == EventType.MouseDown)
		{
			//Handle mouse down events
			HandleMouseDownEvents();
		}
		//Otherwise, if the current event type is a mouse up event
		else if(Event.current.type == EventType.MouseUp)
		{
			//Handle mouse up events
			HandleMouseUpEvents();
		}
		//Otherwise, if the current event type is a mouse drag event
		else if(Event.current.type == EventType.MouseDrag)
		{
			//Handle mouse drag events
			HandleMouseDragEvents();
		}
	}

	//Handles mouse down events for the blueprint node
	private void HandleMouseDownEvents()
	{
		//If the left mouse button triggered the mouse down event
		if(Event.current.button == 0)
		{
			//Set the is draggable flag to true
			_isDraggable = true;
		}
		//Otherwise, if the right mouse button triggered the mouse down event
		else if (Event.current.button == 1)
		{
			//Set the is draggable flag to false
			_isDraggable = false;

			//Set the should show context menu flag to true
			_shouldShowContextMenu = true;
		}
		//Otherwise, if the middle mouse button triggered the mouse down event
		else if(Event.current.button == 2)
		{
			//Set the is draggable flag to false
			_isDraggable = false;
		}

		//Process event
		Event.current.Use();
	}

	//Handles mouse up events for the blueprint node
	private void HandleMouseUpEvents()
	{
		//If the left mouse button triggered the mouse up event
		if(Event.current.button == 0)
		{
			//Set the is draggable flag to false
			_isDraggable = false;
		}
	}

	//Handles mouse drag events for the blueprint node
	private void HandleMouseDragEvents()
	{
		//If a connection is not pending
		if (BlueprintEditorManager.connection == null)
		{
			//If the blueprint node is draggable
			if (_isDraggable)
			{
				//If the left mouse button is triggering the mouse drag event
				if (Event.current.button == 0)
				{
					//Translate the blueprint node by the mouse delta
					Translate(Event.current.delta);
				}
			}
		}

		//Process event
		Event.current.Use();
	}

	//Renders the blueprint node
	public void Render()
	{
		//Render and update the position of the blueprint node
		SetPosition(GUI.Window(nodeID, new Rect(x, y, BlueprintStyleHelper.GetNodeSectionWidth(), BlueprintStyleHelper.GetNodeSectionHeight() * (GetHighestSectionID() + 1)), RenderComponents, "", GUIStyle.none));

		//Render the blueprint nodes connection boxes
		RenderConnectionBoxes();
	}

	//Renders the blueprint nodes components
	private void RenderComponents(int id)
	{
		//Render the blueprint nodes header
		RenderHeader();

		//Render the blueprint nodes body
		RenderBody();

		//Handle the blueprint nodes events
		HandleEvents();
	}

	//Renders the blueprint nodes header
	private void RenderHeader()
	{
		//Store the GUI background color
		Color originalBackgroundColor = GUI.backgroundColor;

		//Set the GUI background color to the blueprint nodes header color
		GUI.backgroundColor = GetHeaderColor();

		//Render the header box
		GUI.Box(new Rect(0.0f, 0.0f, BlueprintStyleHelper.GetNodeSectionWidth(), BlueprintStyleHelper.GetNodeSectionHeight()), "");

		//Restore the GUI background color
		GUI.backgroundColor = originalBackgroundColor;

		//Render the header title
		BeginSection(0);
			GUILayout.Label(GetTitle(), BlueprintStyleHelper.GetNodeHeaderTextStyle());
		EndSection();
	}

	//Renders the blueprint nodes body
	private void RenderBody()
	{
		//Render the blueprint nodes body box
		GUI.Box(new Rect(0.0f, BlueprintStyleHelper.GetNodeSectionHeight(), BlueprintStyleHelper.GetNodeSectionWidth(), BlueprintStyleHelper.GetNodeSectionHeight() * GetHighestSectionID()), "");

		//Render the blueprint nodes body components
		RenderBodyComponents();
	}

	//Renders the blueprint nodes body components
	protected virtual void RenderBodyComponents(){}

	//Renders the blueprint nodes connection boxes
	private void RenderConnectionBoxes()
	{
		//If the input execution connection is valid
		if(inputExecutionConnection != null)
		{
			//If the input execution connection is visible
			if (inputExecutionConnection.IsVisible())
			{
				//Render the input execution connection
				inputExecutionConnection.Render();

				//Handle events for the input execution connection
				inputExecutionConnection.HandleEvents();
			}
		}

		//If the output execution connection is valid
		if(outputExecutionConnection != null)
		{
			//If the output execution connection is visible
			if (outputExecutionConnection.IsVisible())
			{
				//Render the output execution connection
				outputExecutionConnection.Render();

				//Handle events for the output execution connection
				outputExecutionConnection.HandleEvents();
			}
		}

		//For each connection
		for(int connectionIndex = 0; connectionIndex < connections.Count; connectionIndex++)
		{
			//If the connections is visible
			if (connections[connectionIndex].IsVisible())
			{
				//Render the input connection
				connections[connectionIndex].Render();

				//Handle events for the connection
				connections[connectionIndex].HandleEvents();
			}
		}
	}

	//Returns a flag indicating whether the blueprint nodes context menu should be shown
	public bool ShouldShowContextMenu()
	{
		//Return the should show context menu flag
		return _shouldShowContextMenu;
	}

	//Shows the blueprint nodes context menu
	public void ShowContextMenu()
	{
		//Create context menu
		GenericMenu contextMenu = new GenericMenu();
		contextMenu.AddItem(new GUIContent("Delete"), false, Delete);

		//Show the context meu
		contextMenu.ShowAsContext();

		//Set the should show context menu flag to false
		_shouldShowContextMenu = false;
	}

	//Sets the position of the blueprint node using the x and y coordinates in the specified Rect
	private void SetPosition(Rect rect)
	{
		//Set the position of the blueprint node using the x and y coordinate in the specified Rect
		x = rect.x;
		y = rect.y;
	}

	//Translates the blueprint nodes position by the specified translation
	public void Translate(Vector2 translation)
	{
		//Translate the blueprint nodes position by the specified translation
		x += translation.x;
		y += translation.y;
	}
#endif
}