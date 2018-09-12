//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeGetGetBlueprintVariable : BlueprintNodeGet
{
	//Public attributes
	public int selectedType = 0;

	//Returns the blueprint node get get blueprint variable output attribute
	public override object GetAttribute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1)
		{
			//Create reference to the blueprint input attribute
			Blueprint blueprintInputAttribute = BlueprintInstanceManager.GetBlueprintAt((int) BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute());

			//If the blueprint input attribute is valid
			if (blueprintInputAttribute != null)
			{
				//For each node in the blueprint input attribute
				for (int nodeIndex = 0; nodeIndex < blueprintInputAttribute.GetBlueprintNodeCount(); nodeIndex++)
				{
					//If the blueprint node is a variable
					if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariable != null)
					{
						//If the blueprint node variable has a matching variable name
						if (((BlueprintNodeVariable) blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex)).variableName == (string) BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[1].connectionNodeID).GetAttribute())
						{
							//Depending on the selected type
							switch (selectedType)
							{
								//Bool
								case 0:
								{
									//If the blueprint node is a variable bool
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableBool != null)
									{
										//Return output attribute
										return blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex).GetAttribute();
									}
									break;
								}

								//Float
								case 1:
								{
									//If the blueprint node is a variable float
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableFloat != null)
									{
										//Return output attribute
										return blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex).GetAttribute();
									}
									break;
								}

								//GameObject
								case 2:
								{
									//If the blueprint node is a variable GameObject
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableGameObject != null)
									{
										//Return output attribute
										return blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex).GetAttribute();
									}
									break;
								}

								//Int
								case 3:
								{
									//If the blueprint node is a variable int
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableInt != null)
									{
										//Return output attribute
										return blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex).GetAttribute();
									}
									break;
								}

								//Quaternion
								case 4:
								{
									//If the blueprint node is a variable Quaternion
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableQuaternion != null)
									{
										//Return output attribute
										return blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex).GetAttribute();
									}
									break;
								}

								//String
								case 5:
								{
									//If the blueprint node is a variable string
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableString != null)
									{
										//Return output attribute
										return blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex).GetAttribute();
									}
									break;
								}

								//Vector2
								case 6:
								{
									//If the blueprint node is a variable Vector2
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableVector2 != null)
									{
										//Return output attribute
										return blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex).GetAttribute();
									}
									break;
								}

								//Vector3
								case 7:
								{
									//If the blueprint node is a variable Vector3
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableVector3 != null)
									{
										//Return output attribute
										return blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex).GetAttribute();
									}
									break;
								}

								//Vector4
								case 8:
								{
									//If the blueprint node is a variable Vector4
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableVector4 != null)
									{
										//Return output attribute
										return blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex).GetAttribute();
									}
									break;
								}
							}
						}
					}
				}
			}
		}

		//Return the default output attribute
		return GetDefaultOutputAttribute();
	}

	//Returns the blueprint node get get blueprint variable default output attribute
	public override object GetDefaultOutputAttribute()
	{
		//Returns the blueprint node get get blueprint variable default output attribute
		return null;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private static readonly string[] types = new string[]
	{
		"Bool",
		"Float",
		"GameObject",
		"Int",
		"Quaternion",
		"String",
		"Vector2",
		"Vector3",
		"Vector4"
	};

	//Initializes the blueprint node get get blueprint variable instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node get get blueprint variable
		connections.Add(new BlueprintConnectionAttributeInputBlueprint());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeInputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 3, true);
		connections.Add(new BlueprintConnectionAttributeOutput());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);

		//Update output connection
		UpdateOutputConnection(-1);
	}

	//Returns the blueprint node get get blueprint variable title
	protected override string GetTitle()
	{
		//Return the blueprint node get get blueprint variable title
		return "GetBlueprintVariable";
	}

	//Returns the blueprint node get get blueprint variable body components
	protected override void RenderBodyComponents()
	{
		//Store previous selected type
		int previousSelectedType = selectedType;

		//Render the type dropdown menu
		BeginSection(1);
			selectedType = EditorGUILayout.Popup(selectedType, types);
		EndSection();

		//Update the output connection
		UpdateOutputConnection(previousSelectedType);

		//Render the Blueprint blueprint label
		BeginSection(2);
			GUILayout.Label("Blueprint : blueprint", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the string variable name label
		BeginSection(3);
			GUILayout.Label("String : variableName", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}

	//Updates the output connection to work with the selected type
	private void UpdateOutputConnection(int previousSelectedType)
	{
		//If the selected type changed
		if (previousSelectedType != selectedType)
		{
			//Destroy the output connections connections
			connections[2].DestroyConnection();

			//Depending on the selected type
			switch (selectedType)
			{
				//Bool
				case 0:
				{
					//Set the output connection as bool
					connections[2] = new BlueprintConnectionAttributeOutputBool();
					break;
				}

				//Float
				case 1:
				{
					//Set the output connection as float
					connections[2] = new BlueprintConnectionAttributeOutputFloat();
					break;
				}

				//GameObject
				case 2:
				{
					//Set the output connection as GameObject
					connections[2] = new BlueprintConnectionAttributeOutputGameObject();
					break;
				}

				//Int
				case 3:
				{
					//Set the output connection as int
					connections[2] = new BlueprintConnectionAttributeOutputInt();
					break;
				}

				//Quaternion
				case 4:
				{
					//Set the output connection as Quaternion
					connections[2] = new BlueprintConnectionAttributeOutputQuaternion();
					break;
				}

				//String
				case 5:
				{
					//Set the output connection as string
					connections[2] = new BlueprintConnectionAttributeOutputString();
					break;
				}

				//Vector2
				case 6:
				{
					//Set the output connection as Vector2
					connections[2] = new BlueprintConnectionAttributeOutputVector2();
					break;
				}

				//Vector3
				case 7:
				{
					//Set the output connection as Vector3
					connections[2] = new BlueprintConnectionAttributeOutputVector3();
					break;
				}

				//Vector4
				case 8:
				{
					//Set the output connection as Vector4
					connections[2] = new BlueprintConnectionAttributeOutputVector4();
					break;
				}
			}

			//Initialize the output connection
			connections[2].Initialize(blueprintID, nodeID, 1, false);
		}
	}
#endif
}