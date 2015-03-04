using UnityEngine;
using System;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviour {
	//Reference to GameManager
	public GameManager GM;

	//This queue holds references to all the photons. Makes it easy to know which photon is the 'current' one.
	public Queue<PhotonData> PhotonQueue;

	//Prefabs for each of the photons; for instantiation
	public GameObject Red;
	public GameObject Blue;
	public GameObject Yellow;
	public GameObject Purple;
	public GameObject Green;
	public GameObject Orange;

	//Half beats always come in pairs. This controls their triplet pattern
	private bool triplet = false;
	private int tripCount = 0;

	//Used to make sure photons are spawned on beat
	private float BPS; //Beats per second
	private float generationCooldown;

	// Use this for initialization
	void Start () {
		PhotonQueue = new Queue<PhotonData>();
		BPS = GM.data.selectedSong.bpm/60;

		//Set initial cooldown
		setCooldown();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Subtract elapsed time from the cooldown timer.
		generationCooldown -= Time.deltaTime;

		//If the cooldown timer is over, generate a new photon
		if(generationCooldown <= 0.0f)
		{
			PhotonQueue.Enqueue(generatePhoton());
			setCooldown();
		}

	}

	void setCooldown()
	{
		//Half beats always come in triplets; this if statement enforces that.
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

		//Randomly set to cooldown to fire a photon half a beat, one beat, or two beats from now.
		float num = UnityEngine.Random.value;
		if(num > 0.0f && num <= 0.3f)
			generationCooldown = 1f/BPS;
		else if(num > 0.3f && num < 0.45f)
		{
			generationCooldown = 1f/BPS;
			triplet = true;
		}
		else 
			generationCooldown = 2/BPS;
	}

	//Randomly pick the next color photon to spawn; more likely to generate single colors than combos
	PhotonData generatePhoton()
	{
		float num = UnityEngine.Random.value;

		//Color decided and the photon itself is instantiated. The calling function puts it in the queue.
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
