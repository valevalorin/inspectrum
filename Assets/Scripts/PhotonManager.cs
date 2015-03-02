using UnityEngine;
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

	private bool triplet = false;
	private int tripCount = 0;

	private float BPS;
	private System.Random rng;
	private float generationCooldown;

	// Use this for initialization
	void Start () {
		PhotonQueue = new Queue<PhotonData>();
		BPS = GM.data.selectedSong.bpm/60;
		setCooldown();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		generationCooldown -= Time.deltaTime;

		if(generationCooldown <= 0.0f)
		{
			PhotonQueue.Enqueue(generatePhoton());
			setCooldown();
		}

	}

	void setCooldown()
	{
		if(triplet)
		{
			generationCooldown = 0.5f/BPS;
			tripCount++;

			if(tripCount == 2)
			{
				triplet = false;
				tripCount = 0;
			}
			return;
		}

		float num = UnityEngine.Random.value;
		if(num > 0.0f && num <= 1.3f)
			generationCooldown = 1f/BPS;
		else if(num > 0.3f && num < 0.45f)
		{
			generationCooldown = 1f/BPS;
			triplet = true;
		}
		else 
			generationCooldown = 2/BPS;
	}

	PhotonData generatePhoton()
	{
		float num = UnityEngine.Random.value;
		 
		if(num > 0.0f && num <= 1.2f)
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
