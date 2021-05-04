using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class brainPart3 : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    //shared references
    public static Text text;    //the UI element displaying the object's name

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
        if (text == null)
            text = GameObject.FindGameObjectWithTag("Caption").GetComponent<Text>();

        //populate this list and store the result
        rends = new List<MeshRenderer>();
        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            if (!transform.GetChild(i).name.StartsWith("Label"))
            {
                rends.Add(transform.GetChild(i).GetComponent<MeshRenderer>());
            }
        }
        originalColor = rends[0].sharedMaterial;
    }


    // Update is called once per frame
    void Update(){
         //if (Input.GetKeyUp(KeyCode.F)) startFlashing();
         draggingBehaviour();
    
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
        BrainMaster2.previousRemoval = gameObject.GetComponent<brainPart3>();
        
    }

    void OnMouseDown() {
        isGrabbed = true;
        cameraDistance = Vector3.Distance(Camera.main.transform.position, originalPos);

        if (Time.time - timeOfLastClick > 0.5f) {
            //single click = update caption
            if (modeSwitcher.mode == 0) updateInfo();
        } else {
            //double-click actions.
            if (modeSwitcher.mode == 0) putBack();
            //quiz mode will use double click for submit
            if (modeSwitcher.mode == 1) updateInfo();
        }

        timeOfLastClick = Time.time;
    }

    void draggingBehaviour() {
        if (isGrabbed) {
            //snap to the bucket, if there is a bucket available
            if (modeSwitcher.mode == 1 && Input.mousePosition.x < Screen.width * 0.2f) {
               dropBox = GameObject.Find("Drop Box");
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

    public void updateInfo() {
        //update the caption
        string tempText = transform.name;
        if (tempText.EndsWith("CL") || tempText.EndsWith("CR"))
            tempText = tempText.Substring(0, tempText.Length - 3);
        text.text = tempText;

        //paint it red
        redPart = this;
        amIRed = true;
        Color red = new Color32(200, 43, 43, 255); //red
        flashing.Kill(false);
        foreach (MeshRenderer mr in rends) {
            mr.material.DOColor(red, 1.5f);
        }
    }

    public void moveOut() {
    //so the undo button can find this part
    BrainMaster2.previousRemoval = this;

    //determine a new position
    Vector3 directionV = Vector3.Normalize(transform.position - transform.parent.position) * 2f;
    //directionV.y += 2f;
    Vector3 target = transform.position + directionV;

    //do the animation
    transform.DOMove(target, 0.75f, false).SetEase(Ease.InOutCubic);
    transform.DOScale(Vector3.one* 0.75f, 0.75f);
    }

    public void putBack() {
       colorRestore();

        //    this one uses DOTween to create an animated move out
        transform.DOMove(originalPos, UnityEngine.Random.Range(0.9f, 2f), false).SetEase(Ease.InOutCubic);
        transform.DOScale(Vector3.one, 1f);
    }

    public void putBackInstant()
    {
        colorRestore();
        transform.position = originalPos;
        transform.localScale = Vector3.one;
    }

    void colorRestore() {
        flashing.Kill(false);

        if (rends[0].material.color == originalColor.color) return;
        //Debug.Log("Restoring Color: " + originalColor.name);
        foreach (MeshRenderer mr in rends) {
            //restore to the shared material to avoid filling ram with redundant material instances
            mr.sharedMaterial = originalColor;
        }
        amIRed = false;        
    }

    public void startFlashing(){
        if (flashing != null && flashing.IsPlaying()) return;

        //assign a target color. maybe white isn't the best?
        Color flash = Color.white; 
        //yoyo from the current color to white and back, in a infinite loop
        flashing = rends[0].material.DOColor(flash, UnityEngine.Random.Range(0.4f, 0.6f)).SetLoops(-1, LoopType.Yoyo);    
        flashing.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
