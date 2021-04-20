using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modeSwitcher : MonoBehaviour {
    //public static int mode;

    public GameObject[] brains;
    public GameObject quizUI;

    private void Start() {
        switchMode();
    }

    public void switchMode(){       
        brains[0].SetActive(false);
        brains[1].SetActive(false);
        quizUI.SetActive(false);

        if (menuMaster.currentMode == menuMaster.Modes.Explore) {
            brains[0].SetActive(true);
        } else if (menuMaster.currentMode == menuMaster.Modes.Quiz) {
            brains[0].SetActive(true);
            quizUI.SetActive(true);
        } else if (menuMaster.currentMode == menuMaster.Modes.DTI) {
            brains[1].SetActive(true);
        }
    }
}
