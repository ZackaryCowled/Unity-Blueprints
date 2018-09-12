using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlueprintIdentificationHelper
{
	//Generates and returns a unique ID
	public static string GenerateID()
	{
		//Generate and return a unique ID
		return Guid.NewGuid().ToString();
	}
}