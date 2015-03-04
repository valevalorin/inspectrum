/**
 * This class is to catch all the photon's that passes after black hole
 **/
using UnityEngine;
using System.Collections;

public class DestroyWall : MonoBehaviour 
{
	//Destroy all object that collide with this gameobject
	void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(other.gameObject);
	}
}
