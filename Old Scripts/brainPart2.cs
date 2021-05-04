//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using DG.Tweening;
//using System;

//public class brainPart2 : MonoBehaviour
//{
//    shared references
//    public toggleBrains;
//    public static Text text;    //the UI element displaying the object's name

//    caches
//    private Vector3 originalPos;        //cache the original position, so we can restore it later   
//    private float timeOfLastClick = 0f; //to differentiate between single & double clicks
//    private bool isGrabbed = false;     //this is used by the dragging feature

//    color
//    public static brainPart2 redPart;   //a GLOBAL variable about the color's state
//    private bool amIRed = false;        //a LOCAL variable about the color's state
//    private Material originalColor;     //cache the original material, so we can restore it
//    private List<MeshRenderer> rends;   //it's a list because there may be more than 1 child??    


//    dragObject components
//    public GameObject target;

//    public bool snapEnabled = false;
//    private bool isGrabbed = false;
//    private float cameraDistance;
//    private Ray camRay;

//    private Vector3 originalPos;      //cache the original position, so we can restore it later 
//    private Vector3 targetPosition;     //used by mode 2

//    void Start()
//    {
//        initialize this variable at runtime
//        originalPos = transform.position;

//        if (toggleBrains.mode != 0)
//        {
//            if (target != null)
//                targetPosition = target.transform.position;
//            else
//                targetPosition = originalPos;
//        }

//        the if statements prevents redundant gameobject searches
//        if (text == null)
//            text = GameObject.FindGameObjectWithTag("Caption").GetComponent<Text>();

//        populate this list and store the result
//        rends = new List<MeshRenderer>();
//        for (int i = 0; i <= transform.childCount - 1; i++)
//        {
//            if (!transform.GetChild(i).name.StartsWith("Label"))
//            {
//                rends.Add(transform.GetChild(i).GetComponent<MeshRenderer>());
//            }
//        }

//        originalColor = rends[0].sharedMaterial;
//    }

//    private void Update()
//    {
//        colorRestore
//        {
//            if (amIRed && redPart != this)
//                colorRestore();
//        }

//        dragObject merge
//        if (isGrabbed)
//        {
//            the basic mouse following movement
//            camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Vector3.Distance(originalPos, camRay.GetPoint(cameraDistance)) > 0.3f)
//            {
//                transform.position = camRay.GetPoint(cameraDistance);
//            }

//        snapping: 0.4 mazsww234ance(transform.position, originalPos) < 0.4f && snapEnabled)
//            {
//                isGrabbed = true;
//                transform.position = originalPos;
//                snapEnabled = true;
//                Debug.Log("snap!");
//                correct answer event
//            }
//}
//    }

//    single click and hold = dragObject, add this, not in script now...
//    public void OnMouseUp()
//{
//    if (toggleBrains.mode != 0) return;

//    if (Time.time - timeOfLastClick > 0.5f)
//    {
//        single click = update caption
//            updateInfo();
//    }
//    else
//    {
//        double click = toggle positions
//            if (Vector3.Distance(transform.position, originalPos) > 0.1f)
//            putBack();
//        else
//            moveOut();

//        isGrabbed = true;

//        if (Vector3.Distance(transform.position, targetPosition) >= 0.4f)
//        {
//            snapEnabled = false;
//        }
//        BrainMaster2.previousRemoval = gameObject.GetComponent<brainPart3>();
//    }
//}
//public void OnMouseDrag()
//{
//    isGrabbed = true;

//    if (Vector3.Distance(transform.position, targetPosition) >= 0.4f)
//    {
//        snapEnabled = true;
//    }
//    brainMaster.previousRemoval = gameObject.GetComponent<brainPart2>();

//    timeOfLastClick = Time.time; //cache the current time for next click
//}

//dragObject merge
//    void OnMouseDown()
//{
//    if (toggleBrains.mode != 0)
//        return;
//    isGrabbed = true;
//    cameraDistance should be to original pos for mode 0, targetpos for mode 2

//    cameraDistance = Vector3.Distance(Camera.main.transform.position, originalPos);
//        }


//void updateInfo()
//{
//    update the caption
//        string tempText = transform.name;
//    if (tempText.EndsWith("CL") || tempText.EndsWith("CR"))
//        tempText = tempText.Substring(0, tempText.Length - 3);
//    text.text = tempText;

//    paint it red
//    redPart = this;
//    amIRed = true;
//    Color red = new Color32(200, 43, 43, 255); //red
//    foreach (MeshRenderer mr in rends)
//    {
//        mr.material.DOColor(red, 1.5f);
//    }
//}

////this one uses DOTween to create an animated move out
//public void moveOut()
//{
//    colorRestore();

//    determine a new position
//    Vector3 directionV = Vector3.Normalize(transform.position - transform.parent.position) * 2f;
//    directionV.y += 2f;
//    Vector3 target = transform.position + directionV;

//    so the undo button can find this part
//    brainMaster.previousRemoval = this;

//    do the animation

//    transform.DOMove(target, 0.75f, false).SetEase(Ease.InOutCubic);
//    transform.DOScale(Vector3.one * 0.75f, 0.75f);
//}

//this one just sets the variables instantly.
//public void moveOutInstant()
//{
//    colorRestore();
//    Vector3 directionV = Vector3.Normalize(originalPos - transform.parent.position) * 2f;
//    //directionV.y += 2f;
//    brainMaster.previousRemoval = this;
//    transform.position = originalPos + directionV;
//    transform.localScale = Vector3.one * 0.75f;
//}

//public void putBack()
//{
//    if (toggleBrains.mode != 0)
//        colorRestore();
//    this one uses DOTween to create an animated move out
//        transform.DOMove(originalPos, UnityEngine.Random.Range(0.9f, 2f), false).SetEase(Ease.InOutCubic);
//    transform.DOScale(Vector3.one, 1f);
//}

//reset all brain pieces to original positions
//    public void putBackInstant()
//{
//    colorRestore();
//    transform.position = originalPos;
//    transform.localScale = Vector3.one;
//}

//void colorRestore()
//{
//    Debug.Log("Restoring Color: " + originalColor.name);
//    if (!amIRed) return;    //abort if we don't need to do this.

//    foreach (MeshRenderer mr in rends)
//    {
//        restore to the shared material to avoid filling ram with redundant material instances
//            mr.sharedMaterial = originalColor;
//    }
//    amIRed = false;
//}

//public object setActive()
//{
//    toggleBrains.switchIt(Button);
//    if (Button)
//    {
//        GameObject.FindGameObjectWithTag("Caption").GetComponent<Button>().interactable = false;


//    }

//}

//private object Button(string v)
//{
//    throw new NotImplementedException();
//}
//}
