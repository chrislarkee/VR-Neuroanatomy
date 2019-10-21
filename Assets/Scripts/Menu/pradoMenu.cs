using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pradoMenu : MonoBehaviour {

	public Text[] textblocks;

	//scrolling stuff
	private bool inMotion = false;
	private bool loadingStarted = false;
	private float newTarget = 0f;
	private Transform mover;

	// Use this for initialization
	void Start () {
        mover = transform.Find("Texts");

    }

	void Update(){
		if (OVRInput.GetUp(OVRInput.RawButton.DpadRight)) ScrollRight();;
		if (OVRInput.GetUp(OVRInput.RawButton.DpadLeft)) ScrollLeft();
	}


	public void ScrollRight(){
		if (newTarget <= -1500f) return;
		newTarget -= 500f;
		if (!inMotion) StartCoroutine("slider");
	}

	public void ScrollLeft(){
		if (newTarget >= 0f) return;
		newTarget += 500f;
		if (!inMotion) StartCoroutine("slider");
	}

	IEnumerator slider(){
		inMotion = true;
		float newX;
		while (Mathf.Abs(mover.localPosition.x - newTarget) > 0.1f) {
			yield return new WaitForEndOfFrame();
			newX = Mathf.Lerp (mover.localPosition.x, newTarget, Time.deltaTime * 6f);
			mover.localPosition = new Vector3(newX, 20f, 0f);
		}
		inMotion = false;
	}

	public void EnterVR(){
		//PlayerPrefs.Save();

		if (loadingStarted) return;
		loadingStarted = true;

		textblocks[5].transform.parent.GetComponent<Button>().OnSelect(null);

		StartCoroutine("asyncLoad");
	}

	IEnumerator asyncLoad(){
		AsyncOperation AO = SceneManager.LoadSceneAsync (1, LoadSceneMode.Single);		
		int dots = 0;
		string progress;
				
		while (!AO.isDone) {
			dots++;
			if (dots >= 21) dots = 0;
			progress = new string('.', dots);

			textblocks[5].text = "LOADING\r\n" + progress;
			yield return new WaitForSeconds(0.75f);
		}
	}
}
