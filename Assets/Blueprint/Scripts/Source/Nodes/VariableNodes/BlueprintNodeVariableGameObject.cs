//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeVariableGameObject : BlueprintNodeVariable
{
	//Public attributes
	public bool isPrefab = false;
	public string prefabFilepath = "";
	public string instanceTag = "";
	public string instanceName = "";
	public string instanceID = "";

	//Private attributes
	private GameObject attributeValue = null;

	//Links the gameobject
	private void LinkGameObjectInstance()
	{
		//If the gameobject is a prefab
		if (isPrefab)
		{
			//If the prefab filepath is valid
			if (!string.IsNullOrEmpty(prefabFilepath))
			{
				//Link the gameobject to the attribute value
				attributeValue = Resources.Load<GameObject>(BlueprintPathHelper.ConvertToAssetResourcePath(prefabFilepath));
			}
		}
		//Otherwise, if the instance tag is valid
		if (!string.IsNullOrEmpty(instanceTag))
		{
			//Find all GameObjects with the instance tag
			GameObject[] searchList = GameObject.FindGameObjectsWithTag(instanceTag);

			//For each gameobject in the search list
			for (int objectIndex = 0; objectIndex < searchList.Length; objectIndex++)
			{
				//If the GameObjects name matches the instance name
				if (searchList[objectIndex].name == instanceName)
				{
					//Create reference to the GameObjects blueprint ID
					BlueprintUID gameObjectID = searchList[objectIndex].GetComponent<BlueprintUID>();

					//If the GameObjects blueprint ID is valid
					if (gameObjectID != null)
					{
						//If the GameObjects blueprint ID matches the instance ID
						if (gameObjectID.ID == instanceID)
						{
							//Link the GameObject to the attribute value
							attributeValue = searchList[objectIndex];
							return;
						}
					}
				}
			}
		}
	}

	//Returns the blueprint node variable gameobject output attribute
	public override object GetAttribute()
	{
		//If the attribute value is not valid
		if (attributeValue == null)
		{
			//Link the gameobject instance
			LinkGameObjectInstance();
		}

		//Return output attribute
		return attributeValue;
	}

	//Sets the blueprint node variable gameobject output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//If running in the Unity Editor
		#if UNITY_EDITOR
			//If the application is playing
			if (Application.isPlaying)
			{
				//Set the blueprint node variable gameobject output attribute to the specified input attribute
				attributeValue = (GameObject)inputAttribute;
			}
			//Otherwise
			else
			{
				//Link the GameObject to the blueprint
				LinkGameObjectToBlueprint((GameObject) inputAttribute);
			}
		#else
			//Set the blueprint node variable gameobject output attribute to the specified input attribute
			attributeValue = (GameObject)inputAttribute;
		#endif
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node variable gameobject instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node variable gameobject
		connections.Add(new BlueprintConnectionAttributeOutputGameObject());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, false);
	}

	//Generates and returns a valid input connection for the blueprint node variable gameobject
	public override BlueprintConnection GenerateInputConnection()
	{
		//Return a valid input connection for the blueprint node variable gameobject
		return new BlueprintConnectionAttributeInputGameObject();
	}

	//Returns the blueprint node variable gameobject title
	protected override string GetTitle()
	{
		//Return the blueprint node variable gameobject title
		return "GameObject";
	}

	//Links the specified GameObject to the blueprint node variable GameObject and returns true if successful
	private bool LinkGameObjectToBlueprint(GameObject gameObject)
	{
		//If the GameObject is not valid
		if (gameObject == null)
		{
			//Reset attributes
			isPrefab = false;
			prefabFilepath = "";
			instanceTag = "";
			instanceName = "";
			instanceID = "";

			//Update the attribute value
			attributeValue = gameObject;

			//Successfully linked the GameObject to the blueprint
			return true;
		}

		//If the GameObject is a prefab
		if (gameObject.scene.name == null)
		{
			//Update attributes
			isPrefab = true;
			prefabFilepath = AssetDatabase.GetAssetPath(gameObject);
			instanceTag = "";
			instanceName = "";
			instanceID = "";

			//Update the attribute value
			attributeValue = gameObject;

			//Successfully linked the GameObject to the blueprint
			return true;
		}
		//Otherwise, if the GameObject is not the blueprints parent GameObject
		else if(Selection.activeGameObject != gameObject)
		{
			//If the tag is valid
			if (!string.IsNullOrEmpty(gameObject.tag) && gameObject.tag != "Untagged")
			{
				//Create a reference to the GameObjects blueprint UID
				BlueprintUID gameObjectID = gameObject.GetComponent<BlueprintUID>();

				//If the GameObjects blueprint ID is not valid
				if (gameObjectID == null)
				{
					//Create and initialize blueprint UID for the GameObject
					gameObjectID = gameObject.AddComponent<BlueprintUID>();
					gameObjectID.ID = BlueprintIdentificationHelper.GenerateID();
				}

				//Update attributes for instance
				isPrefab = false;
				prefabFilepath = "";
				instanceTag = gameObject.tag;
				instanceName = gameObject.name;
				instanceID = gameObjectID.ID;

				//Update the attribute value
				attributeValue = gameObject;

				//Successfully linked the GameObject to the blueprint
				return true;
			}
			//Otherwise
			else
			{
				//Log warning
				Debug.LogWarning("WARNING: Linking an untagged GameObject in the scene hierachy is not allowed, set the GameObjects tag and try again.");

				//Reset attribute value
				attributeValue = null;
			}
		}
		//Otherwise
		else
		{
			//Log warning
			Debug.LogWarning("WARNING: Linking the parent GameObject is not allowed, try linking a different GameObject.");

			//Reset attribute value
			attributeValue = null;
		}

		//Failed to link the GameObject to the blueprint
		return false;
	}

	//Renders the blueprint node variable gameobject body components
	protected override void RenderBodyComponents()
	{
		//Perform base render body components
		base.RenderBodyComponents();

		//Render the gameobject label
		BeginSection(2);
			GUILayout.Label("GameObject : object", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}

	//Renders the blueprint node variable gameobject inspector and returns true if changes to any attribute occurred
	public override bool RenderInspector()
	{
		//Begin horizontal group
		GUILayout.BeginHorizontal();

		//Render the variable name label
		GUILayout.Label(variableName);

		//Render GameObject field
		GameObject gameObject = (GameObject)EditorGUILayout.ObjectField((GameObject)GetAttribute(), typeof(GameObject), true);

		//End horizontal group
		GUILayout.EndHorizontal();

		//If the GameObject has changed
		if (gameObject != (GameObject)GetAttribute())
		{
			//Link the GameObject to the blueprint
			if (LinkGameObjectToBlueprint(gameObject))
			{
				//Changes occurred
				return true;
			}
		}

		//No changes occurred
		return false;
	}

	//Renders the blueprint node variable gameobject type name information
	public override void RenderTypeNameInformation()
	{
		//Render the blueprint node variable gameobject type name information
		GUILayout.Label("GameObject : " + variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}