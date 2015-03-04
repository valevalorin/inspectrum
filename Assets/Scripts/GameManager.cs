using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	const float END_PHOTON_GEN = 5.0f; //5 seconds
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
	private bool IsGameOver = false;
	public GameObject PauseScreen;

	AudioSource songPlayer;

	void Awake(){
		data = GameObject.Find ("GameData").GetComponent<GameData> ();
		data.score = 0;
		data.multiplier = 1;

		GameObject.Find ("song_title").GetComponent<Text> ().text = data.selectedSong.title;
		GameObject.Find ("bpm").GetComponent<Text> ().text = string.Format("{0}",data.selectedSong.bpm);
		scoreText = GameObject.Find ("score").GetComponent<Text> ();
		multiplierText = GameObject.Find ("multiplier").GetComponentsInChildren<Text>();
		streakText = GameObject.Find ("hit_counter").GetComponent<Text> ();
		songPlayer = (AudioSource) gameObject.GetComponent<AudioSource>();
		multiplierText [2].color = Color.red;

	}

	// Use this for initialization
	void Start () {
		PhotonSpeed = -13.64f/2;//(-15.5f / (1f / data.selectedSong.bpm))/256;
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

		if(IsPaused || IsGameOver){
			pauseGame();
		}else{
			resumeGame();
		}

		remainingSongTime -= Time.deltaTime;
		if(remainingSongTime <= data.selectedSong.audio_length - data.selectedSong.offset){
			GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>().enabled = true;
			GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>().enabled = true;
		}
		if((songPlayer.clip.length - songPlayer.audio.time) <= END_PHOTON_GEN)
			GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>().enabled = false;

		if((songPlayer.clip.length - songPlayer.audio.time) <= 1.0f){
			IsGameOver = true;
		}
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
		scoreText.text = string.Format("{0:000000}", data.score);
	}

	void pauseGame(){
		PauseScreen.SetActive(true);
		PauseScreen.GetComponent<CanvasGroup>().alpha = 1;
		Time.timeScale = 0;
		GameObject.Find ("pauseScore").GetComponent<Text> ().text = string.Format("{0:000000}", data.score);
		UpdateScoreText();
		songPlayer.Pause();
		if(IsGameOver && GameObject.Find ("Resume") != null){
			GameObject.Find ("Resume").SetActive(false);
		}
	}

	void resumeGame(){
		PauseScreen.SetActive(false);
		PauseScreen.GetComponent<CanvasGroup>().alpha = 0;
		Time.timeScale = 1;
		if(!songPlayer.isPlaying){
			songPlayer.Play ();
		}
	}
}
