using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBasketballScript : MonoBehaviour {

	public GameObject thingToCreate;
	private float initialSpeed = 0;
	private bool isThrowing = false;
	public float angleFromHorizontal;
	private bool canShoot = true;

	List<GameObject> items = new List<GameObject>();

	/*void Start()
	{
		Vector3 start = new Vector3 (0, 0, 0), end = new Vector3 (0, 1, 0);
		GameObject upLine = null;
		//Lines.MakeArrow (ref upLine, start, end, 2, Color.red);
		GameObject arcLine = null;
		Vector3 dir = (end - start).normalized;
		//Lines.MakeArcArrow(ref arcLine, 270, 32, 2, dir, start+new Vector3 (0,0,0.5f), 
			new Vector3(0,0,0, Color.red);
	}*/

	public void EnableShooting(bool enabled) {
		canShoot = enabled;
		DestroyALl ();
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log (col.tag + "collided with player");
		if (col.tag == "SpawnySphere(Clone)") { 
			DestroyALl ();
		}
	} 

	public void Update () 
	{
		if (Input.GetButtonDown ("Fire2")) {
			canShoot = true;
			DestroyALl();
		}

		if(Input.GetButtonDown ("Fire1")) {
			isThrowing = true;
		}

		if (Input.GetButtonUp ("Fire1")) {
			ThrowItem();
			canShoot = false;
			initialSpeed = 0;
			isThrowing = false;
			powerLine.SetActive (false);
		}

		if (isThrowing) {
			initialSpeed += 1f;
			Vector3 dir = (transform.up + transform.forward).normalized;
			Vector3 s = transform.position + transform.up;
			Vector3 e = s + dir * 4;
			float size = 0.0675f * initialSpeed;
			Lines.MakeArrow (ref powerLine, s, e, 2, Color.red, size, size);
			powerLine.SetActive (true);
		}
 	}

	GameObject powerLine;

	public void ThrowItem() {
		if (canShoot) {
		Vector3 p = gameObject.transform.position+gameObject.transform.forward*1.5f;
		Debug.Log (gameObject+" wants to shoot a "+thingToCreate);
		GameObject item = Instantiate (thingToCreate, p, gameObject.transform.rotation);
		Quaternion q = item.transform.rotation;
		Quaternion r = Quaternion.Euler (-angleFromHorizontal, 0, 0);
		GameObject qAxis = null, qArc = null;
		float angle;
		Vector3 axis;
		Debug.Log (q);
		r.ToAngleAxis (out angle, out axis);
		//Lines.MakeArrow (ref qAxis, transform.position, transform.position + q*axis);
		//Lines.MakeArcArrow (ref qArc, angle, 32, 4, q*axis, 
		//	transform.forward, transform.position);


		item.transform.rotation = q * r;
		Rigidbody rb = item.GetComponent<Rigidbody> ();
		rb.velocity = item.transform.forward * initialSpeed;
		items.Add (item);
		}
	}

	public void DestroyALl() {
		for (int i = 0; i < items.Count; i++) {
			Destroy (items [i]);
		}
		items.Clear ();
	}
}
