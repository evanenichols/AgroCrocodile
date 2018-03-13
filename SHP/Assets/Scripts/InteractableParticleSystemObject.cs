using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableParticleSystemObject : VRTK.VRTK_InteractableObject {
	public ParticleSystem ps;
	public int triggerBurst = 50;
	public bool drawThrowLine = true;
	public Color throwLineColor = new Color(.5f,.5f,.5f,.5f);

	void Start() {
		if (ps == null) {
			ps = GetComponent<ParticleSystem> ();
			if (ps == null) {
				ps = GetComponentInChildren<ParticleSystem> ();
			}
		}
		ps.Stop ();
		this.isGrabbable = true;
		this.isUsable = true;
	}

	public override void StartUsing(VRTK.VRTK_InteractUse usingObject) {
		base.StartUsing(usingObject);
		ps.Play ();
		ps.Emit (triggerBurst);
	}

	public override void StopUsing(VRTK.VRTK_InteractUse usingObject) {
		base.StopUsing(usingObject);
		ps.Stop ();
	}

	public override void Grabbed(VRTK.VRTK_InteractGrab currentGrabbingObject = null) {
		base.Grabbed (currentGrabbingObject);
		VRTK.VRTK_InteractUse iu = currentGrabbingObject.gameObject.GetComponent<VRTK.VRTK_InteractUse> ();
		if (iu == null) {
			iu = currentGrabbingObject.gameObject.AddComponent<VRTK.VRTK_InteractUse> ();
		}
	}

	public override void Ungrabbed(VRTK.VRTK_InteractGrab previousGrabbingObject = null) {
		if (drawThrowLine) {
			UpdateLine ();
		}
		base.Ungrabbed (previousGrabbingObject);
	}

	GameObject predictionLine;

	void UpdateLine() {
		Vector3[] prediction = new Vector3[32];
		float tdelta = Time.fixedDeltaTime;
		float t = 0;
		Rigidbody rb = GetComponent<Rigidbody> ();
		Vector3 velocity = rb.velocity;
		VRTK.VRTK_ControllerReference controllerReference = VRTK.VRTK_ControllerReference.GetControllerReference(GetGrabbingObject());
		velocity = VRTK.VRTK_DeviceFinder.GetControllerVelocity(controllerReference);
		Vector3 angularVelocity = VRTK.VRTK_DeviceFinder.GetControllerAngularVelocity(controllerReference);

		Vector3 p = transform.position;
		for (int i = 0; i < prediction.Length; ++i) {
			p += velocity * tdelta;
			if (rb.useGravity) {
				velocity += Physics.gravity * tdelta;
			}
			prediction [i] = p;
		}
		NS.Lines.MakeArrow (ref predictionLine, prediction, prediction.Length, throwLineColor);
	}

//	void Update() {
//		base.Update ();
//		if (IsGrabbed ()) {
//			UpdateLine ();
//		}
//	}
}