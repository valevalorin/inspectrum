using UnityEngine;
using System.Collections;

public class SongData : MonoBehaviour {
	public AudioClip song;
	public string title;
	public float bpm;
	public float audio_length{
		get{
			return this.song.length;
		}
	}
}
