/**
 * This class is to detect the triggers for determining user-input
 **/
using UnityEngine;
using System.Collections;

public class BlackHoleTrigger : MonoBehaviour {

	public bool isTriggered = false;

	//Determine if the triggers are active with photon	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(!other.gameObject.CompareTag("DeadPhoton"))
			isTriggered = true;
	}

	//Determine if the triggers are active with photon
	void OnTriggerExit2D(Collider2D other)
	{
		if(!other.gameObject.CompareTag("DeadPhoton"))
			isTriggered = false;
	}
}
