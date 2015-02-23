using UnityEngine;
using System.Collections;

public class DestroyPhoton : MonoBehaviour {
	public PhotonManager PM;

	void Start(){
		PM = (PhotonManager)GameObject.FindGameObjectWithTag ("PhotonManager").GetComponent<PhotonManager>();
	}

	void OnTriggerEnter2D(Collider2D coll){
		PM.PhotonQueue.Dequeue ();
		Destroy(coll.gameObject);
	}
}
