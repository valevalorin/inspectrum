﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviour {
	public GameManager GM;
	public Queue<PhotonData> PhotonQueue;

	public GameObject Red;
	public GameObject Blue;
	public GameObject Yellow;
	public GameObject Purple;
	public GameObject Green;
	public GameObject Orange;

	private float BPS;
	private System.Random rng;
	private float generationCooldown;

	// Use this for initialization
	void Start () {
		rng = new System.Random();
		PhotonQueue = new Queue<PhotonData>();
		BPS = GM.BPM/60;
		setCooldown();
	}
	
	// Update is called once per frame
	void Update () {
		generationCooldown -= Time.deltaTime;

		if(generationCooldown <= 0.0f)
		{
			PhotonQueue.Enqueue(generatePhoton());
			setCooldown();
		}

	}

	void setCooldown()
	{
		float num = rng.Next();
		if(num > 0.0f && num <= 0.7f)
			generationCooldown = 1/BPS;
		else if(num > 0.7f && num < 0.9f)
			generationCooldown = 0.5f/BPS;
		else 
			generationCooldown = 2/BPS;
	}

	PhotonData generatePhoton()
	{
		float num = UnityEngine.Random.value;
		 
		if(num > 0.0f && num <= 0.2f)
			return new PhotonData(InputColor.RED, (GameObject) Instantiate(Red, this.transform.position, Quaternion.identity));
		else if(num > 0.2f && num <= 0.4f)
			return new PhotonData(InputColor.BLUE, (GameObject) Instantiate(Blue, this.transform.position, Quaternion.identity));
		else if(num > 0.4f && num <= 0.6f)
			return new PhotonData(InputColor.YELLOW, (GameObject) Instantiate(Yellow, this.transform.position, Quaternion.identity));
		else if(num > 0.6f && num <= 0.74f)
			return new PhotonData(InputColor.PURPLE, (GameObject) Instantiate(Purple, this.transform.position,Quaternion.identity));
		else if(num > 0.74f && num <= 0.87f)
			return new PhotonData(InputColor.GREEN, (GameObject) Instantiate(Green, this.transform.position,Quaternion.identity));
		else
			return new PhotonData(InputColor.ORANGE, (GameObject) Instantiate(Orange, this.transform.position,Quaternion.identity));
	}
}
