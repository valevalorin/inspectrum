using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {
	public Texture2D fadeOutTexture;	//texture that will overlay the screen
	public float fadeSpeed = 0.8f;		//fading speed
	
	private int drawDepth = -1000;		//texture's order in the draw hierarchy
	private float alpha = 1.0f; 		//texture's alpha between 0 and 1
	private int fadeDir = -1;			//direction to fade: in -1 or out 1
	
	void OnGUI(){
		//fade out/in the alpha value using a direction
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha); //force the number between 0 and 1
		
		//set color
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
		
	}
	
	//set FadeDir to the direction parameter
	public float BeginFade(int direction){
		fadeDir = direction;
		return (fadeSpeed);
	}
	
	void OnLevelWasLoaded(){
		BeginFade (-1);
	}
}
