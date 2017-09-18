using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallScript : MonoBehaviour {

	public GameObject thingToCreate;
	public float initialSpeed;
	public float angleFromHorizontal;
	GameObject item;

	public void ThrowItem() {
		Vector3 p = gameObject.transform.position+gameObject.transform.forward;
		item = Instantiate (thingToCreate, p, gameObject.transform.rotation);
		Quaternion q = item.transform.rotation;
		Quaternion r = Quaternion.Euler (-angleFromHorizontal, 0, 0);
		item.transform.rotation = q * r;
		Rigidbody rb = item.GetComponent<Rigidbody> ();
		rb.velocity = item.transform.forward * initialSpeed;
	}

	public void DeleteItem() {
		Destroy (item);
	}
}
