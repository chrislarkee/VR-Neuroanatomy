using UnityEngine;
using System.Collections;
//using MiddleVR_Unity3D;

public class sliceManager : MonoBehaviour {

	public Texture2D[] slices;
	private int currentSlice = -1;
	private Material sliceMat;

	//wand controls
	private float axisValueH = 0f;
	private double tapCounter = 0.0d;
	private double tapThreshold = 0.300f;

	public static bool pointedAtSlices = false;

	private Vector3 originalPos;
	private Quaternion originalRot;

	// Use this for initialization
	void Start () {
		sliceMat = GetComponent<MeshRenderer> ().sharedMaterial;
		changeSlice (true);
		originalPos = transform.position;
		originalRot = transform.rotation;			
	}
	
	// Update is called once per frame
	void Update () {
        //keyboard controls
        //if (MiddleVR.VRDeviceMgr.IsKeyToggled(MiddleVR.VRK_A))
        //	changeSlice(false);
        //if (MiddleVR.VRDeviceMgr.IsKeyToggled(MiddleVR.VRK_S))
        //	changeSlice(true);

        //wand controls
        axisValueH = 0f; //MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
		if (Mathf.Abs (axisValueH) > 0.1f && sliceManager.pointedAtSlices) {
            tapCounter += Time.deltaTime;

			if (axisValueH > 0f && tapCounter >= tapThreshold) {
				tapCounter = 0d;
				changeSlice (true);
			} else if (axisValueH < 0f && tapCounter >= tapThreshold) {
				changeSlice	(false);
				tapCounter = 0d;
			}
		} else {
			tapCounter = 0d;		
		}	
	}

	//protected void OnMVRWandEnter(VRSelection iSelection) {
	//	sliceManager.pointedAtSlices = true;
	//} 

	//protected void OnMVRWandExit(VRSelection iSelection) {
	//	sliceManager.pointedAtSlices = false;
	//}

	//protected void OnMVRWandButtonPressed(VRSelection iSelection) {
	//	transform.position = originalPos;
	//	transform.rotation = originalRot;		
	//}

	//true is up, false is down
	void changeSlice(bool dir){
		if (dir)
			currentSlice++;
		else
			currentSlice--;

		if (currentSlice >= slices.Length)
			currentSlice = 0;
		if (currentSlice < 0)
			currentSlice = slices.Length - 1;

		sliceMat.mainTexture = slices [currentSlice];	
	}
}
