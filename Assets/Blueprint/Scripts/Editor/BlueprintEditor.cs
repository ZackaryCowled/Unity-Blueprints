using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class BlueprintEditor : EditorWindow
{
	//Private attributes
	private static string editorState = "MENU";

	//Opens the blueprint editor
	public static void Open()
	{
		//Open and configure the blueprint editor window
		EditorWindow blueprintEditorWindow = GetWindow<BlueprintEditor>("Blueprint");
		blueprintEditorWindow.position = new Rect(100.0f, 100.0f, 500, 400);
		blueprintEditorWindow.minSize = new Vector2(500.0f, 400.0f);
	}

	//Draws and udpates the blueprint editor
	private void OnGUI()
	{
		//Update the editor state
		UpdateEditorState();

		//Depending on the editor state
		switch(editorState)
		{
			//Menu state
			case "MENU":
			{
				//Render the blueprint editor menu
				BlueprintEditorMenu.Render();
				break;
			}

			//Edit state
			case "EDIT":
			{
				//Render the blueprint editor header
				BlueprintEditorHeader.Render();
				
				//Perform pre-rendering tasks for the blueprint editor panel
				BlueprintEditorPanel.PreRender();
				
				//Begin rendering windows
				BeginWindows();
				
				//Render the blueprint editor panel
				BlueprintEditorPanel.Render();
				
				//End rendering windows
				EndWindows();
				
				//Perform post-rendering tasks for the blueprint editor panel
				BlueprintEditorPanel.PostRender();

				//Shows any application blueprint node/connection context menus
				BlueprintEditorPanel.ProcessContextMenus();

				//Handle blueprint editor panel events
				BlueprintEditorPanel.HandleEvents();
				
				//Render the blueprint editor footer
				BlueprintEditorFooter.Render();
				break;
			}
		}
	}

	//Updates the editor state
	private static void UpdateEditorState()
	{
		//If the blueprint is not valid
		if(BlueprintEditorManager.blueprint == null)
		{
			//Set the editor state to menu
			editorState = "MENU";
		}
		//Otherwise
		else
		{
			//Set the editor state to edit
			editorState = "EDIT";
		}
	}

	//Creates a blueprint file at the specified filepath
	public static void CreateBlueprint(string filepath)
	{
		//If the filepath is not empty
		if (filepath != "")
		{
			//Create a blueprint
			BlueprintEditorManager.blueprint = new Blueprint(filepath);
		}
	}

	//Closes the blueprint
	public static void CloseBlueprint()
	{
		//Close the blueprint
		BlueprintEditorManager.blueprint = null;
	}

	//Returns the blueprint
	public static Blueprint GetBlueprint()
	{
		//Return the blueprint
		return BlueprintEditorManager.blueprint;
	}

	//Loads the blueprint file at the specified filepath
	public static void LoadBlueprint(string filepath)
	{
		//If the filepath is not empty
		if (filepath != "")
		{
			//Load the blueprint at the specified filepath
			BlueprintEditorManager.blueprint = new Blueprint(filepath);
			BlueprintEditorManager.blueprint.Load();
		}
	}

	//Saves the blueprint
	public static void SaveBlueprint()
	{
		//Save the blueprint
		BlueprintEditorManager.blueprint.Save();

		//Refresh the asset database
		AssetDatabase.Refresh();
	}
}