using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BlueprintCollisionEventHandler : MonoBehaviour
{
	//Public attributes
	public UnityEvent onCollisionEnterEvent = new UnityEvent();
	public UnityEvent onCollisionStayEvent = new UnityEvent();
	public UnityEvent onCollisionExitEvent = new UnityEvent();

	//Called on collision enter
	private void OnCollisionEnter(Collision collision)
	{
		//Execute associated on collision enter events nodes
		onCollisionEnterEvent.Invoke();
	}

	//Called on collision stay
	private void OnCollisionStay(Collision collision)
	{
		//Execute associated on collision stay event nodes
		onCollisionStayEvent.Invoke();
	}

	//Called on collision exit
	private void OnCollisionExit(Collision collision)
	{
		//Executes associated on collision exit event nodes
		onCollisionExitEvent.Invoke();
	}
}