using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newLaser : MonoBehaviour
{
    private Color laserOff = new Color32(200, 43, 43, 30); //transparent red
    private Color laserOn = new Color32(200, 20, 95, 220); //hot magenta
    private Material mat;

    private bool pointTriggerReady = false;
    private float pointTriggerAxis = 0f;
    private float gripTriggerAxis = 0f;

    private OVRInput.Controller controller;
    public static RaycastHit hit;
    private SphereCollider col;

    //sound
    private AudioSource sound;
    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Start() {
        mat = transform.GetChild(0).GetComponent<LineRenderer>().material;
        col = GetComponent<SphereCollider>();
        sound = GetComponent<AudioSource>();

        if (transform.name.StartsWith("L"))
            controller = OVRInput.Controller.LTouch;
        else
            controller = OVRInput.Controller.RTouch;
    }

    // Update is called once per frame
    void Update() {
        gripTriggerAxis = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
        pointTriggerAxis = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);

        if (gripTriggerAxis >= 0.9f)
            col.enabled = true;
        else
            col.enabled = false;

        if (pointTriggerAxis >= 0.1f && col.enabled == false) {
            Physics.Raycast(transform.GetChild(0).position, transform.TransformVector(Vector3.forward), out hit, 20f);
            mat.color = Color32.Lerp(laserOff, laserOn, pointTriggerAxis + 0.1f);

            if (hit.transform != null) {
                hit.transform.SendMessage("hoverOn", controller, SendMessageOptions.DontRequireReceiver); //
                if (!sound.isPlaying) {
                    sound.clip = sounds[0];
                    sound.Play();
                }

                if (pointTriggerAxis >= 0.95f) pointTriggerReady = true;
            }         
        } else {
            mat.color = laserOff;
            if (pointTriggerReady) doTrigger();
        }
    }

    void doTrigger() {
        pointTriggerReady = false;            
        if (hit.transform != null){
            Debug.Log("Clicked on " + hit.transform.name);
            sound.clip = sounds[1];
            sound.Play();
            hit.transform.SendMessage("clickOn", controller, SendMessageOptions.DontRequireReceiver); //
        }
    }
}
