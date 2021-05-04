using UnityEngine;
using System.Collections;

public class killOnClick : MonoBehaviour {

	#if UNITY_EDITOR
	void OnMouseDown(){
        clickOn();
	}
	#endif

	//void Update(){
	//	if (MiddleVR.VRDeviceMgr.IsWandButtonToggled (0))
	//		clickAction ();
	//}

	//protected void OnMVRWandButtonPressed(VRSelection iSelection) {
	//	clickAction ();
	//}

	private void clickOn(){
        Destroy(gameObject);
	}


}
