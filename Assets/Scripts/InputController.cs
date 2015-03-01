using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour
{
	public int InputFrames;
	public BlackHoleTrigger left_zone;
	public BlackHoleTrigger right_zone;
	public PhotonManager PM;
	public GameManager GM;

	public float entryZoneEnd;
	public float entryZoneBegin;

	private bool inputActive = false;
	private int activeInputCounter = 0;
	private List<InputColor> activeColors;

	// Use this for initialization
	void Start ()
	{
		activeColors = new List<InputColor>();
		PM = GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>();
		GM = (GameManager) GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			GM.IsPaused = !GM.IsPaused;
		}

		bool red = Input.GetKeyDown(KeyCode.A);
		bool blue = Input.GetKeyDown(KeyCode.S);
		bool yellow = Input.GetKeyDown(KeyCode.D);
		float distance;
		PhotonData currentPhoton;

		if(PM.PhotonQueue.Count > 0)
		{
			currentPhoton = (PhotonData) PM.PhotonQueue.Peek();
			distance = Vector3.Distance(currentPhoton.self.transform.position, PM.transform.position);
		}
		else
			return;


		if(distance > entryZoneEnd)
		{
			Debug.Log ("MISS!: "+distance);
			currentPhoton.self.tag = "DeadPhoton";
			GM.Miss();
			PM.PhotonQueue.Dequeue();
		}

		if(!inputActive && (red || blue || yellow))
		{
			inputActive = true;
			activeInputCounter = InputFrames;
		}

		if(inputActive)
		{
			if(red && !activeColors.Contains(InputColor.RED))
				activeColors.Add(InputColor.RED);

			if(blue && !activeColors.Contains(InputColor.BLUE))
				activeColors.Add(InputColor.BLUE);

			if(yellow && !activeColors.Contains(InputColor.YELLOW))
				activeColors.Add(InputColor.YELLOW);
		}

		if(inputActive && activeInputCounter == 0)
		{
			InputColor input = determineColor(activeColors.Contains(InputColor.RED), activeColors.Contains(InputColor.BLUE), activeColors.Contains(InputColor.YELLOW));

			if(distance > entryZoneBegin && distance <= entryZoneEnd && input == currentPhoton.color)
			{
				Debug.Log ("HIT!");
				GM.Score();
				currentPhoton.self.gameObject.rigidbody2D.velocity = new Vector2 (GM.PhotonSpeed, RandomUpOrDown() * GM.PhotonSpeed);
				PM.PhotonQueue.Dequeue();

			}
			else if((distance < entryZoneBegin && distance > (entryZoneBegin - 1.3)) || (distance > entryZoneBegin && distance <= entryZoneEnd && input != currentPhoton.color))
			{
				Debug.Log ("MISS!: "+distance);
				currentPhoton.self.tag = "DeadPhoton";
				GM.Miss();
				PM.PhotonQueue.Dequeue();
			}


			//Reset inputs
			activeColors = new List<InputColor>();
			inputActive = false;
		}

		if(inputActive)
			activeInputCounter--;
	}

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

