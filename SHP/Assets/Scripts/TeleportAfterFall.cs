using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAfterFall : MonoBehaviour {

	Vector3 startLocation;


	// Use this for initialization
	void Start () {
		startLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -20) {
			transform.position = startLocation;
		}
	}
}
