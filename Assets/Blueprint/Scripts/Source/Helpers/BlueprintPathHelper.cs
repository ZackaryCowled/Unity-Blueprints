using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class BlueprintPathHelper
{
	//Returns the result of converting the specified filepath to a valid blueprint resource filepath
	public static string ConvertToBlueprintResourcePath(string filepath)
	{
		//Return the blueprint resource filepath for the specified filepath
		return ExcludeFileExtension(ExcludeResourcesPath(filepath));
	}

	//Returns the result of converting the specified filepath to a valid asset resource filepath
	public static string ConvertToAssetResourcePath(string filepath)
	{
		//Return the asset resource filepath for the specified filepath
		return ExcludeFileExtension(ExcludeResourcesPath(filepath));
	}

	//Returns the specified filepath without its file extension
	public static string ExcludeFileExtension(string filepath)
	{
		//If the filepath is valid
		if (filepath != null)
		{
			//Find the index of the last '.' character
			int fileExtensionIndex = filepath.LastIndexOf('.');

			//If the filepath has a valid file extension
			if (fileExtensionIndex > -1)
			{
				//Return the filepath with the file extension excluded
				return filepath.Substring(0, fileExtensionIndex);
			}
		}

		//The file extension could not be excluded
		return filepath;
	}

	//Returns the specified filepath removing any of the path upto the Resources directory
	public static string ExcludeResourcesPath(string filepath)
	{
		//If the filepath is valid
		if (filepath != null)
		{
			//Find the index of the last "Resources/" string
			int resourcesIndex = filepath.LastIndexOf("Resources/");

			//If the filepath has a valid resources path
			if (resourcesIndex > -1)
			{
				//Return the filepath with the resources path excluded
				return filepath.Substring(resourcesIndex + 10, filepath.Length - resourcesIndex - 11);
			}
		}

		//The resources path could not be excluded
		return filepath;
	}

	//Returns the name of the file the specified filepath is pointing at
	public static string GetNameFromPath(string filepath)
	{
		//If the filepath is valid
		if (filepath != null)
		{
			//Find the index of the last '.' character
			int fileExtensionIndex = filepath.LastIndexOf('.');

			//Find the index of the last '/' character
			int nameIndex = filepath.LastIndexOf('/');

			//If the filepath has a valid file extension
			if (fileExtensionIndex > -1)
			{
				//If the filepath has a valid path
				if (nameIndex > -1)
				{
					//If the file extension index is after the name index
					if (fileExtensionIndex > nameIndex)
					{
						//Return the name of the file from the filepath
						return filepath.Substring(nameIndex + 1, fileExtensionIndex - nameIndex - 1);
					}
				}
			}
			//Otherwise
			else
			{
				//If the filepath has a valid path
				if (nameIndex > -1)
				{
					//Return the name of the file from the filepath
					return filepath.Substring(nameIndex + 1, filepath.Length - nameIndex - 1);
				}
			}
		}

		//The filepath cannot be parsed or the filepath is a file name
		return filepath;
	}

	//Returns an array of all the blueprint filepaths
	public static string[] GetBlueprintFilepaths()
	{
		//Return an array of all the blueprint filepaths
		return Directory.GetFiles(Application.dataPath + "/Resources/Blueprints/", "*.txt");
	}
}