using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionPicker : MonoBehaviour
{
    //public Transform partName;
    private brainPart3 part;

    public string hitPoint;
    public int scanCounter = 0;

    void OnEnable() {
        OnDisable();
    }

    void OnDisable() {
        transform.position = new Vector3(0f, -100f, 0f);
        scanCounter = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) scanIt();

        //subtle animation
        transform.Rotate(Vector3.forward, 6f * Time.deltaTime);
    }

    void scanIt(){
        if (menuMaster.currentTool != menuMaster.Tools.Identify) return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)){
            transform.position = hit.point;
            transform.LookAt(Camera.main.transform.position);
            scanCounter++;

            //just to make it easier to copy and paste into the database
            if (Application.isEditor)
                hitPoint = "new Vector3(" + transform.position.x.ToString("F5") + "f, " + transform.position.y.ToString("F5") + "f, " + transform.position.z.ToString("F5") + "f),";           
        }
    }

}
