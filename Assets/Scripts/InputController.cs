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

	private bool inputActive = false;
	private int activeInputCounter = 0;
	private List<InputColor> activeColors;

	// Use this for initialization
	void Start ()
	{
		activeColors = new List<InputColor>();
		PM = (PhotonManager) GameObject.FindGameObjectWithTag("PhotonManager").GetComponent<PhotonManager>();
		GM = (GameManager) GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		left_zone = (BlackHoleTrigger) GameObject.FindGameObjectWithTag("left_zone").GetComponent<BlackHoleTrigger>();
		right_zone = (BlackHoleTrigger) GameObject.FindGameObjectWithTag("right_zone").GetComponent<BlackHoleTrigger>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool red = Input.GetKeyDown(KeyCode.A);
		bool blue = Input.GetKeyDown(KeyCode.S);
		bool yellow = Input.GetKeyDown(KeyCode.D);

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

		if(inputActive && activeInputCounter == 0 && PM.PhotonQueue.Count > 0)
		{
			InputColor input = determineColor(activeColors.Contains(InputColor.RED), activeColors.Contains(InputColor.BLUE), activeColors.Contains(InputColor.YELLOW));

		    PhotonData currentPhoton = (PhotonData) PM.PhotonQueue.Peek();

			if(left_zone.isTriggered && right_zone.isTriggered && input == currentPhoton.color)
			{
				Debug.Log ("HIT!");
				GM.Score();
				currentPhoton.self.GetComponent<BoxCollider2D>().enabled = false;
				currentPhoton.self.gameObject.rigidbody2D.AddForce(new Vector2 (0f, RandomUpOrDown() * GM.PhotonSpeed));
				PM.PhotonQueue.Dequeue();
				left_zone.isTriggered = false;
				right_zone.isTriggered = false;

			}
			else if(left_zone.isTriggered ^ right_zone.isTriggered || (left_zone.isTriggered && right_zone.isTriggered && input != currentPhoton.color))
			{
				Debug.Log ("MISS!: "+left_zone.isTriggered+":"+right_zone.isTriggered);
				currentPhoton.self.tag = "DeadPhoton";
				GM.Miss();
				PM.PhotonQueue.Dequeue();
				left_zone.isTriggered = false;
				right_zone.isTriggered = false;

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

