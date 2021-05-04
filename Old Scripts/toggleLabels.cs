using UnityEngine;
using System.Collections;
//using MiddleVR_Unity3D;

public class toggleLabels : MonoBehaviour {

	private int state = 99;
	private GameObject[] labelsOuter;
	private GameObject[] labelsInner;
	private MeshRenderer caption; 

	// Use this for initialization
	void Start () {
		labelsOuter = GameObject.FindGameObjectsWithTag ("LabelOuter");
		labelsInner = GameObject.FindGameObjectsWithTag ("LabelInner");
		caption = GameObject.Find("Caption").GetComponent<MeshRenderer>();
		togglelabels ();
	}
	
	// Update is called once per frame
	void Update () {
		//if (MiddleVR.VRDeviceMgr.IsKeyToggled (MiddleVR.VRK_X))
		//	togglelabels ();
		//if (MiddleVR.VRDeviceMgr.IsWandButtonToggled (2))
		//	togglelabels ();	
	}

	void togglelabels(){
		state++;
		if (state > 3)
			state = 0;

		Debug.Log ("label state = " + state.ToString());


		if (state == 0) { //state 0 = no labels at all
			turnOff (labelsOuter);
			turnOff (labelsInner);
			caption.enabled = false;
		}
		else if (state == 1) { //state 1 = just the pointer label
			turnOff (labelsOuter);
			turnOff (labelsInner);
			caption.enabled = true;
			brainPart.text.text = "Pointer Label Activated.";
		} else if (state == 2) {
			turnOn (labelsOuter);
			turnOff (labelsInner);
			caption.enabled = false;
		} else if (state == 3) {
			turnOff (labelsOuter);
			turnOn (labelsInner);
			caption.enabled = false;
		}
	}

	void turnOn(GameObject[] array){
		foreach (GameObject go in array) {
			go.SetActive (true);		
		}	
	}

	void turnOff(GameObject[] array){
		foreach (GameObject go in array) {
			go.SetActive (false);		
		}	
	}

}
