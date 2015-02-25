using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

	public ParticleSystem nearSystem;
	public ParticleSystem midSystem;
	public ParticleSystem farSystem;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	//Sets the speed and lifetime of all particles in the scene
	public void changeParticleSpeed(float speed){

		Debug.Log ("Speed is:" + speed);

		//Set the speed of each partilce system
		nearSystem.startSpeed = speed;
		nearSystem.startLifetime = Screen.width / nearSystem.startSpeed;
		
		midSystem.startSpeed = nearSystem.startSpeed / 2f;
		midSystem.startLifetime = Screen.width / midSystem.startSpeed;
		
		farSystem.startSpeed = midSystem.startSpeed / 2f;
		farSystem.startLifetime = Screen.width / farSystem.startSpeed;
	}
}
