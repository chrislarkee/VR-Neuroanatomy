using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOrbit : MonoBehaviour
{
    //this is public so other scripts can easily block movement.
    public bool motionEnabled = true;

    //caches
    private Transform cam;          //the main camera
    private Transform camTarget;    //the camera's target position. This transform is the look target.
    private Vector3 mousePosOld;    //used for comparing mouse movement from the previous frame

    //Limits
    private float minimumDistance = -8f;
    private float maximumDistance = -0.3f;

    private float minimumHeight = 0.001f;
    private float maximumHeight = 4f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        camTarget = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update() {
        if (motionEnabled) {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                mousePosOld = Input.mousePosition;

            //drag the left mouse button to change orientation
            if (Input.GetMouseButton(0)) dragOrientation();
            if (Input.GetKey(KeyCode.LeftArrow)) keyboardOrientation(Vector2.left);
            else if (Input.GetKey(KeyCode.RightArrow)) keyboardOrientation(Vector2.right);
            if (Input.GetKey(KeyCode.UpArrow)) keyboardOrientation(Vector2.up);
            else if (Input.GetKey(KeyCode.DownArrow)) keyboardOrientation(Vector2.down);

            //drag the right mouse button to change altitude
            if (Input.GetMouseButton(1)) dragAltitude();

            //zoom can be a mouse wheel scroll, middle mouse drag, or keyboard
            if (Input.mouseScrollDelta.y != 0f) scrollZoom();
            else if (Input.GetMouseButton(2)) dragZoom();
            else if (Input.GetKey(KeyCode.Q)) keyboardZoom(1f);
            else if (Input.GetKey(KeyCode.E)) keyboardZoom(-1f);

            //keep the camera out of the ground
            if (camTarget.position.y < minimumHeight) {
                camTarget.position = new Vector3 (camTarget.position.x, minimumHeight, camTarget.position.z);
            }
        }

        //apply the changes smoothly, if applicable
        if (Vector3.Distance(cam.position, camTarget.position) >= 0.01f) {
            cam.position = Vector3.Lerp(cam.position, camTarget.position, Time.deltaTime * 5f);
            cam.transform.LookAt(transform.position);
        }
    }

    //triggered by the scroll wheel
    void scrollZoom() {
        float zoomDelta = Input.mouseScrollDelta.y * Time.deltaTime * 30f;
        float newValue = camTarget.localPosition.z + zoomDelta;

        if (newValue >= minimumDistance && newValue <= maximumDistance)
            camTarget.localPosition = new Vector3(0f, 0f, newValue);
    }

    public void keyboardZoom(float amount){
        float newValue = camTarget.localPosition.z + (amount * Time.deltaTime * 5f);
        if (newValue >= minimumDistance && newValue <= maximumDistance)
            camTarget.localPosition = new Vector3(0f, 0f, newValue);
    }

    //triggered by a vertical middle mouse drag
    void dragZoom() {
        float zoomAmount = (Input.mousePosition.y - mousePosOld.y) / Screen.height * Time.deltaTime * 50f;
        float newValue = camTarget.localPosition.z + zoomAmount;

        if (newValue >= minimumDistance && newValue <= maximumDistance)
            camTarget.localPosition = new Vector3(0f, 0f, newValue);
    }

    //this is called by the scenebuilder, to autoscroll the camera to the new skid height
    public void setHeight (float newHeight){
        if (newHeight >= minimumHeight && newHeight <= maximumHeight)
            transform.localPosition = new Vector3(0f, newHeight, 0f);
        else if (newHeight > maximumHeight)
            transform.localPosition = new Vector3(0f, maximumHeight, 0f);
    }

    void dragAltitude() {
        float flyAmount = (Input.mousePosition.y - mousePosOld.y) / Screen.height * Time.deltaTime * 10f;

        if (transform.localPosition.y + flyAmount >= minimumHeight && transform.localPosition.y + flyAmount <= maximumHeight)
            transform.localPosition = new Vector3(0f, transform.localPosition.y + flyAmount, 0f);
    }

    void dragOrientation(){
        float horizontalMovement = (Input.mousePosition.x - mousePosOld.x) / Screen.width * Time.deltaTime * -300f;
        float verticalMovement = (Input.mousePosition.y - mousePosOld.y) / Screen.height * Time.deltaTime * 100f;

        transform.Rotate(0f, horizontalMovement, 0f, Space.World);
        transform.Rotate(verticalMovement, 0f, 0f, Space.Self);
    }

    public void keyboardOrientation(Vector2 direction){
        direction = direction * Time.deltaTime * 50f;
        transform.Rotate(0f, direction.x * -2f, 0f, Space.World);
        transform.Rotate(direction.y, 0f, 0f, Space.Self);
    }
}
