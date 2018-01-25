using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {
	Rigidbody rb;
	Camera myCamera;
	public float bodySpeed = 10;
	void Start () {
		rb = GetComponent<Rigidbody> ();
		myCamera = Camera.main;
	}

	GameObject line_forward;
	GameObject line_velocity;
	void Update () {
		float h = Input.GetAxis ("Horizontal")*bodySpeed;
		float v = Input.GetAxis ("Vertical")*bodySpeed;
		bool jump = Input.GetButtonDown ("Jump");
		Vector3 movement = rb.velocity;
		float gravitySpeed = Vector3.Dot(Vector3.down, rb.velocity);
		movement = Vector3.down*gravitySpeed + transform.forward * v + transform.right * h;
		if (jump) {
			movement.y += 3;
		}
		rb.velocity = movement;

		Lines.MakeArrow (ref line_velocity, transform.position, 
			transform.position + rb.velocity, 3, Color.magenta);
	}

	public void matchCameraLook() {
		Vector3 myRotation = Vector3.Cross(myCamera.transform.right, Vector3.up);
		Lines.Make(ref line_forward, transform.position, transform.position + myRotation, Color.green);
		transform.rotation = Quaternion.LookRotation (myRotation.normalized, Vector3.up);
	}
}
