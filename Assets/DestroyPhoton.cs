using UnityEngine;
using System.Collections;

public class DestroyPhoton : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll){
		Debug.Log ("HIT MEH");
		Destroy(coll.gameObject);
	}
}
