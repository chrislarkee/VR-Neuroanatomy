using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleBrains : MonoBehaviour {
    public GameObject[] brains;
    
    void Update() {
        if (Application.isEditor && Input.GetKeyUp(KeyCode.X)) switchIt();
        if (OVRInput.GetUp(OVRInput.RawButton.X)) switchIt();
    }

    void switchIt(){
        brains[0].SetActive(!brains[0].activeSelf);
        brains[1].SetActive(!brains[1].activeSelf);
    }
}
