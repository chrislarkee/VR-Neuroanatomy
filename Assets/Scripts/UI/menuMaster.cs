using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuMaster : MonoBehaviour
{
    public enum Tools {None, Identify, Move, Rotate, Zoom};
    public static Tools currentTool;

    public enum Modes {None, Explore, Slice, Quiz, DTI};
    public static Modes currentMode;

    List<menuButton> buttons;
    public Button undoButton;
    public Button nextButton;

    public GameObject brain;
    private bool fmriLoaded = false;
    private GameObject fmri = null;
    public GameObject quizUI;

    public GameObject slicer;

    // Start is called before the first frame update
    void Start() {
        switchTool(2);
        switchModes(1);
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Alpha1)) switchModes(1);
        if (Input.GetKeyUp(KeyCode.Alpha2)) switchModes(2);
        if (Input.GetKeyUp(KeyCode.Alpha3)) switchModes(3);
        if (Input.GetKeyUp(KeyCode.Alpha4)) switchModes(4);

        if (Input.GetKeyUp(KeyCode.Alpha5)) switchTool(1);
        if (Input.GetKeyUp(KeyCode.Alpha6)) switchTool(2);
        if (Input.GetKeyUp(KeyCode.Alpha7)) switchTool(3);
        if (Input.GetKeyUp(KeyCode.Alpha8)) switchTool(4);

        if (Input.GetKeyUp(KeyCode.R)) smartRestore();
    }

    public void registerButton(menuButton newButton){
        if (buttons == null) buttons = new List<menuButton>();
        buttons.Add(newButton);
    }

    public void smartRestore(){
        if (menuMaster.currentTool == Tools.Rotate) {
            cameraOrbit cam = GameObject.Find("CameraOrbit").GetComponent<cameraOrbit>();
            cam.restoreRotation();
        } else if (menuMaster.currentTool == Tools.Zoom) {
            cameraOrbit cam = GameObject.Find("CameraOrbit").GetComponent<cameraOrbit>();
            cam.restoreZoom();
        }

        BrainMaster2 bm = (BrainMaster2)brain.GetComponent(typeof(BrainMaster2));
        bm.putBackPrevious();
    }

    public void verifyStates(){
        //only one button in each group can be highlighted.
        //the chosen one's mode will match the master's mode.
        foreach (menuButton mb in buttons){
            if (mb.thisTool != Tools.None) {
                if (mb.thisTool == menuMaster.currentTool)
                    mb.controlHighlight(true);
                else
                    mb.controlHighlight(false);
            } else if (mb.thisMode != Modes.None) {
                if (mb.thisMode == menuMaster.currentMode)
                    mb.controlHighlight(true);
                else
                    mb.controlHighlight(false);
            } else
                mb.controlHighlight(false);
        }
    }

    public void switchModes(int jump){
        menuMaster.currentMode = (Modes)jump;

        switchModes();
        verifyStates();
    }

    public void switchTool(int jump){
        menuMaster.currentTool = (Tools)jump;
        verifyStates();
    }

    public void switchModes() {
        //to be safe, turn off everything first
        brain.SetActive(false);
        if (fmri != null) fmri.SetActive(false);
        slicer.SetActive(false);
        quizUI.SetActive(false);
        nextButton.interactable = false;

        //then turn on what we need
        if (menuMaster.currentMode == menuMaster.Modes.Explore) {
            brain.SetActive(true);
        } else if (menuMaster.currentMode == menuMaster.Modes.Slice) {
            brain.SetActive(true);
            slicer.SetActive(true);
        } else if (menuMaster.currentMode == menuMaster.Modes.Quiz) {
            brain.SetActive(true);
            nextButton.interactable = true;            
            quizUI.SetActive(true);
        } else if (menuMaster.currentMode == menuMaster.Modes.DTI) {
            if (!fmriLoaded) {
                StartCoroutine("loadFMRI");
            } else {
                fmri.SetActive(true);
                slicer.SetActive(true);
            }
        }
    }

    IEnumerator loadFMRI(){
        fmriLoaded = true;
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        
        fmri = GameObject.Find("connectome");
        fmri.SetActive(true);
        slicer.SetActive(true);
    }
}
