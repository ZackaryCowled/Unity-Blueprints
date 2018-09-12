//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeSetSetBlueprintVariable : BlueprintNodeSet
{
	//Public attributes
	public int selectedType = 0;

	//Executes the blueprint node set set variable instance
	public override void Execute()
	{
		//If dependent attribute connections are valid
		if (connections[0].connectionNodeID > -1 && connections[1].connectionNodeID > -1 && connections[2].connectionNodeID > -1)
		{
			//Create reference to the blueprint input attribute
			Blueprint blueprintInputAttribute = BlueprintInstanceManager.GetBlueprintAt((int)BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute());
			
			//If the blueprint input attribute is valid
			if (blueprintInputAttribute != null)
			{
				//Flag indicating whether the blueprint variable has been found
				bool foundBlueprintVariable = false;

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
										//Set the found blueprint variable flag to true
										foundBlueprintVariable = true;
									}

									break;
								}

								//Float
								case 1:
								{
									//If the blueprint node is a variable float
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableFloat != null)
									{
										//Set the found blueprint variable flag to true
										foundBlueprintVariable = true;
									}

									break;
								}

								//GameObject
								case 2:
								{
									//If the blueprint node is a variable GameObject
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableGameObject != null)
									{
										//Set the found blueprint variable flag to true
										foundBlueprintVariable = true;
									}

									break;
								}

								//Int
								case 3:
								{
									//If the blueprint node is a variable int
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableInt != null)
									{
										//Set the found blueprint variable flag to true
										foundBlueprintVariable = true;
									}

									break;
								}

								//Quaternion
								case 4:
								{
									//If the blueprint node is a variable Quaternion
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableQuaternion != null)
									{
										//Set the found blueprint variable flag to true
										foundBlueprintVariable = true;
									}

									break;
								}

								//String
								case 5:
								{
									//If the blueprint node is a variable string
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableString != null)
									{
										//Set the found blueprint variable flag to true
										foundBlueprintVariable = true;
									}

									break;
								}

								//Vector2
								case 6:
								{
									//If the blueprint node is a variable Vector2
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableVector2 != null)
									{
										//Set the found blueprint variable flag to true
										foundBlueprintVariable = true;
									}

									break;
								}

								//Vector3
								case 7:
								{
									//If the blueprint node is a variable Vector3
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableVector3 != null)
									{
										//Set the found blueprint variable flag to true
										foundBlueprintVariable = true;
									}

									break;
								}

								//Vector4
								case 8:
								{
									//If the blueprint node is a variable Vector4
									if (blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex) as BlueprintNodeVariableVector4 != null)
									{
										//Set the found blueprint variable flag to true
										foundBlueprintVariable = true;
									}

									break;
								}
							}

							//If the blueprint variable is found
							if (foundBlueprintVariable)
							{
								//Set the blueprint nodes output attribute to the input attribute
								blueprintInputAttribute.GetBlueprintNodeAt(nodeIndex).SetAttribute(BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[2].connectionNodeID).GetAttribute());
								break;
							}
						}
					}
				}
			}
		}

		//Execute the output execution node
		ExecuteOutputExecutionNode();
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

	//Initializes the blueprint node set blueprint variable instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node set set blueprint variable
		connections.Add(new BlueprintConnectionAttributeInputBlueprint());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 2, true);
		connections.Add(new BlueprintConnectionAttributeInputString());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 3, true);
		connections.Add(new BlueprintConnectionAttributeInput());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 4, true);

		//Update input connection
		UpdateInputConnection(-1);
	}

	//Returns the blueprint node set set blueprint variable title
	protected override string GetTitle()
	{
		//Return the blueprint node set set blueprint variable title
		return "SetBlueprintVariable";
	}

	//Returns the blueprint node set set blueprint variable type name
	protected override string GetTypeName()
	{
		//Return the blueprint node set set blueprint variable type name
		return types[selectedType];
	}

	//Renders the blueprint node set set blueprint variable body components
	protected override void RenderBodyComponents()
	{
		//Store previous selected type
		int previousSelectedType = selectedType;

		//Render the type dropdown menu
		BeginSection(1);
			selectedType = EditorGUILayout.Popup(selectedType, types);
		EndSection();

		//Update the input connection
		UpdateInputConnection(previousSelectedType);

		//Render the blueprint label
		BeginSection(2);
			GUILayout.Label("Blueprint : blueprint", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the string variable name label
		BeginSection(3);
			GUILayout.Label("String : variableName", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();

		//Render the input value label
		BeginSection(4);
			GUILayout.Label(GetTypeName() + " : value", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}

	//Updates the input connection to work with the selected type
	private void UpdateInputConnection(int previousSelectedType)
	{
		//If the selected type changed
		if (previousSelectedType != selectedType)
		{
			//Destroy the input connections connections
			connections[2].DestroyConnection();

			//Depending on the selected type
			switch (selectedType)
			{
				//Bool
				case 0:
				{
					//Set the input connection as bool
					connections[2] = new BlueprintConnectionAttributeInputBool();
					break;
				}

				//Float
				case 1:
				{
					//Set the input connection as float
					connections[2] = new BlueprintConnectionAttributeInputFloat();
					break;
				}

				//GameObject
				case 2:
				{
					//Set the input connection as GameObject
					connections[2] = new BlueprintConnectionAttributeInputGameObject();
					break;
				}

				//Int
				case 3:
				{
					//Set the input connection as int
					connections[2] = new BlueprintConnectionAttributeInputInt();
					break;
				}

				//Quaternion
				case 4:
				{
					//Set the input connection as Quaternion
					connections[2] = new BlueprintConnectionAttributeInputQuaternion();
					break;
				}

				//String
				case 5:
				{
					//Set the input connection as string
					connections[2] = new BlueprintConnectionAttributeInputString();
					break;
				}

				//Vector2
				case 6:
				{
					//Set the input connection as Vector2
					connections[2] = new BlueprintConnectionAttributeInputVector2();
					break;
				}

				//Vector3
				case 7:
				{
					//Set the input connection as Vector3
					connections[2] = new BlueprintConnectionAttributeInputVector3();
					break;
				}

				//Vector4
				case 8:
				{
					//Set the input connection as Vector4
					connections[2] = new BlueprintConnectionAttributeInputVector4();
					break;
				}
			}

			//Initialize the input connection
			connections[2].Initialize(blueprintID, nodeID, 4, true);
		}
	}
#endif
}