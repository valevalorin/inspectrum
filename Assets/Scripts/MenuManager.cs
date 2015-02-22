﻿using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.IO;
using System.Linq;

public class MenuManager : MonoBehaviour {
	const string PATH = "Assets/Audio/";
	const int Y_OFFSET = -30;
	
	void Start(){
		//TODO: dynamically add list of songs from Assets/Audio/
//		DirectoryInfo dir = new DirectoryInfo (PATH);
//		FileInfo[] files = dir.GetFiles ();
//		
//		foreach(FileInfo file in files){
//			if(!file.Name.Contains(".meta")){
//				
//			}
//		}
	}

	public void StartGame(){
		GameData data = GameObject.Find ("GameData").GetComponent<GameData> ();
		Toggle active = GameObject.Find ("song_list").GetComponent<ToggleGroup> ().ActiveToggles ().FirstOrDefault ();
		data._audio = active.GetComponent<AudioSource> ().clip;
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
