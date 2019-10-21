using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class catchClicks : MonoBehaviour {

	private brainPart part;
    private List<MeshRenderer> rends;
	private Material cachedMaterial;
	private bool isThisRed = false;

	// Use this for initialization
	void Start () {
        rends = new List<MeshRenderer>();
        for (int i = 0; i <= transform.parent.childCount; i++){
            if (!transform.GetChild(i).name.StartsWith("Label")) {
                rends.Add(transform.GetChild(i).GetComponent<MeshRenderer>());
            }
        }

        cachedMaterial = rends[0].sharedMaterial;
        part = GetComponent<brainPart>();

		//if it doesn't have the component, try its parent
		if (part == null)
			part = GetComponentInParent<brainPart> ();	
	}

	#if UNITY_EDITOR
		void OnMouseEnter(){
			part.getName ();
			if (!isThisRed)
				StartCoroutine("turnThisRed");		
		}

		void OnMouseExit(){
			ColorReset();
		}

		void OnMouseDown(){
			clickAction ();
		}
	#endif


	//protected void OnMVRWandEnter(VRSelection iSelection) {
	//	part.getName ();
	//	if (!isThisRed)
	//		StartCoroutine("turnThisRed");		
	//} 
		
	//protected void OnMVRWandButtonPressed(VRSelection iSelection) {
	//	clickAction ();
	//}

	//protected void OnMVRWandExit(VRSelection iSelection) {
	//	ColorReset();
	//}


	void clickAction(){
		part.StopAllCoroutines ();
		//get a point to shrink around
		//Mesh targetmesh = GetComponent<MeshCollider> ().sharedMesh;
		//Vector3 endPoint = targetmesh.vertices[(int)(targetmesh.vertices.Length / 2f)];
		//endPoint = transform.TransformPoint (endPoint);
		//StartCoroutine (part.shrink (endPoint)); 

		StartCoroutine (part.expand());
		//Resources.UnloadUnusedAssets();
	}

	public void turnThisRedNow(){
		Material mat = rends[0].material;
		mat.color = new Color (0.866f, 0.019f, 0.019f, 1f);
		isThisRed = true;
	}

	public IEnumerator turnThisRed() {
		//ColorReset();
		isThisRed = true;
        Material mat = rends[0].material;
        Color StartCol = mat.color;
		Color TargetCol = new Color(0.866f, 0.019f, 0.019f, 1f); //a bright red. "Monza"

		float colorPosition = 0f;

		while (colorPosition < 1f) {
            colorPosition += Time.deltaTime;
			mat.color = Color.Lerp(StartCol, TargetCol, colorPosition);
			yield return new WaitForEndOfFrame ();
		}

	}

	void ColorReset() {
        //part.StopAllCoroutines ();

        //DestroyImmediate(rend.material);
        foreach (MeshRenderer m in rends){
            m.material = cachedMaterial;
        }
        
		//Resources.UnloadUnusedAssets();

		isThisRed = false;
	}
		
}
