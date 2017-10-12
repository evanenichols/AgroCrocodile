using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidScoreTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		Debug.Log ("You Scored!");
	}
}
