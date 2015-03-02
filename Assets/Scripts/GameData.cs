using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

	public static GameData Instance{
		get{ return Instance; }
		set{}
	}

	private static GameData instance = null;

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
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(transform.gameObject);
		}else{
			DestroyImmediate(this);
		}
	}
}
