using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	public static GameObject head;

	// Use this for initialization
	void Start () {
		if (head == null)
			head = GameObject.Find ("CenterEyeAnchor");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeInHierarchy)
			transform.LookAt(head.transform.position, head.transform.up);
	}
}
