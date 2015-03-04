/**
 * Handle all misses with triggers
 **/
using UnityEngine;
using System.Collections;

public class MissTrigger : MonoBehaviour {

	private GameManager GM;
	private PhotonManager PM;

	// Use this for initialization
	void Start () {
		PM = (PhotonManager) GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>();
		GM = (GameManager) GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if(!other.CompareTag("DeadPhoton"))
		{
			other.tag = "DeadPhoton";
			GM.Miss();
			PM.PhotonQueue.Dequeue();
		}
	}
}
