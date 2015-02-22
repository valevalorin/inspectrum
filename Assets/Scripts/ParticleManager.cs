using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

	public float trackSpeed;
	public float lifeTime;
	
	public ParticleSystem nearSystem;
	public ParticleSystem midSystem;
	public ParticleSystem farSystem;
	
	private const float NEAR_SIZE = 0.3f;
	private const float MID_SIZE = 0.15f;
	private const float FAR_SIZE = 0.075f;
	
	// Use this for initialization
	void Start () {
		
		//Set the size for each particle system
		nearSystem.startSize = NEAR_SIZE;
		midSystem.startSize = MID_SIZE;
		farSystem.startSize = FAR_SIZE;
		
		//Set the speed for each particle system
		nearSystem.startSpeed = trackSpeed;
		midSystem.startSpeed = nearSystem.startSpeed / 2f;
		farSystem.startSpeed = midSystem.startSpeed / 2f;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
