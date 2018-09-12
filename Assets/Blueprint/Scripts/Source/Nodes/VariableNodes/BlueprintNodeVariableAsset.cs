//If running in the Unity Editor
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlueprintNodeVariableAsset<T1, T2, T3> : BlueprintNodeVariable where T1 : Object where T2 : BlueprintConnectionAttribute, new() where T3 : BlueprintConnectionAttribute, new()
{
	//Public attributes
	public string assetFilepath = "";
	public string assetName = "";

	//Protected attributes
	protected Object asset = null;

	//Links the variables asset instance
	protected void LinkAssetInstance()
	{
		//If the asset filepath is valid
		if (!string.IsNullOrEmpty(assetFilepath))
		{
			//Link the asset
			asset = Resources.Load<T1>(BlueprintPathHelper.ConvertToAssetResourcePath(assetFilepath));
		}
	}

	//Returns the blueprint node variable asset output attribute
	public override object GetAttribute()
	{
		//If the asset is not valid
		if (asset == null)
		{
			//Link the asset instance
			LinkAssetInstance();
		}

		//Return output attribute
		return asset;
	}

	//Sets the blueprint node variable asset output attribute to the specified input attribute
	public override void SetAttribute(object inputAttribute)
	{
		//If running in the Unity Editor
		#if UNITY_EDITOR
			//If the application is playing
			if (Application.isPlaying)
			{
				//Set the blueprint node variable asset output attribute to the specified input attribute
				asset = (T1)inputAttribute;
			}
			//Otherwise
			else
			{
				//Link the asset to the blueprint
				LinkAssetToBlueprint((T1)inputAttribute);
			}
		#else
			//Set the blueprint node asset output attribute to the specified input attribute
			asset = (T1)inputAttribute;
		#endif
	}

	//Sets the blueprint node asset name
	public void SetAssetName(string name)
	{
		//Set the blueprint node asset name
		assetName = name;
	}

//If running in the Unity Editor
#if UNITY_EDITOR
	//Initializes the blueprint node variable asset instance
	public override void Initialize()
	{
		//Perform base initialization
		base.Initialize();

		//Initialize the blueprint node variable asset
		connections.Add(new T3());
		connections[connections.Count - 1].Initialize(blueprintID, nodeID, 1, false);
	}

	//Generates and returns a valid input connection for the blueprint node asset
	public override BlueprintConnection GenerateInputConnection()
	{
		//Return a valid input connection for the blueprint variable asset
		return new T2();
	}

	//Returns the blueprint node variable asset title
	protected override string GetTitle()
	{
		//Return the blueprint node variable asset title
		return assetName;
	}

	//Links the specified asset to the blueprint node variable asset and returns true if successful
	private bool LinkAssetToBlueprint(T1 asset)
	{
		//If the asset is not valid
		if (asset == null)
		{
			//Update attributes
			assetFilepath = "";

			//Update the asset
			this.asset = asset;

			//Successfully linked the asset to the blueprint
			return true;
		}
		//Otherwise
		else
		{
			//Update attributes
			assetFilepath = AssetDatabase.GetAssetPath(asset);

			//Update the asset
			this.asset = asset;

			//Successfully linked the asset to the blueprint
			return true;
		}
	}

	//Renders the blueprint node asset body components
	protected override void RenderBodyComponents()
	{
		//Perform base render body components
		base.RenderBodyComponents();

		//Render the asset label
		BeginSection(2);
			GUILayout.Label(assetName + " : " + "asset", BlueprintStyleHelper.GetNodeAttributeTextStyle());
		EndSection();
	}

	//Renders the blueprint node variable asset inspector and return true if changes to any attribute occurred
	public override bool RenderInspector()
	{
		//Begin horizontal group
		GUILayout.BeginHorizontal();

		//Render the variable name label
		GUILayout.Label(variableName);

		//Render asset field
		T1 asset = (T1)EditorGUILayout.ObjectField((T1) GetAttribute(), typeof(T1), false);

		//End horizontal group
		GUILayout.EndHorizontal();

		//If the asset has changed
		if (asset != (T1)GetAttribute())
		{
			//Link the asset to the blueprint
			if (LinkAssetToBlueprint(asset))
			{
				//Changes occurred
				return true;
			}
		}

		//No changes occurred
		return false;
	}

	//Renders the blueprint node variable asset type name information
	public override void RenderTypeNameInformation()
	{
		//Render the blueprint node variable asset type name information
		GUILayout.Label(assetName + " : " + variableName, BlueprintStyleHelper.GetNodeAttributeTextStyle());
	}
#endif
}