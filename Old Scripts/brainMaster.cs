//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

//public class brainMaster : MonoBehaviour
//{
    //add dragObject components to brainMaster, will need to modify brainPart2
    //private List<brainPart3> parts = new List<brainPart3>();
    //public static brainPart3 previousRemoval;
    //public static GameObject activeSelection;

    //public static bool buildABrainActive = false;
    //public brainPart3[] buildABrain;
    //private int builderPointer = 0;

    //private float axisValueH = 0f;
    //private double tapCounter = 0.0d;
    //private double tapThreshold = 0.150f;


    // Use this for initialization
    //void Start()    {
    //    //get all the brain parts into an array
    //    GameObject[] partsGO = GameObject.FindGameObjectsWithTag("brainpart");

    //    foreach (GameObject go in partsGO) {
    //        parts.Add(go.GetComponent<brainPart3>());
    //    }
    //}

    // Update is called once per frame
//    void Update()
//    {
//        if (Application.isEditor)
//        {
//            if (Input.GetKeyDown(KeyCode.LeftArrow))
//                builder(false);
//            if (Input.GetKeyDown(KeyCode.RightArrow))
//                builder(true);
//        }

//        if (Mathf.Abs(axisValueH) > 0.1f)
//        {
//            brainMaster.buildABrainActive = true;
//            tapCounter += Time.deltaTime;

//            if (axisValueH > 0f && tapCounter > tapThreshold)
//            {
//                builder(true);
//                tapCounter = 0d;
//            }
//            else if (axisValueH < 0f && tapCounter > tapThreshold)
//            {
//                builder(false);
//                tapCounter = 0d;
//            }
//        }
//        else
//        {
//            tapCounter = 0d;
//        }
//    }

//    called by the "reset" UI button
//    public void putBackAll()
//    {
//        if (toggleBrains.mode != 0) return;
//        if (brainMaster.buildABrainActive) switchToNormal();

//        foreach (brainPart3 part in parts)
//        {
//            part.gameObject.SetActive(true);
//            part.putBack();
//        }
//    }

//    public void switchIt()
//    {
//        brains[0].SetActive(!brains[0].activeSelf);
//        brains[1].SetActive(!brains[1].activeSelf);

//        Text text = GameObject.FindGameObjectWithTag("Caption").GetComponent<Text>();
//        if (brains[1].activeInHierarchy)
//            text.text = "";
//    }

//    called by the "Undo" UI button
//    public void putBackOne()
//    {
//        if (toggleBrains.mode != 0) return;
//        if (previousRemoval == null) return;

//        previousRemoval.gameObject.SetActive(true);
//        previousRemoval.putBack();

//        previousRemoval = null;
//    }

//    public void switchToNormal()
//    {
//        if (toggleBrains.mode != 0) return;
//        foreach (brainPart3 part in parts)
//        {
//            part.gameObject.SetActive(true);
//            part.putBackInstant();
//        }
//    }

//    void incrementstates()
//    {
//        state++;
//        if (state > maxState)
//            state = 0u;
//        //debug.log (state);

//        switch (state)
//        {
//            case 0:
//                //this is the reset state.
//                brainMaster.buildABrainActive = false;
//                foreach (brainPart2 part in parts)
//                {
//                    part.gameobject.setactive(true);
//                    part.quickreset();
//                }
//                brainPart2.text.text = "mode: reset";
//                break;
//            case 1:
//                //this is the 'build a brain' mode
//                brainMaster.buildABrainActive = true;
//                builderPointer = -1;
//                builder(true);
//                brainPart2.text.text = "mode: building a brain";
//                break;
//            case 2:
//                //this is the fornix isolation state.
//                brainMaster.buildABrainActive = false;
//                foreach (brainPart2 part in parts)
//                {
//                    part.gameobject.setactive(true);
//                    part.quickreset();
//                    part.filter(state);
//                }
//                brainMaster.text.text = "mode: fornix isolation";
//                break;
//            case 3:
//                //this is the caudate isolation state.
//                brainMaster.buildABrainActive = false;
//                foreach (brainPart2 part in parts)
//                {
//                    part.gameobject.setactive(true);
//                    part.quickreset();
//                    part.filter(state);
//                }
//                brainPart2.text.text = "mode: caudate isolation";
//                break;
//        }
//    }

//}
