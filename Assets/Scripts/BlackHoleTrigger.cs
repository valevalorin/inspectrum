using UnityEngine;
using System.Collections;

public class BlackHoleTrigger : MonoBehaviour {

	public bool isTriggered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		isTriggered = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		isTriggered = false;
	}
}
