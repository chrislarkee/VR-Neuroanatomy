using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragObject : MonoBehaviour
{
    public GameObject target;

    public bool snapEnabled = false;
    private bool isGrabbed = false;
    private float cameraDistance;
    private Ray camRay;

    private Vector3 originalPos;        //cache the original position, so we can restore it later 
    private Vector3 targetPosition;     //used by mode 2

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        if (target != null)
            targetPosition = target.transform.position;
        else
            targetPosition = originalPos;

        //originalColor = rends[0].sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
        {
            //the basic mouse following movement
            camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Vector3.Distance(originalPos, camRay.GetPoint(cameraDistance)) > 0.3f)
            {
                transform.position = camRay.GetPoint(cameraDistance);
            }

            //snapping: 0.4 mazsww234ance(transform.position, originalPos) < 0.4f && snapEnabled)
            {
                isGrabbed = false;
                transform.position = originalPos;
                snapEnabled = false;
                Debug.Log("snap!");
                //correct answer event
            }
        }

    }
    public void OnMouseUp()
    {
        isGrabbed = false;

        if (Vector3.Distance(transform.position, targetPosition) >= 0.4f)
        {
            snapEnabled = true;
        }
        BrainMaster2.previousTouch = gameObject.GetComponent<brainPart3>();

        //timeOfLastClick = Time.time; //cache the current time for next click
    }
    void OnMouseDown()
    {
        //if (modeSwitcher.mode != 0) return;
        isGrabbed = true;
        //cameraDistance should be to original pos for mode 0, targetpos for mode 2
        cameraDistance = Vector3.Distance(Camera.main.transform.position, originalPos);
    }
}
