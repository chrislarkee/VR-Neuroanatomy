using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    //behavior variables
    const float distance = 4f;	    //also controls apparant onscreen size. may need tuning?
    public GameObject[] contents;	//public because the quiz generator scans it for correct/incorrect answers

    //caches
    private Vector3 targetPoint;	 //up here to avoid GC.
    private Vector3 contentOffset;   //up here to avoid GC. the vector of how much we've moved
    private Vector3 oldPosition;     //position in the previous frame

    private void OnDisable() {
        contents = new GameObject[0];
    }

    void OnEnable() {
        oldPosition = transform.position;
        updateContents();
    }

    void Update() {
        updatePosition();

        //we check on buttonUp to scan on deliberate, user committed actions, not inadvertent pass throughs
        if (Input.GetMouseButtonUp(0)) updateContents();
        if (Input.GetMouseButtonUp(1)) updateContents();
    }

    void updatePosition(){
        //keep this in the edge of the screen, regardless of the camera's zoom/rotation
        targetPoint = new Vector3(Screen.width * 0.2f, Screen.height * 0.45f, distance);
        transform.position = Camera.main.ScreenToWorldPoint(targetPoint);
		
		//keep 'contents' in the box when rotation, without using parenting or joints
        //this also prevents objects from being locked in, unless you move the camera and drag objects at the same time.
        if (transform.position != oldPosition) {                    //if we haven't moved, we can skip this step
            contentOffset = transform.position - oldPosition;       //calc a vector for how much we've moved since the last frame
            foreach (GameObject go in contents){
                go.transform.Translate(contentOffset, Space.World);              //apply that vector to each thing in the box
            }
        }

        oldPosition = transform.position;       //update our position cache for next time
    }
	

	//void OnCollisionExit(Collision other) {
	//	updateContents();		
	//}

    //void OnTriggerExit(Collider other) {
    //  updateContents();
    //}

    void updateContents(){
        //what's in the box?
        Collider[] thingsInTheBox;
        thingsInTheBox = Physics.OverlapBox(transform.position, transform.lossyScale * 0.45f);
        
        //convert the collider array into an shareable gameobject array
        contents = new GameObject[thingsInTheBox.Length];
        for (int i = 0; i < thingsInTheBox.Length; i++) {
            contents[i] = thingsInTheBox[i].gameObject;
        }

        //future: highlight things in the box using DoTween
    }
}
