using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slicingPlane : MonoBehaviour
{
    //slicing
    private float slicePosition = 0f;
    private Slicer[] slicerConfig;
    private int activeSlice = 0;

    //grabbing
    private bool isGrabbed = false;

    //other caches
    private List<Material> mats;                //shared materials only, for instancing
    private Text text;
    public SlicerControl slicerControl;
    private BoxCollider col;
    public BrainMaster2 bm;

    //DATA MANAGEMENT
    private class Slicer {
        //a custom class to avoid mixing up all these parameters
        public string name;             //the info bar will use this
        public Vector3 shaderAxis;      //the AXIS on the shader's clipping plane
        public Vector3 planeEulers;     //the EULERS of the slicing plane
        public Vector3 planeMovement;   //the VECTOR the slicing plane needs to move
        public bool defaultFlip;
    }

    Slicer newSlice (string name, Vector3 shaderAxis, Vector3 planeRotation, Vector3 planeMovement, bool flip) {
        //a shortcut for constructing this class in a single line
        Slicer myPlane = new Slicer();
        myPlane.name = name;
        myPlane.shaderAxis = shaderAxis.normalized;
        if (planeRotation.magnitude == 1)
            myPlane.planeEulers = planeRotation.normalized * 90f;
        else
            myPlane.planeEulers = planeRotation;
        myPlane.planeMovement = planeMovement.normalized;        
        myPlane.defaultFlip = flip;
        return myPlane;
    }

    //STARTUP
    public void OnEnable() {
        col = GetComponent<BoxCollider>();

        MeshRenderer[] renderers;
        if (menuMaster.currentMode == menuMaster.Modes.DTI)
            renderers = GameObject.Find("connectome").GetComponentsInChildren<MeshRenderer>();
        else
            renderers = GameObject.Find("blender-brain-opt").GetComponentsInChildren<MeshRenderer>();

        mats = new List<Material>();
        foreach (MeshRenderer mr in renderers) {
            if (!mats.Contains(mr.sharedMaterial))
                mats.Add(mr.sharedMaterial);
        }

        //normally, OnEnable is called before Start, but these conditions are just for re-enabling
        if (slicerConfig != null && menuMaster.currentMode == menuMaster.Modes.DTI)
        {
            slicePosition = 0;
            activeSlice = -1;
            switchSlicerOrientation();
        } else if (slicerConfig != null) {
            activeSlice = 0;
            slicePosition = 0;
            switchSlicerOrientation();
        }
    }
    void Start() {
        text = GameObject.FindGameObjectWithTag("Caption").GetComponent<Text>();

        //prepare the possible states all in one place
        slicerConfig = new Slicer[5];
        slicerConfig[0] = newSlice("Disabled",              Vector3.forward, Vector3.right, Vector3.zero, false);
        slicerConfig[1] = newSlice("Coronal Section",       new Vector3(0f, 1f, 0.4f), new Vector3 (18f, 0f, 90f), Vector3.forward, true);
        slicerConfig[2] = newSlice("Coronal Section Inverse", new Vector3(0f, 1f, 0.4f), new Vector3(18f, 0f, 90f), Vector3.forward, false);
        slicerConfig[3] = newSlice("Horizontal Section",    new Vector3(0f, -0.25f, 1f), new Vector3(101.25f, 0f, 0f), Vector3.down, true);    //aka transverse?
        slicerConfig[4] = newSlice("Sagittal Section",      Vector3.right, Vector3.up, Vector3.right, true);

        if (menuMaster.currentMode == menuMaster.Modes.DTI) activeSlice = -1;
        switchSlicerOrientation();
    } 

    public void OnDisable() {
        //trigger the 'disabled' config
        activeSlice = -1;
        switchSlicerOrientation();
    }

    //INTERACTIONS
    public void OnMouseEnter() {
        if (menuMaster.currentTool == menuMaster.Tools.Identify) col.enabled = false;
    }

    private void OnMouseUp() {
        if (menuMaster.currentTool == menuMaster.Tools.Rotate) switchSlicerOrientation();
    }

    private void OnMouseDown() {
        if (menuMaster.currentTool == menuMaster.Tools.Identify) col.enabled = false;
        if (menuMaster.currentTool == menuMaster.Tools.Move && !isGrabbed) StartCoroutine("dragMove");
    }

    private void Update() {
        //keyboard shortcuts
        if (Input.GetKey(KeyCode.A)) {
            slicePosition += Time.deltaTime * 0.3f;
            syncPlaneWithShader();
            slicerControl.reverseSync(slicePosition);
        } else if (Input.GetKey(KeyCode.D)) {
            slicePosition += Time.deltaTime * -0.3f;
            syncPlaneWithShader();
            slicerControl.reverseSync(slicePosition);
        }

        if (Input.GetKeyDown(KeyCode.S)) switchSlicerOrientation();
        if (menuMaster.currentTool != menuMaster.Tools.Identify) col.enabled = true;
    }
    
    IEnumerator dragMove() {
        isGrabbed = true;

        Vector3 planeStartPosition = transform.position;
        Vector3 cameraDistance;
        Vector3 mouseVector;
        Camera camera = Camera.main;        

        yield return new WaitForSeconds(0.2f);
        if (!Input.GetMouseButton(0)) {
            isGrabbed = false;
            yield break;
        }

        while (Input.GetMouseButton(0) && menuMaster.currentTool == menuMaster.Tools.Move) {
            //this isn't orthogonal to the plane's movement axis, but close enough
            cameraDistance = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(camera.transform.position, Vector3.up));
            mouseVector = Vector3.Scale(camera.ScreenToWorldPoint(cameraDistance), slicerConfig[activeSlice].planeMovement) + planeStartPosition;

            if (slicerConfig[activeSlice].planeMovement.x != 0) slicePosition = mouseVector.x;
            else if (slicerConfig[activeSlice].planeMovement.y != 0) slicePosition = mouseVector.y;
            else if (slicerConfig[activeSlice].planeMovement.z != 0) slicePosition = mouseVector.z;

            syncPlaneWithShader();
            slicerControl.reverseSync(slicePosition);

            yield return new WaitForEndOfFrame();
        }

        isGrabbed = false;
    }

    public void updateSlice(float newPosition){
        if (activeSlice == 0 && menuMaster.currentMode == menuMaster.Modes.DTI) {
            activeSlice = 2;
            switchSlicerOrientation();        
        }

        slicePosition = newPosition;
        syncPlaneWithShader();
    }

    //UTILITIES
    void switchSlicerOrientation(){
        activeSlice++;
        if (activeSlice >= slicerConfig.Length && menuMaster.currentMode == menuMaster.Modes.DTI) activeSlice = 0;
        else if (activeSlice >= slicerConfig.Length) activeSlice = 1;

        //apply settings from the config
        foreach (Material mat in mats) {
            mat.SetFloat("_flip", slicerConfig[activeSlice].defaultFlip ? 1f : 0f);
            mat.SetVector("_axis", slicerConfig[activeSlice].shaderAxis);
        }
        transform.localEulerAngles = slicerConfig[activeSlice].planeEulers;
        if (text != null) text.text = slicerConfig[activeSlice].name;     //show the name in the UI

        //manual override for horizontal section
        if (activeSlice == 3) slicePosition = -1.45f;   
        else if (activeSlice == 0) slicePosition = 2f;
        else slicePosition = 0f; 
        
        syncPlaneWithShader();
        isGrabbed = false;
    }

    void syncPlaneWithShader(){
        if (slicePosition > 2f) slicePosition = 2f;
        if (slicePosition < -2f) slicePosition = -2f;

        transform.position = slicerConfig[activeSlice].planeMovement * slicePosition;
        if (activeSlice == 1 || activeSlice == 2)
            transform.position += new Vector3(0f, 1.1f, 0.4f);
        else if (activeSlice == 4)
            transform.position += new Vector3(0f, 1.1f, 0f);

        foreach (Material mat in mats) {
            mat.SetFloat("_amount", slicePosition);
        }

        //turn off colliders on the invisible side of the plane

        if (activeSlice == 0) 
            bm.checkColliderVisibility(new Plane(transform.forward * -1f, transform.position));
        else
            bm.checkColliderVisibility(new Plane(transform.forward, transform.position));

    }
}
