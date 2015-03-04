/**
 * Class to handle photon movement
 **/
using UnityEngine;
using System.Collections;

public class Photon : MonoBehaviour {
	private GameManager GM;

	// Instantiate
	void Awake()
	{
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// Use this for initialization
	void Start () 
	{
		//rigidbody2D.AddForce (new Vector2(GM.PhotonSpeed, 0f));
		rigidbody2D.velocity = new Vector2(GM.PhotonSpeed, 0f);
	}
}
