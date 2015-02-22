using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float BPM;
	public Vector2 PhotonSpeed;
	public AudioClip song;
	public bool SongFinished = false;

	public int PlayerScore = 0;
	public int Multiplier = 1;

	private AudioSource songPlayer;
	private int streakCount = 0;

	void Awake(){
		GameData data = GameObject.Find ("GameData").GetComponent<GameData> ();
		song = data._audio;
	}

	// Use this for initialization
	void Start () {
		PhotonSpeed = new Vector2(-1 * (10/(1/BPM)), 0);
		songPlayer = (AudioSource) gameObject.GetComponent<AudioSource>();
		songPlayer.clip = song;
		songPlayer.Play();
		GameObject.FindGameObjectWithTag("photon_manager").GetComponent<PhotonManager>().enabled = true;
		//Enable Other Components
	}

	// Update is called once per frame
	void Update () {

	}

	public void Score()
	{
		streakCount++;
		if(streakCount % 10 == 0 && streakCount <= 40)
		{
			Multiplier++;
		}
		PlayerScore += 100 * Multiplier;
	}

	public void Miss()
	{
		streakCount = 0;
		Multiplier = 1;
	}
}
