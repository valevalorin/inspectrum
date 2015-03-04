/**
 * Class that contains functions for Pause menu interactions
 **/
using UnityEngine;
using System.Collections;

public class PauseButtons : MonoBehaviour {

	public GameManager GM;

	void Start()
	{
		GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}

	public void ResumeGame()
	{
		GM.IsPaused = !GM.IsPaused;
	}

	public void RestartGame()
	{
		Application.LoadLevel(1);
	}

	public void QuitToMenu()
	{
		Application.LoadLevel(0);
	}

	public void QuitToDesktop()
	{
		Application.Quit();
	}
}
