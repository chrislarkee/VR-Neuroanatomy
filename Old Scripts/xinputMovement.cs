using UnityEngine;
using System.Collections;
//using MiddleVR_Unity3D;

public class xinputMovement : MonoBehaviour {
	private Vector3 move;
	private Vector3 tilt;

	public float moveSpeed = 1f;
	public float tiltSpeed = 1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		move.z = Input.GetAxis("Forward");
		move.x = Input.GetAxis("Strafe") * 0.75f;
		move.y = (Input.GetAxis("ElevateUp") - Input.GetAxis("ElevateDown")) * 0.5f;

		if (move.magnitude > 0.1f){
			transform.Translate(move * Time.deltaTime * moveSpeed);
		}

		tilt.x = Input.GetAxisRaw("Vertical") * 0.5f;
		tilt.y = Input.GetAxisRaw("Horizontal");
		if (tilt.magnitude > 0.1f){
			transform.Rotate(tilt * Time.deltaTime * tiltSpeed);
		}
	}
}
