using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	const int PHOTON_VALUE_SCORE = 1;
	const string EMPTY_STRING = "";

	public float PhotonSpeed;
	public bool SongFinished = false;
	public float shutdownTimer;
	private float remainingSongTime;
	private float timeToStart;

	private int streakCount = 0;
	private Text scoreText;
	private Text[] multiplierText;
	private Text streakText;

	public GameData data;
	
	public bool IsPaused = false;
	public GameObject PauseScreen;

	void Awake(){
		data = GameObject.Find ("GameData").GetComponent<GameData> ();
		data.score = 0;
		data.multiplier = 1;

		GameObject.Find ("song_title").GetComponent<Text> ().text = data.selectedSong.title;
		GameObject.Find ("bpm").GetComponent<Text> ().text = string.Format("{0}",data.selectedSong.bpm);
		scoreText = GameObject.Find ("score").GetComponent<Text> ();
		multiplierText = GameObject.Find ("multiplier").GetComponentsInChildren<Text>();
		streakText = GameObject.Find ("hit_counter").GetComponent<Text> ();

		multiplierText [2].color = Color.red;
	}

	// Use this for initialization
	void Start () {
		PhotonSpeed = -13.64f/2;//(-15.5f / (1f / data.selectedSong.bpm))/256;
		AudioSource songPlayer = (AudioSource) gameObject.GetComponent<AudioSource>();
		songPlayer.clip = data.selectedSong.song;
		remainingSongTime = songPlayer.clip.length;
		songPlayer.Play();

		//Enable Other Components
		shutdownTimer = (13.64f/2f)/PhotonSpeed;
	}

	// Update is called once per frame
	void Update () {
		UpdateStreakText ();
		UpdateScoreText ();
		UpdateMultText();

		if(IsPaused)
		{
			PauseScreen.GetComponent<CanvasGroup>().alpha = 1;
			Time.timeScale = 0;
		}
		else
		{
			PauseScreen.GetComponent<CanvasGroup>().alpha = 0;
			Time.timeScale = 1;
		}

		remainingSongTime -= Time.deltaTime;
		if(remainingSongTime <= data.selectedSong.audio_length - data.selectedSong.offset)
		{
			GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>().enabled = true;
			GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>().enabled = true;
		}
		if(remainingSongTime <= shutdownTimer)
			GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>().enabled = false;
	}

	public void Score(){
		streakCount++;
		if(streakCount % 10 == 0 && streakCount <= 40 && data.multiplier < 4){
			data.multiplier++;
		}
		data.score += PHOTON_VALUE_SCORE * data.multiplier;
	}

	public void Miss(){
		streakCount = 0;
		data.multiplier = 1;
	}

	void UpdateMultText(){
		resetMultText ();
		multiplierText [data.multiplier - 1].color = Color.red;
	}

	void resetMultText(){
		foreach(Text txt in multiplierText){
			txt.color = Color.white;
		}
	}

	void UpdateStreakText(){
		if (streakCount > 5){
			streakText.text = string.Format ("x{0:0000}", streakCount);
		}else{
			streakText.text = "";
		}
	}

	void UpdateScoreText(){
		scoreText.text = string.Format("{0:0000000}", data.score);
	}
}
