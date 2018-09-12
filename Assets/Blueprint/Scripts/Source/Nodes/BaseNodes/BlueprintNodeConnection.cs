using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeConnection : BlueprintNode
{
	//Returns the blueprint node connections output attribute
	public override object GetAttribute()
	{
		//If the input connections connection node ID is valid
		if(connections[0].connectionNodeID > -1)
		{
			//Return the input connection nodes output attribute
			return BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).GetAttribute();
		}

		//Return the default output attribute
		return GetDefaultOutputAttribute();
	}

	//Sets the blueprint node connections input attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//If the input connections connection node ID is valid
		if (connections[0].connectionNodeID > -1)
		{
			//Set the blueprint node connections input attribute to the specified input attribute
			BlueprintInstanceManager.GetBlueprintAt(blueprintID).GetBlueprintNodeAt(connections[0].connectionNodeID).SetAttribute(inputAttribute);
		}
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Adds an output connection of the specified type to the blueprint node
	//NOTE: Only call after the input connection has been created
	protected void AddOutputConnection<T>() where T : BlueprintConnection, new()
	{
		//Add output connection of the specified type to the blueprint node
		connections.Add(new T());

		//Initialize the output connection
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, connections.Count -1, false);
	}

	//Returns the blueprint node connection header color
	protected override Color GetHeaderColor()
	{
		//Return the blueprint node connection header color
		return new Color(0.25f, 0.25f, 0.25f);
	}

	//Renders the blueprint node connection one to many components for the specified blueprint connection output
	protected void RenderBodyComponentsOneToMany<T>() where T : BlueprintConnection, new()
	{
		//Begin section
		BeginSection(1);

		//If the developer presses the add output button
		if (GUILayout.Button("Add Output"))
		{
			//Add an output connection
			AddOutputConnection<T>();
		}

		//End section
		EndSection();

		//For each additional connection
		for (int connectionIndex = 2; connectionIndex < connections.Count; connectionIndex++)
		{
			//Begin section
			BeginSection(connectionIndex);

			//If the developer presses the remove output button
			if (GUILayout.Button("Remove Output"))
			{
				//Remove the output connection
				connections[connectionIndex].DestroyConnection();
				connections.RemoveAt(connectionIndex);

				//For each connection from the connection index
				for (int postConnectionIndex = connectionIndex; postConnectionIndex < connections.Count; postConnectionIndex++)
				{
					//Decrement the connections section ID
					connections[postConnectionIndex].sectionID -= 1;

					//If the connection is connected to a valid blueprint node
					if(connections[postConnectionIndex].connectionNodeID > -1)
					{
						//Decrement the connections section ID for the external blueprint node
						BlueprintEditorManager.blueprint.GetBlueprintNodeAt(connections[postConnectionIndex].connectionNodeID).GetConnection(connections[postConnectionIndex].connectionSectionID, connections[postConnectionIndex].connectionIsInput).connectionSectionID -= 1;
					}
				}
			}

			//End section
			EndSection();
		}
	}
#endif
}