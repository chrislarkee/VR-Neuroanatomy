using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateUser : MonoBehaviour {

	private float rotationAmount = 60f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.GetUp(OVRInput.RawButton.DpadRight)) rotateHead(rotationAmount);
		if (OVRInput.GetUp(OVRInput.RawButton.DpadLeft)) rotateHead(rotationAmount * -1f);
	}

	void rotateHead(float amount){
		//blink?
		transform.Rotate(new Vector3(0f, amount, 0f));
	}
}
