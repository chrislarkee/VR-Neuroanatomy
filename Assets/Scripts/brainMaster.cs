using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class brainMaster : MonoBehaviour {

	private List<brainPart2> parts = new List<brainPart2> ();
    public static brainPart2 previousRemoval;
	public static GameObject activeSelection;
    
    public static bool buildABrainActive = false;
    public brainPart2[] buildABrain;
	private int builderPointer = 0;
	
	private float axisValueH = 0f;
	private double tapCounter = 0.0d;
	private double tapThreshold = 0.150f;


	// Use this for initialization
	void Start () {
		//get all the brain parts into an array
		GameObject[] partsGO = GameObject.FindGameObjectsWithTag("brainpart");

		foreach (GameObject go in partsGO) {
			parts.Add(go.GetComponent<brainPart2> ());
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.isEditor) {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            	builder (false);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                builder (true);
        }

        if (OVRInput.GetUp(OVRInput.RawButton.A))
            putBackOne();
        if (OVRInput.GetUp(OVRInput.RawButton.B))
            putBack();
        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch) || OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
            switchToNormal();

        //wand controls
        axisValueH = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x;
        if (Mathf.Abs(axisValueH) < 0.1f) axisValueH = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x;

        if (Mathf.Abs (axisValueH) > 0.1f) {
            brainMaster.buildABrainActive = true;
            tapCounter += Time.deltaTime;

			if (axisValueH > 0f && tapCounter > tapThreshold) {
				builder (true);
				tapCounter = 0d;	
			} else if (axisValueH < 0f && tapCounter > tapThreshold) {
				builder (false);
				tapCounter = 0d;
			}			
		} else {
			tapCounter = 0d;		
		}
	}

	void putBack(){
        if (brainMaster.buildABrainActive) switchToNormal();

        foreach (brainPart2 part in parts) {
			part.gameObject.SetActive (true);
            part.putBack();
		}	
	}

    void putBackOne(){
        if (previousRemoval != null) {
            previousRemoval.gameObject.SetActive(true);
            previousRemoval.putBack();
        }
    }

    void switchToNormal(){
        brainMaster.buildABrainActive = false;
        foreach (brainPart2 part in parts)
        {
            part.quickReset();
        }
    }




    //void incrementstates() {
    //    state++;
    //    if (state > maxState)
    //        state = 0u;
    //    //debug.log (state);

    //    switch (state)
    //    {
    //        case 0:
    //            //this is the reset state.
    //            brainMaster.buildABrainActive = false;
    //            foreach (brainPart2 part in parts)
    //            {
    //                part.gameobject.setactive(true);
    //                part.quickreset();
    //            }
    //            brainPart2.text.text = "mode: reset";
    //            break;
    //        case 1:
    //            //this is the 'build a brain' mode
    //            brainMaster.buildABrainActive = true;
    //            builderPointer = -1;
    //            builder(true);
    //            brainPart2.text.text = "mode: building a brain";
    //            break;
    //        case 2:
    //            //this is the fornix isolation state.
    //            brainMaster.buildABrainActive = false;
    //            foreach (brainPart2 part in parts)
    //            {
    //                part.gameobject.setactive(true);
    //                part.quickreset();
    //                part.filter(state);
    //            }
    //            brainMaster.text.text = "mode: fornix isolation";
    //            break;
    //        case 3:
    //            //this is the caudate isolation state.
    //            brainMaster.buildABrainActive = false;
    //            foreach (brainPart2 part in parts)
    //            {
    //                part.gameobject.setactive(true);
    //                part.quickreset();
    //                part.filter(state);
    //            }
    //            brainPart2.text.text = "mode: caudate isolation";
    //            break;
    //    }
    //}

    void builder(bool dir) {
		//if (!brainMaster.buildABrainActive || sliceManager.pointedAtSlices)
		//	return;

		//reset
		foreach (brainPart2 part in parts) {
			part.quickOut();
		}

		if (dir && builderPointer < buildABrain.Length - 1)
			builderPointer++;
		if (!dir && builderPointer > 0)
			builderPointer--;

		for (int i = 0; i <= builderPointer; i++) {
            buildABrain[i].quickReset();
			//buildABrain [i].putBack();
		}

		brainPart2.text.text = buildABrain[builderPointer].transform.name + " added";
	}
}
