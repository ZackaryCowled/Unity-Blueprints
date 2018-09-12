using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BlueprintInputEventHandler : MonoBehaviour
{
	//Public attributes
	public UnityEvent onMouseOverEvent = new UnityEvent();

	//Called on mouse over GameObject
	private void OnMouseOver()
	{
		//Execute associated on mouse over event nodes
		onMouseOverEvent.Invoke();
	}
}