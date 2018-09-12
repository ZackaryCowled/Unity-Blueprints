using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class Blueprint
{
	//Private attributes
	private List<BlueprintNode> blueprintNodes = new List<BlueprintNode>();
	private JsonSerializerSettings jsonSerializerSettings;

	//Creates and initializes a blueprint instance
	public Blueprint()
	{
		//Configure JSON serializer
		ConfigureJSONSerializer();
	}

	//Configures the blueprints JSON serializer
	private void ConfigureJSONSerializer()
	{
		//Configure Json serializer settings
		jsonSerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All,
			ReferenceLoopHandling = ReferenceLoopHandling.Serialize
		};
	}

	//Returns the blueprint node with the specified ID
	public BlueprintNode GetBlueprintNodeAt(int id)
	{
		//Return the blueprint node with the specified ID
		return blueprintNodes[id];
	}

	//Returns the number of blueprint nodes in the blueprint
	public int GetBlueprintNodeCount()
	{
		//Return the number of blueprint nodes in the blueprint
		return blueprintNodes.Count;
	}

	//Loads the blueprint from the specified blueprint JSON string
	public void Load(string blueprintJSON)
	{
		//Deserialize the blueprint data
		blueprintNodes = JsonConvert.DeserializeObject<List<BlueprintNode>>(blueprintJSON, jsonSerializerSettings);
	}

	//Loads the blueprint from the blueprint at the specified blueprint filepath as a resource
	//NOTE: The blueprint file must be located in a directory with the name "Resources"
	public void LoadFromResources(string blueprintFilepath)
	{
		//Load the blueprint file
		TextAsset blueprintJSON = Resources.Load<TextAsset>(BlueprintPathHelper.ConvertToBlueprintResourcePath(blueprintFilepath));

		//If the blueprint JSON file failed to load
		if (blueprintJSON == null)
		{
			//Log warning
			Debug.LogWarning("WARNING: Blueprint.cs failed to load the blueprint at " + BlueprintPathHelper.ConvertToBlueprintResourcePath(blueprintFilepath));
		}
		//Otherwise
		else
		{
			//Deserialize the blueprint data
			blueprintNodes = JsonConvert.DeserializeObject<List<BlueprintNode>>(blueprintJSON.text, jsonSerializerSettings);
		}
	}

	//Sets the blueprints ID to the specified ID
	public void SetID(int id)
	{
		//For each blueprint node
		for (int blueprintNodeIndex = 0; blueprintNodeIndex < blueprintNodes.Count; blueprintNodeIndex++)
		{
			//Set the blueprint ID to the specified ID
			blueprintNodes[blueprintNodeIndex].blueprintID = id;
		}
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Private attributes
	private string filepath;

	//Creates and initializes a blueprint instance from the specified blueprint file
	//filepath - The location, name and extension of the blueprint file
	public Blueprint(string filepath)
	{
		//Initialize blueprint
		this.filepath = filepath;

		//Configure JSON serializer
		ConfigureJSONSerializer();
	}

	//Adds the specified blueprint node to the blueprint at the specified position
	public void AddBlueprintNode(BlueprintNode blueprintNode, Vector2 position)
	{
		//Add the specified blueprint node to the blueprint
		blueprintNodes.Add(blueprintNode);

		//Set the blueprint nodes ID
		blueprintNodes[blueprintNodes.Count - 1].nodeID = blueprintNodes.Count - 1;

		//Set the blueprint nodes position to the specified position
		blueprintNodes[blueprintNodes.Count - 1].Translate(position);

		//Initialize the blueprint node
		blueprintNodes[blueprintNodes.Count - 1].Initialize();
	}

	//TODO: Add attribute asset function...
	//TODO: Add attribute component function...

	//Adds the specified blueprint node connection one to many to the blueprint at the specified position
	public void AddBlueprintNodeConnectionOneToMany<T1, T2>(string componentName, Vector2 position) where T1 : BlueprintConnectionAttribute, new() where T2 : BlueprintConnectionAttribute, new()
	{
		//Add the specified blueprint node to the blueprint
		blueprintNodes.Add(new BlueprintNodeConnectionVariableComponentOneToMany<T1, T2>());

		//Set the blueprint nodes ID
		blueprintNodes[blueprintNodes.Count - 1].nodeID = blueprintNodes.Count - 1;

		//Set the blueprint nodes position to the specified position
		blueprintNodes[blueprintNodes.Count - 1].Translate(position);

		//Set the blueprint node connection one to many variable component component name
		((BlueprintNodeConnectionVariableComponentOneToMany<T1, T2>)blueprintNodes[blueprintNodes.Count - 1]).SetComponentName(componentName);

		//Initialize the blueprint node
		blueprintNodes[blueprintNodes.Count - 1].Initialize();
	}

	//Adds the specified blueprint node variable asset component to the blueprint at the specified position
	public void AddBlueprintNodeVariableAsset<T1, T2, T3>(string assetName, Vector2 position) where T1 : Object where T2 : BlueprintConnectionAttribute, new() where T3 : BlueprintConnectionAttribute, new()
	{
		//Add the specified blueprint node to the blueprint
		blueprintNodes.Add(new BlueprintNodeVariableAsset<T1, T2, T3>());

		//Set the blueprint nodes ID
		blueprintNodes[blueprintNodes.Count - 1].nodeID = blueprintNodes.Count - 1;

		//Set the blueprint nodes position to the specified position
		blueprintNodes[blueprintNodes.Count - 1].Translate(position);

		//Set the blueprint node variable asset name
		((BlueprintNodeVariableAsset<T1, T2, T3>) blueprintNodes[blueprintNodes.Count - 1]).SetAssetName(assetName);

		//Initialize the blueprint node
		blueprintNodes[blueprintNodes.Count - 1].Initialize();
	}

	//Adds the specified blueprint node variable component to the blueprint at the specified position
	public void AddBlueprintNodeVariableComponent<T1, T2, T3>(string componentName, Vector2 position) where T1 : Component where T2 : BlueprintConnectionAttribute, new() where T3 : BlueprintConnectionAttribute, new()
	{
		//Add the specified blueprint node to the blueprint
		blueprintNodes.Add(new BlueprintNodeVariableComponent<T1, T2, T3>());

		//Set the blueprint nodes ID
		blueprintNodes[blueprintNodes.Count - 1].nodeID = blueprintNodes.Count - 1;

		//Set the blueprint nodes position to the specified position
		blueprintNodes[blueprintNodes.Count - 1].Translate(position);

		//Set the blueprint node variable component name
		((BlueprintNodeVariableComponent<T1, T2, T3>)blueprintNodes[blueprintNodes.Count - 1]).SetComponentName(componentName);

		//Initialize the blueprint node
		blueprintNodes[blueprintNodes.Count - 1].Initialize();
	}

	//Adds the specified custom blueprint node to the blueprint
	public void AddCustomBlueprintNode(string blueprintFilepath, Vector2 position)
	{
		//Add the specified blueprint node to the blueprint
		blueprintNodes.Add(new BlueprintNodeBlueprint());

		//Set the blueprint nodes ID
		blueprintNodes[blueprintNodes.Count - 1].nodeID = blueprintNodes.Count - 1;

		//Set the blueprint nodes position to the specified position
		blueprintNodes[blueprintNodes.Count - 1].Translate(position);

		//Set the blueprint nodes blueprint filepath
		((BlueprintNodeBlueprint)blueprintNodes[blueprintNodes.Count - 1]).blueprintFilepath = blueprintFilepath;

		//Initialize the blueprint node
		blueprintNodes[blueprintNodes.Count - 1].Initialize();
	}

	//Converts and returns the blueprint nodes as a JSON string
	public string ConvertToJSON()
	{
		//Convert and return the blueprint nodes as a JSON string 
		return JsonConvert.SerializeObject(blueprintNodes, jsonSerializerSettings);
	}

	//Destroys the specified blueprint node
	public void DestroyBlueprintNode(BlueprintNode blueprintNode)
	{
		//For each blueprint node
		for(int blueprintNodeIndex = 0; blueprintNodeIndex < blueprintNodes.Count; blueprintNodeIndex++)
		{
			//If the blueprint node matches the specified blueprint node
			if(blueprintNodes[blueprintNodeIndex].nodeID == blueprintNode.nodeID)
			{
				//Destroy the blueprint node
				blueprintNodes[blueprintNodeIndex] = null;

				//Optimize the blueprint data
				Optimize();

				//Return from the function
				return;
			}
		}
	}

	//Returns the name of the blueprint
	public string GetName()
	{
		//Return the name of the blueprint
		return BlueprintPathHelper.GetNameFromPath(filepath);
	}

	//Optimizes the blueprint data
	private void Optimize()
	{
		//For each blueprint node
		for (int blueprintNodeIndex = 0; blueprintNodeIndex < blueprintNodes.Count; blueprintNodeIndex++)
		{
			//If the blueprint node is not valid
			if(blueprintNodes[blueprintNodeIndex] == null)
			{
				//For each blueprint node
				for(int nodeIndex = 0; nodeIndex < blueprintNodes.Count; nodeIndex++)
				{
					//If the blueprint node is valid
					if(blueprintNodes[nodeIndex] != null)
					{
						//If the blueprint nodes node ID is higher then the blueprint node index
						if(blueprintNodes[nodeIndex].nodeID > blueprintNodeIndex)
						{
							//Decrement the blueprint node ID
							blueprintNodes[nodeIndex].nodeID -= 1;
						}

						//If the blueprint nodes input execution connection is valid
						if(blueprintNodes[nodeIndex].inputExecutionConnection != null)
						{
							//If the blueprint nodes input execution connection node ID is higher then the blueprint node index
							if(blueprintNodes[nodeIndex].inputExecutionConnection.nodeID > blueprintNodeIndex)
							{
								//Decrement the blueprint nodes input execution connection node ID
								blueprintNodes[nodeIndex].inputExecutionConnection.nodeID -= 1;
							}

							//If the blueprint nodes input execution connection connection node ID is higher then the blueprint node index
							if(blueprintNodes[nodeIndex].inputExecutionConnection.connectionNodeID > blueprintNodeIndex)
							{
								//Decrement the blueprint nodes input execution connection node connection node ID
								blueprintNodes[nodeIndex].inputExecutionConnection.connectionNodeID -= 1;
							}
						}

						//If the blueprint nodes output execution connection is valid
						if (blueprintNodes[nodeIndex].outputExecutionConnection != null)
						{
							//If the blueprint nodes output execution connection node ID is higher then the blueprint node index
							if (blueprintNodes[nodeIndex].outputExecutionConnection.nodeID > blueprintNodeIndex)
							{
								//Decrement the blueprint nodes output execution connection node ID
								blueprintNodes[nodeIndex].outputExecutionConnection.nodeID -= 1;
							}

							//If the blueprint nodes output execution connection connection node ID is higher then the blueprint node index
							if (blueprintNodes[nodeIndex].outputExecutionConnection.connectionNodeID > blueprintNodeIndex)
							{
								//Decrement the blueprint nodes output execution connection node connection node ID
								blueprintNodes[nodeIndex].outputExecutionConnection.connectionNodeID -= 1;
							}
						}

						//For each blueprint node connection
						for(int connectionIndex = 0; connectionIndex < blueprintNodes[nodeIndex].connections.Count; connectionIndex++)
						{
							//If the blueprint nodes connection is valid
							if (blueprintNodes[nodeIndex].connections[connectionIndex] != null)
							{
								//If the blueprint nodes connection node ID is higher then the blueprint node index
								if (blueprintNodes[nodeIndex].connections[connectionIndex].nodeID > blueprintNodeIndex)
								{
									//Decrement the blueprint nodes connection node ID
									blueprintNodes[nodeIndex].connections[connectionIndex].nodeID -= 1;
								}

								//If the blueprint nodes connection connection node ID is higher then the blueprint node index
								if (blueprintNodes[nodeIndex].connections[connectionIndex].connectionNodeID > blueprintNodeIndex)
								{
									//Decrement the blueprint nodes connection node connection node ID
									blueprintNodes[nodeIndex].connections[connectionIndex].connectionNodeID -= 1;
								}
							}
						}
					}
				}

				//Remove the blueprint node
				blueprintNodes.RemoveAt(blueprintNodeIndex);

				//Decrement the blueprint node index
				blueprintNodeIndex -= 1;
			}
		}
	}

	//Renders the blueprint
	public void Render()
	{
		//For each blueprint node
		for(int blueprintNodeIndex = 0; blueprintNodeIndex < blueprintNodes.Count; blueprintNodeIndex++)
		{
			//If the blueprint node is valid
			if (blueprintNodes[blueprintNodeIndex] != null)
			{
				//Render the blueprint node
				blueprintNodes[blueprintNodeIndex].Render();
			}
		}
	}

	//Saves the blueprint
	public void Save()
	{
		//Serialize blueprint data
		string blueprintJSON = JsonConvert.SerializeObject(blueprintNodes, jsonSerializerSettings);

		//Save the blueprint data to the blueprint filepath
		File.WriteAllText(filepath, blueprintJSON);
	}

	//Loads the blueprint
	public void Load()
	{
		//Load the blueprint JSON file
		TextAsset blueprintJSON = Resources.Load<TextAsset>(BlueprintPathHelper.ConvertToBlueprintResourcePath(filepath));

		//If the blueprint JSON file failed to load
		if (blueprintJSON == null)
		{
			//Log warning
			Debug.LogWarning("WARNING: Blueprint.cs failed to load the blueprint at " + BlueprintPathHelper.ConvertToBlueprintResourcePath(filepath));
		}
		//Otherwise
		else
		{
			//Deserialize the blueprint data
			blueprintNodes = JsonConvert.DeserializeObject<List<BlueprintNode>>(blueprintJSON.text, jsonSerializerSettings);
		}
	}

	//Translates all blueprint nodes in the blueprint by the specified translation
	public void Translate(Vector2 translation)
	{
		//For each blueprint node
		for(int blueprintNodeIndex = 0; blueprintNodeIndex < blueprintNodes.Count; blueprintNodeIndex++)
		{
			//If the blueprint node is valid
			if (blueprintNodes[blueprintNodeIndex] != null)
			{
				//Translate the blueprint node by the specified translation
				blueprintNodes[blueprintNodeIndex].Translate(translation);
			}
		}
	}
#endif
}