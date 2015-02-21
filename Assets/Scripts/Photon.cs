using UnityEngine;
using System.Collections;

public class Photon : MonoBehaviour {
	public GameManager GM;

	//DELETE this constant later, just for testing photon to move left/right
	const float SPEED = 100;

	// Use this for initialization
	void Start () {
		//rigidbody2D.velocity = GM.PhotonSpeed;
		rigidbody2D.AddForce (Vector3.left * SPEED);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
