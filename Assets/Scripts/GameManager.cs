/**
 * Manages the data associated with the Game and Update UI
 **/
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	//Constants
	const float END_PHOTON_GEN = 5.0f; //5 seconds
	const int PHOTON_VALUE_SCORE = 1;
	const string EMPTY_STRING = "";

	//Variables
	public float PhotonSpeed;
	public bool SongFinished = false;
	public float shutdownTimer;
	private float remainingSongTime;
	private float timeToStart;

	//Variables for UI
	private int streakCount = 0;
	private Text scoreText;
	private Text[] multiplierText;
	private Text streakText;
	private Text timeText;

	//Data for the Demo.Scene
	public GameData data;

	//Variable for Pause scene
	public bool IsPaused = false;
	private bool IsGameOver = false;
	public GameObject PauseScreen;

	AudioSource songPlayer;

	//Instantiate
	void Awake()
	{
		data = GameObject.Find ("GameData").GetComponent<GameData> ();
		data.score = 0;
		data.multiplier = 1;

		GameObject.Find ("song_title").GetComponent<Text> ().text = data.selectedSong.title;
		GameObject.Find ("bpm").GetComponent<Text> ().text = string.Format("{0}",data.selectedSong.bpm);
		scoreText = GameObject.Find ("score").GetComponent<Text> ();
		multiplierText = GameObject.Find ("multiplier").GetComponentsInChildren<Text>();
		streakText = GameObject.Find ("hit_counter").GetComponent<Text> ();
		timeText = GameObject.Find ("Timestamp").GetComponent<Text> ();

		songPlayer = (AudioSource) gameObject.GetComponent<AudioSource>();
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
		//If song is playing, update UI component
		if(songPlayer.isPlaying){
			UpdateStreakText ();
			UpdateScoreText ();
			UpdateMultText();
			UpdateTimeText();
		}

		//Determine if the song is in a Paused State
		if(IsPaused || IsGameOver){
			pauseGame();
		}else{
			resumeGame();
		}

		//Enable PhotonManager after an offset (if any)
		remainingSongTime -= Time.deltaTime;
		if(remainingSongTime <= songPlayer.clip.length - data.selectedSong.offset)
		{
			GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>().enabled = true;
			GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>().enabled = true;
		}

		//Disable PhotonManager before a the song ends
		if((songPlayer.clip.length - songPlayer.audio.time) <= END_PHOTON_GEN)
			GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>().enabled = false;

		//Determine the game is over 1 second before the song ends
		if((songPlayer.clip.length - songPlayer.audio.time) <= 1.0f){
			IsGameOver = true;
		}
	}

	//Function to update streakCount, score, and multiplier when user selects correct color combination
	public void Score()
	{
		streakCount++;
		if(streakCount % 10 == 0 && streakCount <= 40 && data.multiplier < 4)
		{
			data.multiplier++;
		}
		data.score += PHOTON_VALUE_SCORE * data.multiplier;
	}

	//Function to reset streakCount and multiplier when user misses color combination
	public void Miss()
	{
		streakCount = 0;
		data.multiplier = 1;
	}

	//Function to update multiplier UI
	void UpdateMultText()
	{
		resetMultText ();
		multiplierText [data.multiplier - 1].color = Color.red;
	}

	//Function to reset multiplier to WHITE text
	void resetMultText()
	{
		foreach(Text txt in multiplierText)
		{
			txt.color = Color.white;
		}
	}

	//Function to display streak counter on screen
	void UpdateStreakText()
	{
		//If streak is greater than 5, display streak
		if (streakCount > 5)
		{
			streakText.text = string.Format ("x{0:0000}", streakCount);
		}
		else
		{
			streakText.text = "";
		}
	}

	//Function to update score from GameData
	void UpdateScoreText()
	{
		scoreText.text = string.Format("{0:000000}", data.score);
	}

	//Function to update the Time to display of the duration of the song
	void UpdateTimeText()
	{
		float totalTime = songPlayer.clip.length;
		float currTime = songPlayer.time;

		timeText.text = string.Format ("{0} / {1}", convertFloatToTime (currTime), convertFloatToTime (totalTime));
	}

	//Function to convert float into actual time {mm:ss}
	//param: value - represents seconds
	public string convertFloatToTime(float value)
	{
		int minute = (int)value / 60;
		int seconds = (int)value % 60;
		return string.Format ("{0:00}:{1:00}", minute, seconds);
	}

	//Function to present Pause UI
	void pauseGame()
	{
		//Set to active and displays Pause UI
		PauseScreen.SetActive(true);
		PauseScreen.GetComponent<CanvasGroup>().alpha = 1;
		Time.timeScale = 0;
		//Update score on Pause UI
		GameObject.Find ("pauseScore").GetComponent<Text> ().text = string.Format("{0:000000}", data.score);
		UpdateScoreText();
		//Pause the music
		songPlayer.Pause();

		//If the game is over, hide the resume button and display Victory message
		if(IsGameOver && GameObject.Find ("Resume") != null)
		{
			GameObject.Find ("Resume").SetActive(false);
		}
	}

	//Function to resume game from Pause UI
	void resumeGame()
	{
		//Set PAUSE UI active to not display
		PauseScreen.SetActive(false);
		PauseScreen.GetComponent<CanvasGroup>().alpha = 0;
		//Resume time
		Time.timeScale = 1;

		//Resume the song
		if(!songPlayer.isPlaying)
		{
			songPlayer.Play ();
		}
	}
}
