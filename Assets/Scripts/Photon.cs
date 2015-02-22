using UnityEngine;
using System.Collections;

public class Photon : MonoBehaviour {
	private GameManager GM;

	//DELETE this constant later, just for testing photon to move left/right
	public float SPEED = 100;

	void Awake(){
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// Use this for initialization
	void Start () {
//		rigidbody2D.velocity = GM.PhotonSpeed;
		rigidbody2D.AddForce (new Vector2(GM.PhotonSpeed, 0f));
		Debug.Log (GM.PhotonSpeed.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
	}
}
