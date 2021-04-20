using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cameraOrbit : MonoBehaviour
{
    public bool motionEnabled = true;   //quickly lock movements

    //caches
    private Transform mainCamera;       //the main camera
    private Transform camTarget;        //the camera's target position. This transform is the look target.
    private Vector3 mousePosOld;        //used for comparing mouse movement from the previous frame
    private bool touchStarted = false;  //hold for the multitouch handler

    //limits
    private float minimumDistance = -8f;
    private float maximumDistance = -0.3f;
    //private float minimumHeight = 0.001f;
    //private float maximumHeight = 4f;

    //restore
    Quaternion originalRotation;
    float originalDistance;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;       

        mainCamera = Camera.main.transform;
        camTarget = transform.GetChild(0);

        originalRotation = transform.rotation;
        originalDistance = camTarget.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (motionEnabled)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                mousePosOld = Input.mousePosition;

            //spin controls. right mouse first:            
            if (Input.GetMouseButton(1)) rotateMouse();
            else if (Input.GetMouseButton(0) && menuMaster.currentTool == menuMaster.Tools.Rotate) rotateMouse();
            else {
                //keyboard orientation controls.
                if (Input.GetKey(KeyCode.LeftArrow)) rotateKeyboard(Vector2.left);
                else if (Input.GetKey(KeyCode.RightArrow)) rotateKeyboard(Vector2.right);
                if (Input.GetKey(KeyCode.UpArrow)) rotateKeyboard(Vector2.up);
                else if (Input.GetKey(KeyCode.DownArrow)) rotateKeyboard(Vector2.down);
            }

            //zoom can be a mouse wheel scroll, middle mouse drag, or keyboard
            if (Input.mouseScrollDelta.y != 0f) scrollZoom();
            else if (Input.GetMouseButton(2)) dragZoom();
            else if (Input.GetMouseButton(0) && menuMaster.currentTool == menuMaster.Tools.Zoom) dragZoom();
            else if (Input.GetKey(KeyCode.Q)) keyboardZoom(1f);
            else if (Input.GetKey(KeyCode.E)) keyboardZoom(-1f);

            //multitouch controls for tablets & phones
            if (Input.touchCount >= 2 && !touchStarted) StartCoroutine("multitouchControls");
        }

        //if the camTarget has moved, move the camera to follow it
        if (Vector3.Distance(mainCamera.position, camTarget.position) >= 0.01f)
        {
            mainCamera.position = Vector3.Lerp(mainCamera.position, camTarget.position, Time.deltaTime * 8f);
            mainCamera.transform.LookAt(transform.position);
        }
    }

    public void restoreRotation(){
        transform.rotation = originalRotation;
    }
    public void restoreZoom(){
        camTarget.localPosition = new Vector3(0f, 0f, originalDistance);
    }

    void scrollZoom() 
    {
        float zoomDelta = Input.mouseScrollDelta.y * Time.deltaTime * 7.5f;
        if (zoomDelta > 0.2f) zoomDelta = 0.2f;
        if (zoomDelta < -0.2f) zoomDelta = -0.2f;

        float newValue = camTarget.localPosition.z + zoomDelta;

        if (newValue >= minimumDistance && newValue <= maximumDistance)
            camTarget.localPosition = new Vector3(0f, 0f, newValue);
    }

    public void keyboardZoom(float amount)
    {
        float newValue = camTarget.localPosition.z + (amount * Time.deltaTime * 2f);
        if (newValue >= minimumDistance && newValue <= maximumDistance)
            camTarget.localPosition = new Vector3(0f, 0f, newValue);
    }

    void dragZoom() {
        float zoomAmount = (Input.mousePosition.y - mousePosOld.y) / (float)Screen.height * Time.deltaTime * 7.5f;
        float newValue = camTarget.localPosition.z + zoomAmount;

        if (newValue >= minimumDistance && newValue <= maximumDistance)
            camTarget.localPosition = new Vector3(0f, 0f, newValue);
    }

    ////this is called by the scenebuilder, to autoscroll the camera to the new skid height
    //public void setHeight(float newHeight)
    //{
    //    if (newHeight >= minimumHeight && newHeight <= maximumHeight)
    //        transform.localPosition = new Vector3(0f, newHeight, 0f);
    //    else if (newHeight > maximumHeight)
    //        transform.localPosition = new Vector3(0f, maximumHeight, 0f);
    //}

    //void dragAltitude()
    //{
    //    float flyAmount = (Input.mousePosition.y - mousePosOld.y) / Screen.height * Time.deltaTime * 10f;

    //    if (transform.localPosition.y + flyAmount >= minimumHeight && transform.localPosition.y + flyAmount <= maximumHeight)
    //        transform.localPosition = new Vector3(0f, transform.localPosition.y + flyAmount, 0f);
    //}

    void rotateMouse() {
        float horizontalMovement = (Input.mousePosition.x - mousePosOld.x) / Screen.width * Time.deltaTime * 200f;
        float verticalMovement = (Input.mousePosition.y - mousePosOld.y) / Screen.height * Time.deltaTime * -120f;

        transform.Rotate(0f, horizontalMovement, 0f, Space.World);
        transform.Rotate(verticalMovement, 0f, 0f, Space.Self);
    }

    public void rotateKeyboard(Vector2 direction) {
        direction = direction * Time.deltaTime * 30f;
        transform.Rotate(0f, direction.x * -2f, 0f, Space.World);
        transform.Rotate(direction.y, 0f, 0f, Space.Self);
    }

    private IEnumerator multitouchControls()
    {
        touchStarted = true;    //prevents multiple coroutines from being triggered

        //set up default values and structs
        Vector2 direction;
        Vector3 distances = Vector3.zero;   //it's really an array, not a vector3
        distances[0] = 1.0f;                //index 0 = resulting delta;
        distances[1] = Vector2.Distance(Input.touches[0].position, Input.touches[1].position) / Screen.dpi; //index 1 = current distance between two touches
        yield return new WaitForEndOfFrame();

        //loop until the touch stops
        while (Input.touchCount >= 2)
        {
            //update distances
            distances[2] = distances[1];
            distances[1] = Vector2.Distance(Input.touches[0].position, Input.touches[1].position) / Screen.dpi;
            distances[0] = (distances[1] - distances[2]) * 8f;    //adjusted delta from the previous frame

            //apply distances by recycling the keyboard functions
            keyboardZoom(distances[0]);

            //update directions
            direction = Input.touches[0].deltaPosition / Screen.dpi * 0.3f;
            rotateKeyboard(direction);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
        touchStarted = false;   //tells update() we're done
    }
}
    