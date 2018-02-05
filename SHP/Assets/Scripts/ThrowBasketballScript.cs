using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBasketballScript : MonoBehaviour {

	public GameObject thingToCreate;
	private float initialSpeed = 0;
	private bool isThrowing = false;
	//public float angleFromHorizontal;
	private Camera mainCamera;
	private int numUpdatesSinceThrow;

	List<GameObject> items = new List<GameObject>();

	void Start()
	{
		mainCamera = Camera.main;
	}

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
		DestroyALl ();
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log (col.tag + "collided with player");
		if (col.tag == "SpawnySphere(Clone)" ) { 
			if (numUpdatesSinceThrow == 100) {
				DestroyALl ();
				Debug.Log ("Destroyed");
			}
		}
	} 

	GameObject predictionLine;

	private void handlePredictionLine ()
	{
		List<Vector3> ballLocations = new List<Vector3> ();
		if (mainCamera == null) {
			mainCamera = Camera.main;
		}
		Vector3 v = (mainCamera.transform.forward).normalized * initialSpeed;
		Vector3 p = gameObject.transform.position;;//+gameObject.transform.forward*1.5f;;
		ballLocations.Add (p);
		for (int i = 0; i < 50; ++i) {
			float t = 1.0f / 8;
			p += v * t;
			v += Vector3.down * 9.81f * t;
			ballLocations.Add (p);
		}
		Lines.Make (ref predictionLine, ballLocations.ToArray (), ballLocations.Count, Color.green);

	}

	public void Update () 
	{

		if(Input.GetButtonDown ("Fire1")) {
			//predictionLine.SetActive (true);
			DestroyALl();
			isThrowing = true;
		} 

		if(Input.GetButtonDown ("Fire2")) {
			//predictionLine.SetActive (true);
			DestroyALl();
		} 

		/* if (items.Count > 0 && Input.GetButton ("Fire1")) {
			canShoot = true;
			DestroyALl ();
		} */

		if (Input.GetButtonUp ("Fire1")) {
			ThrowItem ();
			initialSpeed = 0;
			isThrowing = false;
			//powerLine.SetActive (false);
		}

		if (isThrowing) {
			initialSpeed += 1f;
			Vector3 dir = (transform.up + transform.forward).normalized;
			Vector3 s = transform.position + transform.up;
			Vector3 e = s + dir * 4;
			float size = 0.0675f * initialSpeed;
			//Lines.MakeArrow (ref powerLine, s, e, 2, Color.red, size, size);
			//powerLine.SetActive (true);

			handlePredictionLine ();
		}

		if (numUpdatesSinceThrow < 100) {
			numUpdatesSinceThrow++;
		}

		Debug.Log (numUpdatesSinceThrow);
 	}

	GameObject powerLine;

	public void ThrowItem() {
		Vector3 p = gameObject.transform.position;//+gameObject.transform.forward*1.5f;

			Debug.Log (gameObject+" wants to shoot a "+thingToCreate);
			GameObject item = Instantiate (thingToCreate, p, mainCamera.transform.rotation);
			Rigidbody rb = item.GetComponent<Rigidbody> ();
			rb.velocity = item.transform.forward * initialSpeed;
			items.Add (item);
		numUpdatesSinceThrow = 0;
	}

	public void DestroyALl() {
		//predictionLine.SetActive (false);
		for (int i = 0; i < items.Count; i++) {
			Destroy (items [i]);
		}
		items.Clear ();
	}
}