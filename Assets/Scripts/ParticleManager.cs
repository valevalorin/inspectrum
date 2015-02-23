using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

	public float trackSpeed;
	public float menuSpeed;
	
	public ParticleSystem nearSystem;
	public ParticleSystem midSystem;
	public ParticleSystem farSystem;

	private PhotonManager photonManager;
	
	private const float NEAR_SIZE = 0.3f;
	private const float MID_SIZE = 0.15f;
	private const float FAR_SIZE = 0.075f;
	private const int MENU = 0;

	void Awake(){

		//Set the size for each particle system
		nearSystem.startSize = NEAR_SIZE;
		midSystem.startSize = MID_SIZE;
		farSystem.startSize = FAR_SIZE;
		
		//Set the speed for each particle system
		if(Application.loadedLevel == MENU){
			nearSystem.startSpeed = menuSpeed;
			nearSystem.startLifetime = Screen.width / nearSystem.startSpeed;

			midSystem.startSpeed = nearSystem.startSpeed / 2f;
			midSystem.startLifetime = Screen.width / midSystem.startSpeed;

			farSystem.startSpeed = midSystem.startSpeed / 2f;
			farSystem.startLifetime = Screen.width / farSystem.startSpeed;
		}
		else{
			nearSystem.startSpeed = trackSpeed;
			nearSystem.startLifetime = Screen.width / nearSystem.startSpeed;

			midSystem.startSpeed = nearSystem.startSpeed / 2f;
			midSystem.startLifetime = Screen.width / midSystem.startSpeed;

			farSystem.startSpeed = midSystem.startSpeed / 2f;
			farSystem.startLifetime = Screen.width / farSystem.startSpeed;
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
}
