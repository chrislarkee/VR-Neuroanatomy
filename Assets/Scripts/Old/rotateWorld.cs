using UnityEngine;
using System.Collections;
//using MiddleVR_Unity3D;

public class rotateWorld : MonoBehaviour {

	private GameObject anchor;
	private Quaternion startRot;
	private Vector3 startPos;
	private bool IsAnimActive = false;

	private float axisValueH = 0f;
	private float axisValueV = 0f;


	// Use this for initialization
	void Start () {
		anchor = GameObject.Find("RotationPoint");
		startRot = transform.rotation;
		startPos = transform.position;
	}

	void Update () {
		//if (MiddleVR.VRDeviceMgr.IsKeyPressed(MiddleVR.VRK_LEFT))
		//	rotateScene("h", 1f);
		//if (MiddleVR.VRDeviceMgr.IsKeyPressed(MiddleVR.VRK_RIGHT))
		//	rotateScene("h", -1f);
		

		//if (MiddleVR.VRDeviceMgr.IsKeyPressed(MiddleVR.VRK_UP))
		//	rotateScene("v", 1f);
		//if (MiddleVR.VRDeviceMgr.IsKeyPressed(MiddleVR.VRK_DOWN))
		//	rotateScene("v", -1f);

		//axisValueH = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
		//axisValueV = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();
		//if (Mathf.Abs(axisValueH) > 0.05f) rotateScene("h", axisValueH * -1f);
		//if (Mathf.Abs(axisValueV) > 0.05f) rotateScene("v", axisValueV);

		//if (MiddleVR.VRDeviceMgr.IsKeyToggled(MiddleVR.VRK_SPACE) && !IsAnimActive)
		//	StartCoroutine("returnToStart");
		//if (MiddleVR.VRDeviceMgr.IsWandButtonToggled(4) && !IsAnimActive)
		//	StartCoroutine("returnToStart");	
	}

	//smoothly go back to the start, in case we get upside down or lost
	IEnumerator returnToStart(){
		IsAnimActive = true;
		float animPosition = 0f;
		Quaternion oldRot = transform.rotation;
		Vector3 oldPos = transform.position;

		while (animPosition < 1f) {
			transform.rotation = Quaternion.Slerp(oldRot, startRot, animPosition);
			transform.position = Vector3.Lerp(oldPos, startPos, animPosition);	
			yield return new WaitForEndOfFrame();
			animPosition += Time.deltaTime / 4f;
		}

		yield return new WaitForEndOfFrame();
		IsAnimActive = false;
	}

	void rotateScene(string direction, float amount){
		//disable this if some other mode is using the joystick.
		if (sliceManager.pointedAtSlices || brainMaster.buildABrainActive)
			return;

		//always rotate the world relative to the camera's local orientation.
		Vector3 localVector = Vector3.zero;
		if (direction.StartsWith("h"))
			localVector = transform.TransformVector(Vector3.forward);
		if (direction.StartsWith("v"))
			localVector = transform.TransformVector(Vector3.right);

		//control the speed of the motion here.
		amount *= Time.deltaTime * 25f; 

		transform.RotateAround(anchor.transform.position, localVector, amount);
	}

}
