/**
 * Singleton class that instantiate and contains all data for the game
 * 	based on the choice made from the Menu.scene
 **/
using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour 
{
	//instance of object
	public static GameData Instance
	{
		get{ return instance; }
		set{}
	}

	private static GameData instance = null;
	//determine if the object already exist or not
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			DestroyImmediate(this.gameObject);
		}
	}
	
	public int score{get; set;}
	
	public int multiplier{get; set;}

	//SelectedSong from Menu.Scene
	public SongData selectedSong{get; set;}
}
