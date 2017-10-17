using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidScoreTrigger : MonoBehaviour {

	public TMPro.TextMeshPro output;
	public string HomeGuest;
	private int score = 0;

	void Start() {
		output.text = HomeGuest + " :" + (score);
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log (HomeGuest + " Scored!");
		output.text = HomeGuest + " :" + (++score);
	}
}
