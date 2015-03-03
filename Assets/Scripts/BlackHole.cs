using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {
	Vector2 ZERO_VELOCITY = new Vector2 (0.0f, 0.0f);
	const float DECREASING_VALUE = 0.1f;
	const float WAIT_TIME = 0f;

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.transform.position.x <= this.gameObject.transform.position.x){
			other.gameObject.rigidbody2D.velocity = ZERO_VELOCITY;
			other.gameObject.collider2D.enabled = false;
			StartCoroutine (ShrinkTransform (other.gameObject.transform));

			//Add particle system explosion cycle
		}
	}
	
	public IEnumerator ShrinkTransform(Transform obj){
		for(float i = obj.localScale.x; i > 0; i-=DECREASING_VALUE){
			obj.localScale -= 
				new Vector3(obj.localScale.x-i, obj.localScale.y-i);
			yield return new WaitForSeconds(WAIT_TIME);
		}
		if(obj != null)
			Destroy(obj.gameObject);
	}
}
