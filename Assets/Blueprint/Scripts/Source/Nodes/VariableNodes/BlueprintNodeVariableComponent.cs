//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeVariableComponent<T1, T2, T3> : BlueprintNodeVariable where T1 : Component where T2 : BlueprintConnectionAttribute, new() where T3 : BlueprintConnectionAttribute, new()
{
	//Public attributes
	public string instanceTag = "";
	public string instanceName = "";
	public string instanceID = "";
	public string componentName = "";

	//Protected attributes
	protected Component component = null;

	//Links the variables component instance
	protected void LinkComponentInstance()
	{
		//If the instance tag is valid
		if (!string.IsNullOrEmpty(instanceTag))
		{
			//Query list of GameObjects with the instance tag
			GameObject[] searchList = GameObject.FindGameObjectsWithTag(instanceTag);

			//For each GameObject in the search list
			for (int gameObjectIndex = 0; gameObjectIndex < searchList.Length; gameObjectIndex++)
			{
				//If the GameObjects name matches the instance name
				if (searchList[gameObjectIndex].name == instanceName)
				{
					//Create reference  to the GameObjects blueprint UID
					BlueprintUID gameObjectUID = searchList[gameObjectIndex].GetComponent<BlueprintUID>();

					//If the GameObjects blueprint UID matches the instance ID
					if (gameObjectUID != null && gameObjectUID.ID == instanceID)
					{
						//Link the variables component
						component = searchList[gameObjectIndex].GetComponent<T1>();
						break;
					}
				}
			}
		}
	}

	//Returns the blueprint node variable component output attribute
	public override object GetAttribute()
	{ 
		//If the component is not valid
		if (component == null)
		{
			//Link the component instance
			LinkComponentInstance();
		}

		//Return output attribute
		return component;
	}

	//Sets the blueprint node variable component output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//If running in the Unity Editor
		#if UNITY_EDITOR
			//If the application is playing
			if (Application.isPlaying)
			{
				//Set the blueprint node variable component output attribute to the specified input attribute
				component = (T1)inputAttribute;
			}
			//Otherwise
			else
			{
				//Link the component to the blueprint
				LinkComponentToBlueprint((T1)inputAttribute);
			}
		#else
			//Set the blueprint node variable component output attribute to the specified input attribute
			component = (T1)inputAttribute;
		#endif
	}

	//Sets the blueprint node variable component name
	public void SetComponentName(string name)
	{
		//Set the blueprint node variable component name
		componentName = name;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node variable component instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node variable component
		connections.Add(new T3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Generates and returns a valid input connection for the blueprint node variable component
	public override BlueprintConnection GenerateInputConnection()
	{
		//Return a valid input connection for the blueprint node variable component
		return new T2();
	}

	//Returns the blueprint node variable component title
	protected override string GetTitle()
	{
		//Return the blueprint node variable component title
		return componentName;
	}

	//Links the specified component to the blueprint node variable component and returns true if successful
	private bool LinkComponentToBlueprint(T1 component)
	{
		//If the Component is not valid
		if (component == null)
		{
			//Update attributes
			instanceTag = "";
			instanceName = "";
			instanceID = "";

			//Update the component
			this.component = component;

			//Successfully linked the component to the blueprint
			return true;
		}
		//Otherwise, if the component is attached to the blueprints parent GameObject
		else if (Selection.activeGameObject == component.gameObject)
		{
			//Log warning
			Debug.LogWarning("WARNING: Linking a Component attached to the parent GameObject is not allowed, try linking a Component on a different GameObject.");

			//Reset component value
			this.component = null;
		}
		//Otherwise, if the component is on a prefab
		else if (component.gameObject.scene.name == null)
		{
			//Log warning
			Debug.LogWarning("WARNING: Linking a Component from a prefab is not allowed, try linking a Component from a GameObject instance.");

			//Reset component value
			this.component = null;
		}
		//Otherwise, if the tag is valid
		else if (!string.IsNullOrEmpty(component.gameObject.tag) && component.gameObject.tag != "Untagged")
		{
			//Create reference to the components parent GameObjects blueprint UID
			BlueprintUID gameObjectUID = component.gameObject.GetComponent<BlueprintUID>();

			//If the components parent GameObjects blueprint UID is not valid
			if (gameObjectUID == null)
			{
				//Create and initialize blueprint UID for the components parent GameObject
				gameObjectUID = component.gameObject.AddComponent<BlueprintUID>();
				gameObjectUID.ID = BlueprintIdentificationHelper.GenerateID();
			}

			//Update attributes
			instanceTag = component.gameObject.tag;
			instanceName = component.gameObject.name;
			instanceID = gameObjectUID.ID;

			//Update the component
			this.component = component;

			//Successfully linked the component to the blueprint
			return true;
		}
		//Otherwise
		else
		{
			//Log warning
			Debug.LogWarning("WARNING: Linking a Component from an untagged GameObject is not allowed, set the GameObjects tag and try again.");

			//Reset component value
			this.component = null;
		}

		//Failed to link the component to the blueprint
		return false;
	}

	//Renders the blueprint node variable component body components
	protected override void RenderBodyComponents()
	{
		//Perform base render body components
		base.RenderBodyComponents();

		//Render the component label
		BeginSection(2);
			GUILayout.Label(componentName + " : component", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}

	//Renders the blueprint node variable component inspector and returns true if changes to any attribute occurred
	public override bool RenderInspector()
	{
		//Begin horizontal group
		GUILayout.BeginHorizontal();

		//Render the variable name label
		GUILayout.Label(variableName);

		//Render component field
		T1 component = (T1)EditorGUILayout.ObjectField((T1)GetAttribute(), typeof(T1), true);

		//End horizontal group
		GUILayout.EndHorizontal();

		//If the component has changed
		if (component != (T1)GetAttribute())
		{
			//Link the component to the blueprint
			if (LinkComponentToBlueprint(component))
			{
				//Changes occurred
				return true;
			}
		}

		//No changes occurred
		return false;
	}

	//Renders the blueprint node variable component type name information
	public override void RenderTypeNameInformation()
	{
		//Render the blueprint node variable component type name information
		GUILayout.Label(componentName + " : " + variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}