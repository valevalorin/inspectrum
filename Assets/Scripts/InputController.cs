using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour
{
	//How many frames the player has to enter combinations. Setable in inspector
	public int InputFrames;

	public PhotonManager PM;
	public GameManager GM;

	//The shortest and longest distance the photon can be away from the photon manager and it count as a hit.
	public float entryZoneEnd;
	public float entryZoneBegin;

	//Input window control variables
	private bool inputActive = false;
	private int activeInputCounter = 0;
	private List<InputColor> activeColors;

	// Use this for initialization
	void Start ()
	{
		//Set empty color inputs; get references to PM and GM
		activeColors = new List<InputColor>();
		PM = (PhotonManager) GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>();
		GM = (GameManager) GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Handle pause menu first
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			GM.IsPaused = !GM.IsPaused;
		}

		//Get color keys currently pressed.
		bool red = Input.GetKeyDown(KeyCode.A);
		bool blue = Input.GetKeyDown(KeyCode.S);
		bool yellow = Input.GetKeyDown(KeyCode.D);

		float distance;
		PhotonData currentPhoton;

		//if there's a photon to process put it in current photon and find out how far it is from the manager; else exit method
		if(PM.PhotonQueue.Count > 0)
		{
			currentPhoton = (PhotonData) PM.PhotonQueue.Peek();
			distance = Vector3.Distance(currentPhoton.self.transform.position, PM.transform.position);
		}
		else
			return;

		//First check if the current photon is past the entry zone; if it is count it as a miss
		if(distance > entryZoneEnd)
		{
			Debug.Log ("MISS!: "+distance);
			currentPhoton.self.tag = "DeadPhoton";
			GM.Miss();
			PM.PhotonQueue.Dequeue();
		}

		//Else check if input has become active
		if(!inputActive && (red || blue || yellow))
		{
			inputActive = true;
			activeInputCounter = InputFrames;
		}

		//If input is active add color keys to input list if they are not already there
		if(inputActive)
		{
			if(red && !activeColors.Contains(InputColor.RED))
				activeColors.Add(InputColor.RED);

			if(blue && !activeColors.Contains(InputColor.BLUE))
				activeColors.Add(InputColor.BLUE);

			if(yellow && !activeColors.Contains(InputColor.YELLOW))
				activeColors.Add(InputColor.YELLOW);
		}

		//If the input window has ended
		if(inputActive && activeInputCounter == 0)
		{
			InputColor input = determineColor(activeColors.Contains(InputColor.RED), activeColors.Contains(InputColor.BLUE), activeColors.Contains(InputColor.YELLOW));

			//If photon is inside the entry zone and the right color, score hit
			if(distance > entryZoneBegin && distance <= entryZoneEnd && input == currentPhoton.color)
			{
				Debug.Log ("HIT!");
				GM.Score();

				//Change velocity to save photon from blackhole and dequeue them.
				currentPhoton.self.gameObject.rigidbody2D.velocity = new Vector2 (GM.PhotonSpeed, RandomUpOrDown() * GM.PhotonSpeed);
				PM.PhotonQueue.Dequeue();

			}
			else if((distance < entryZoneBegin && distance > (entryZoneBegin - 1.3)) || (distance > entryZoneBegin && distance <= entryZoneEnd && input != currentPhoton.color))
			{
				//If photon is partially but not fully in the zone OR in the zone and the wrong color score miss
				Debug.Log ("MISS!: "+distance);
				currentPhoton.self.tag = "DeadPhoton"; //Used to make sure the photon isn't counted for other interactions
				GM.Miss();
				PM.PhotonQueue.Dequeue(); //Dequeue
			}


			//Reset inputs
			activeColors = new List<InputColor>();
			inputActive = false;
		}

		//Countdown input frames
		if(inputActive)
			activeInputCounter--;
	}

	//Determines the color input by the player
	private InputColor determineColor(bool red, bool blue, bool yellow)
	{	
		if(red && !(blue || yellow))
			return InputColor.RED;
		else if(red && blue && !yellow)
			return InputColor.PURPLE;
		else if(red && yellow && !blue)
			return InputColor.ORANGE;
		else if(blue && !(red || yellow))
			return InputColor.BLUE;
		else if(blue && yellow && !red)
			return InputColor.GREEN;
		else if(yellow && !(red || blue))
			return InputColor.YELLOW;
		else if(red && blue && yellow)
			return InputColor.BLACK;

		return InputColor.NONE;
	}

	//Used to randomly send the photon up or down on score.
	public float RandomUpOrDown()
	{
		float num = UnityEngine.Random.value;
		if(num >= 0.5f)
			return 1f;
		else
			return -1f;
	}
}

public enum InputColor
{
	RED,
	BLUE,
	YELLOW,
	PURPLE,
	ORANGE,
	GREEN,
	BLACK,
	NONE
}

