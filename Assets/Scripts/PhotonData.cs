/**
 * Class that defines the photon identity and color
 **/
using UnityEngine;
using System.Collections;

public class PhotonData{

	public InputColor color {get; set;}
	public GameObject self {get; set;}

	public PhotonData(InputColor color, GameObject self)
	{
		this.color = color;
		this.self = self;
	}
}
