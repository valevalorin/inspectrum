using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {
	public AudioClip _audio {
				get;
				set;
		}

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}
}
