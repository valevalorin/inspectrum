/**
 * Manager that will manage the Menu.scene
 **/
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.IO;
using System.Linq;

public class MenuManager : MonoBehaviour
{
	void Awake()
	{
		//When returning from Demo.Scene, reset the timeScale to 1; from pause menu
		Time.timeScale = 1;
	}

	public void StartGame()
	{
		//Determine which song was selected
		GameData data = GameObject.Find ("GameData").GetComponent<GameData> ();
		Toggle active = GameObject.Find ("SongList").GetComponent<ToggleGroup> ().ActiveToggles ().FirstOrDefault ();
		data.selectedSong = active.GetComponent<SongData> ();
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	//function to Exit.Scene
	public void ExitGame()
	{
		Application.Quit ();
	}

	//Function to transistion between scenes based on ID
	public void LoadScene(int sceneId)
	{
		Application.LoadLevel(sceneId);
	}
}
