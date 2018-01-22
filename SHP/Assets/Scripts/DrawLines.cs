using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour {
	private Camera myCamera;
	GameObject cameraLine;
	GameObject bodyLine;

	void Start () {
		myCamera = Camera.main;
		Debug.Log (myCamera);
	}

	void Update () {
		if (myCamera == null) {
			myCamera = Camera.main;
			if (myCamera == null) {
				return;
			}
		}
		Lines.MakeArrow (ref cameraLine, myCamera.transform.position, 
						myCamera.transform.position + myCamera.transform.forward*2f, 2, Color.red);
		Lines.MakeArrow (ref bodyLine, transform.position, 
			transform.position + transform.forward*2f, 2, Color.blue);
		
	}
}
