using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour
{
	private bool inputActive = false;
	private int activeInputCounter = 0;
	private List<Color> activeColors;
	public int inputFrames;

	// Use this for initialization
	void Start ()
	{
		activeColors = new List<Color>();
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
			activeInputCounter = inputFrames;
		}

		if(inputActive)
		{
			if(red && !activeColors.Contains(Color.red))
				activeColors.Add(Color.red);

			if(blue && !activeColors.Contains(Color.blue))
				activeColors.Add(Color.blue);

			if(yellow && !activeColors.Contains(Color.yellow))
				activeColors.Add(Color.yellow);
		}
		else if(activeInputCounter == 0)
		{
			Color input = determineColor(activeColors.Contains(Color.red), activeColors.Contains(Color.blue), activeColors.Contains(Color.yellow));
			//Send input to blackhole
			activeColors = new List<Color>();
			inputActive = false;
		}
		else
			activeInputCounter--;
	}

	private Color determineColor(bool red, bool blue, bool yellow)
	{	
		if(red && !(blue || yellow))
			return Color.red;
		else if(red && blue && !yellow)
			return Color.purple;
		else if(red && yellow && !blue)
			return Color.orange;
		else if(blue && !(red || yellow))
			return Color.blue;
		else if(blue && yellow && !red)
			return Color.green;
		else if(yellow && !(red || blue))
			return Color.yellow;
		else if(red && blue && yellow)
			return Color.black;

		return null;
	}
}

