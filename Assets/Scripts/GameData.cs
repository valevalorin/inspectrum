using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {
	public AudioClip _audio;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}
}
