using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float BPM;
	public Vector2 PhotonSpeed;
	public int PlayerScore = 0;
	public int Multiplier = 1;

	private int streakCount = 0;

	// Use this for initialization
	void Start () {
		PhotonSpeed = new Vector2(-1 * (10/(1/BPM)), 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Score()
	{
		streakCount++;
		if(streakCount % 10 == 0 && streakCount <= 40)
		{
			Multiplier++;
		}
		PlayerScore += 100 * Multiplier;
	}

	void Miss()
	{
		streakCount = 0;
		Multiplier = 1;
	}
}
