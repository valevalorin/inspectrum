﻿using UnityEngine;
using System.Collections;

public class Photon : MonoBehaviour {
	public GameManger GM;


	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = GM.PhotonSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
