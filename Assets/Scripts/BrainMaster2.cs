using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrainMaster2 : MonoBehaviour
{
    //add dragObject components to brainMaster, will need to modify brainPart2
    private List<brainPart3> parts = new List<brainPart3>();
    public static brainPart3 previousTouch;
    public static GameObject activeSelection;

    //slidepart caches
    Camera cam;

    // Start is called before the first frame update
    void Start() {
        //get all the brain parts into an array
        GameObject[] partsGO = GameObject.FindGameObjectsWithTag("brainpart");
        cam = Camera.main;

        foreach (GameObject go in partsGO) {
            parts.Add(go.GetComponent<brainPart3>());
        }
    }

    public void Update() {
        //if (Input.GetKeyDown(KeyCode.F)) putBackAll();

        if (Input.GetKey(KeyCode.W)) slidePart(Vector2.up);
        if (Input.GetKey(KeyCode.A)) slidePart(Vector2.left);
        if (Input.GetKey(KeyCode.S)) slidePart(Vector2.down);
        if (Input.GetKey(KeyCode.D)) slidePart(Vector2.right);

        if (Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.D)) previousTouch = null;
    }

    private void slidePart(Vector2 direction){
        if (menuMaster.currentMode == menuMaster.Modes.Slice) return;

        //if the user hasn't clicked on anything, find something in the center.
        if (previousTouch == null) {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) {
                previousTouch = (brainPart3)hit.transform.GetComponent(typeof(brainPart3));
            }        
        }
        if (previousTouch == null) return;  //if we still can't find anything, abort

        //slide it along the screen plane.
        Vector3 newPos = cam.WorldToViewportPoint(previousTouch.transform.position);
        newPos += (Vector3)direction * Time.deltaTime * 0.75f;
        previousTouch.transform.position = cam.ViewportToWorldPoint(newPos);    
    }

    public void putBackPrevious() {
        if (previousTouch == null) {
            putBackAll();
            return;
        }
		
		audioManager.play("Reset_Undo");
        previousTouch.putBack();
        previousTouch = null;
    }
    public void putBackAll()
    {
        DOTween.KillAll();
        audioManager.play("Reset_Undo");
        foreach (brainPart3 part in parts) {
            part.putBack();
        }
        previousTouch = null;
    }

    public void stopAllFlashing() {
        foreach (brainPart3 part in parts) {
            part.colorRestore();
        }
        previousTouch = null;
    }

    public void randomize(){
        Vector3 randomPosition;
        foreach (brainPart3 part in parts) {
            if (part.name.Contains("Ventricle")) continue;
            randomPosition = new Vector3(
                UnityEngine.Random.Range(-2.5f, 2.5f), 
                UnityEngine.Random.Range(0.5f, 1.5f), 
                UnityEngine.Random.Range(-2.5f, 2.5f)
            );
            part.transform.DOMove(randomPosition, 4f, false).SetEase(Ease.OutCubic);
        }
    }

    public void checkColliderVisibility(Plane sp){
        foreach (brainPart3 part in parts) {
            part.vizScan(sp);
        }
    }
}
