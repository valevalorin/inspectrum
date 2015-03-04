/**
 * This class is for interaction between black hole and photon
 **/
using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour 
{
	//Constants
	Vector2 ZERO_VELOCITY = new Vector2 (0.0f, 0.0f);
	const float DECREASING_VALUE = 0.1f;
	const float WAIT_TIME = 0f;

	//Detect when photon collides with the black hole
	void OnTriggerStay2D(Collider2D other)
	{
		//When the photon and black hole are aligned, stop the photon and shrink the object
		if(other.gameObject.transform.position.x <= this.gameObject.transform.position.x)
		{
			other.gameObject.rigidbody2D.velocity = ZERO_VELOCITY;
			other.gameObject.collider2D.enabled = false;
			StartCoroutine (ShrinkTransform (other.gameObject.transform));
		}
	}

	//function to shrink a Transform obj over a particular time, then destroy the object
	public IEnumerator ShrinkTransform(Transform obj)
	{
		for(float i = obj.localScale.x; i > 0; i-=DECREASING_VALUE)
		{
			obj.localScale -= 
				new Vector3(obj.localScale.x-i, obj.localScale.y-i);
			yield return new WaitForSeconds(WAIT_TIME);
		}

		if(obj != null)
			Destroy(obj.gameObject);
	}
}
