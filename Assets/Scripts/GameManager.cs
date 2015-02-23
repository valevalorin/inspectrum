using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	const int PHOTON_VALUE_SCORE = 1;

	public float PhotonSpeed;
	public bool SongFinished = false;

	private int streakCount = 0;
	private Text scoreText;
	private Text[] multiplierText;
	public GameData data;

	void Awake(){
		data = GameObject.Find ("GameData").GetComponent<GameData> ();
		data.score = 0;
		data.multiplier = 1;

		GameObject.Find ("song_title").GetComponent<Text> ().text = data.selectedSong.title;
		scoreText = GameObject.Find ("score").GetComponent<Text> ();
		multiplierText = GameObject.Find ("multiplier").GetComponentsInChildren<Text>();
	}

	// Use this for initialization
	void Start () {
		PhotonSpeed = (-20f / (1f / data.selectedSong.bpm))/4;
		AudioSource songPlayer = (AudioSource) gameObject.GetComponent<AudioSource>();
		songPlayer.clip = data.selectedSong.song;
		songPlayer.Play();
		GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>().enabled = true;
		//Enable Other Components
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = string.Format("{0:0000000}", data.score);
		streakCount++;
		Score ();
		UpdateMultUI (data.multiplier);
	}

	public void Score(){
		streakCount++;
		if(streakCount % 10 == 0 && streakCount <= 40){
			data.multiplier++;
		}
		data.score += PHOTON_VALUE_SCORE * data.multiplier;
	}

	public void Miss(){
		streakCount = 0;
		data.multiplier = 1;
	}

	void UpdateMultUI(int multi){
		foreach(Text txt in multiplierText){
			txt.color = Color.white;
		}
		multiplierText [multi - 1].color = Color.red;
	}
}
