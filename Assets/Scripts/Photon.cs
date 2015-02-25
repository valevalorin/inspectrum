using UnityEngine;
using System.Collections;

public class Photon : MonoBehaviour {
	private GameManager GM;

	void Awake(){
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// Use this for initialization
	void Start () {
		rigidbody2D.AddForce (new Vector2(GM.PhotonSpeed, 0f));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
