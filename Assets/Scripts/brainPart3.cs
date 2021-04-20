//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


public class brainPart3 : MonoBehaviour
{
    //shared references
    public bool isGrabbed = false;
    private Vector3 originalPos;        //cache original position  
    private float timeOfLastClick = 0f; //to differentiate between single & double clicks
    private GameObject dropBox;

    //color
    public static brainPart3 redPart;   //Global variable
    private bool amIRed = false;        //Local variable
    private Material originalColor;     //caches
    private List<MeshRenderer> rends;
    private Tween flashing;

    //dragObject components
    private float cameraDistance;
    private Vector3 targetPoint;

    // Start is called before the first frame update
    void Start() {
        originalPos = transform.position;

        //the if statements prevents redundant gameobject searches
        

        //populate this list and store the result
        rends = new List<MeshRenderer>();
        for (int i = 0; i <= transform.childCount - 1; i++) {
            if (!transform.GetChild(i).name.StartsWith("Label")) {
                rends.Add(transform.GetChild(i).GetComponent<MeshRenderer>());
            }
        }

        originalColor = rends[0].sharedMaterial;
    }


    // Update is called once per frame
    void Update(){
        dragMove();
    
        //restores color from red to original
        if (amIRed && redPart != this)
            colorRestore();
    }

    public void OnMouseUp(){
        isGrabbed = false;
        dropBox = null;                
        
        //double click = toggle positions
        //if (Vector3.Distance(transform.position, originalPos) > 0.1f)

        //if (Vector3.Distance(transform.position, targetPosition) >= 0.4f)
        //{
        //    snapEnabled = false;
        //}
        BrainMaster2.previousTouch = gameObject.GetComponent<brainPart3>();   
    }

    public void OnMouseOver() {
        //don't show ID in quiz mode for difficulty.
        //don't show ID in slice mode because the slicing plane doesn't affect colliders, making scanning weird.
        if (menuMaster.currentMode == menuMaster.Modes.Quiz) {
            InfoUnderCursor.setText("");
            return;
        } 

        //update the caption
        string tempText = transform.name;
        if (tempText.EndsWith("CL") || tempText.EndsWith("CR"))
            tempText = tempText.Substring(0, tempText.Length - 3);
        InfoUnderCursor.setText(tempText);
    }

    void OnMouseDown() {
        isGrabbed = true;
        cameraDistance = Vector3.Distance(Camera.main.transform.position, originalPos);

        if (Time.time - timeOfLastClick > 0.5f) {
            //single click actions.
            if (menuMaster.currentTool == menuMaster.Tools.Identify) selectIt();
            //CursorMode.Move's single click is handled by draggingBehaviour
        } else {
            //double-click actions.
            if (menuMaster.currentTool == menuMaster.Tools.Identify) colorRestore();
            float distanceMoved = Vector3.Distance(transform.position, originalPos);
            if (menuMaster.currentTool == menuMaster.Tools.Move && distanceMoved <= 0.2f) slideOut();
            else if (menuMaster.currentTool == menuMaster.Tools.Move) putBack();
        }

        timeOfLastClick = Time.time;
    }

    void dragMove() {
        if (menuMaster.currentMode == menuMaster.Modes.Slice) return;
        if (menuMaster.currentTool != menuMaster.Tools.Move) return;

        if (isGrabbed) {
            //snap to the bucket, if there is a bucket available
            if (menuMaster.currentMode == menuMaster.Modes.Quiz && dropBox == null) dropBox = GameObject.Find("Drop Box");
            if (menuMaster.currentMode == menuMaster.Modes.Quiz && Input.mousePosition.x < Screen.width * 0.2f) {
               if (dropBox != null)
                    cameraDistance = 4f; //Vector3.Distance(Camera.main.transform.position, magicBucket.transform.position);
            }

            targetPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance);
            targetPoint = Camera.main.ScreenToWorldPoint(targetPoint);

            if (Vector3.Distance(targetPoint, originalPos) <= 0.3f) {
                transform.position = originalPos;
            } 
            else {
                transform.position = targetPoint;
            }
        }
    }

    public void selectIt() {
        //update the caption
        string tempText = transform.name;
        if (tempText.EndsWith("CL") || tempText.EndsWith("CR"))
            tempText = tempText.Substring(0, tempText.Length - 3);
        InfoUnderCursor.setText(tempText);

        //paint it red
        redPart = this;
        amIRed = true;
        Color red = new Color32(200, 43, 43, 255); //red
        flashing.Kill(false);
        foreach (MeshRenderer mr in rends) {
            mr.material.DOColor(red, 1.5f);
        }
    }
    public void slideOut() {
        if (menuMaster.currentMode == menuMaster.Modes.Slice) return;
        //Vector3 directionV = Vector3.Normalize(transform.position - transform.parent.position) * 3f;
        //Vector3 target = transform.position + directionV;
        Vector3 target = new Vector3(UnityEngine.Random.Range(-1f, 1f) * 2f, UnityEngine.Random.Range(-1f, 1f) * 2f, UnityEngine.Random.Range(-1f, 1f) * 2f);
        target = originalPos + target;

        BrainMaster2.previousTouch = this;
        transform.DOMove(target, 1.5f, false).SetEase(Ease.InOutCubic);
    }

    public void putBack() {
        if (menuMaster.currentMode == menuMaster.Modes.Slice) return;
        colorRestore();

        //    this one uses DOTween to create an animated move out
        transform.DOMove(originalPos, UnityEngine.Random.Range(0.9f, 2f), false).SetEase(Ease.InOutCubic);
        transform.DOScale(Vector3.one, 1f);
    }

    public void colorRestore() {
        flashing.Kill(false);

        if (rends[0].material.color == originalColor.color) return;
        //if (rends[0].material.color == originalColor.color) return;
        foreach (MeshRenderer mr in rends) {
            //restore to the shared material to avoid filling ram with redundant material instances
            mr.sharedMaterial = originalColor;
        }
        amIRed = false;
    }

    public void startFlashing(){
        flashing.Kill();
        //if (flashing != null && flashing.IsPlaying()) return;

        //assign a target color. maybe white isn't the best?
        Color flash = Color.white; 
        //yoyo from the current color to white and back, in a infinite loop
        flashing = rends[0].material.DOColor(flash, UnityEngine.Random.Range(0.4f, 0.6f)).SetLoops(-1, LoopType.Yoyo);    

        if (rends.Count > 1){
            for (int i = 1; i < rends.Count; i++)
                rends[i].material = rends[0].material;
        }
        flashing.Play();
    }

    public Vector3 originalPosition(){
        return originalPos;
    }

    public void vizScan(Plane sp){
        bool vizCheck;
        if (menuMaster.currentMode != menuMaster.Modes.Slice) {
            vizCheck = true;
        } else {
            //test just the center point. That should be sufficient, I suppose.
            //Idea: test all vertexes in the collider, see if ~80% are past the threshold
            vizCheck = sp.GetSide(transform.position);
        }

        //Debug.Log("Is " + transform.name + " visible? " + vizCheck.ToString());
        //if vizCheck is false, the object is invisible, and we can disable the collider.
        GetComponent<MeshCollider>().enabled = vizCheck;
    }
}
