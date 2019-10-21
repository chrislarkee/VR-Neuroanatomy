using UnityEngine;
using System.Collections;

public class pointAt : MonoBehaviour {

	//this method is triggered by the button, defined in pointAtEditor. It's never triggered in runtime.
	public void connectLine(){
		//initialization values
	
        GameObject target = transform.parent.parent.gameObject;
        Vector3 endPoint = target.transform.position;

        //the line renderer is using world coordinates, so a conversion is needed.
        Vector3 startPoint = transform.TransformPoint(new Vector3 (0f, -0.0006f, 0f));

		//now that we have the coordinates we want, apply them:
		LineRenderer line = GetComponent<LineRenderer> ();
		line.SetPosition (0, startPoint);
		line.SetPosition (1, endPoint);

		//transform.parent = target.transform;
	}
}
