using UnityEngine;
using System.Collections;

public class brainPart : MonoBehaviour {

	private Vector3 originalPos;
	private Vector3 orignalSize;
	public static TextMesh text;
	private static GameObject anchor;

	public bool InFornixGroup = false;
	public bool InCaudateGroup = false;
	public bool inTransition = false;
    public bool isVisible = true;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
		orignalSize = transform.localScale;	
		if (text == null)
			text = GameObject.Find ("Caption").GetComponent<TextMesh> ();
		if (anchor == null)
			anchor = GameObject.Find("RotationPoint");
	}
			
	public void getName(){
		text.text = transform.name;
		//Debug.Log (Time.time);
	}

	public IEnumerator putBack(){
		//if (Vector3.Distance (transform.position, originalPos) < 0.01f)
		//yield break;
		if (inTransition)
			yield break;

		inTransition = true;

		float moveLength = 1f;
		float movePosition = 0f;

		Vector3 currentPos = transform.position;
		Vector3 currentSize = transform.localScale;

		while (movePosition < moveLength) {
            movePosition += Time.deltaTime;
			transform.position = Vector3.Lerp (currentPos, originalPos, movePosition / moveLength);
			transform.localScale = Vector3.Lerp(currentSize, orignalSize, movePosition / moveLength);
			yield return new WaitForEndOfFrame ();
		}

		inTransition = false;
		quickReset ();
	}

	public IEnumerator slideUp (){
		if (inTransition)
			yield break;

		inTransition = true;
		Vector3 targetPos = new Vector3 (originalPos.x, Random.Range (20f, 40f), originalPos.z);

		float movePosition = 0f;
		float moveDuration = 3f;

		while (movePosition < moveDuration) {
			transform.position = Vector3.Lerp(originalPos, targetPos, movePosition / moveDuration);
            movePosition += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}

		inTransition = true;
		gameObject.SetActive (false);
	}

	public IEnumerator shrink(Vector3 point){
		if (inTransition)
			yield break;

		inTransition = true;		
		Vector3 currentSize = transform.localScale;

		float shrinkPosition = 0f;
		float shrinkDuration = 1f;

		while (shrinkPosition < shrinkDuration) {
			transform.localScale = currentSize * ((shrinkDuration - shrinkPosition) / shrinkDuration);
			transform.position = Vector3.Lerp (transform.position, point, shrinkPosition / shrinkDuration);
            shrinkPosition += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}

		inTransition = false;
		gameObject.SetActive (false);
	}

	//slide out in a random direction. not exactly 'expand'
	public IEnumerator expand(){
		if (inTransition)
			yield break;

		inTransition = true;

		float expandPosition = 0f;
		float expandDuration = 2f;

		Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

		while (expandPosition < expandDuration) {
            expandPosition += Time.deltaTime;
			transform.Translate(direction * Time.deltaTime * 30f);
			//transform.position = Vector3.MoveTowards(transform.position, point, -0.1f);
			//transform.position = Vector3.Lerp (startPoint, point, shrinkPosition / shrinkDuration);

			yield return new WaitForEndOfFrame ();
		}

		inTransition = false;
		gameObject.SetActive (false);
	}


	public void quickReset(){
		StopAllCoroutines ();
		gameObject.SetActive (true);
		transform.position = originalPos;
		transform.localScale = orignalSize;
	}

	public void quickHide (){
		StopAllCoroutines ();
		gameObject.SetActive (false);
	}

	public void filter(uint state){
		if (state == 2 && !InFornixGroup) {
			quickHide ();
		}
		if (state == 3 && !InCaudateGroup) {
			quickHide ();
		}
	}

    void checkVisibility(){


    }

}
