using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {
	public int score{
		get;
		set;
	}

	public int multiplier{
		get;
		set;
	}
	public SongData selectedSong {
		get;
		set;
	}

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}
}
