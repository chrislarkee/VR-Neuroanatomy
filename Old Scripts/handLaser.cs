
//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;
//using UnityEngine.XR;
//using DG.Tweening;

//public class handLaser : MonoBehaviour {

//	public TextMesh gui;
//	public Renderer laser;

//    //sound
//    private AudioSource sound;
//    public AudioClip[] sounds;

//    //teleportation
//    private GameObject teleport;
//    private bool teleportArmed = false;
//    private GameObject baseNode;

//    //caches
//    private bool inRightHand = true;
//	private RaycastHit hit;
//	private float triggerValue;
//	private float activationTime = -1000f;
//    private bool actionArmed = false;    

//	// Use this for initialization
//	void Start () {
//        DOTween.Init();
//        sound = GetComponent<AudioSource>();
//        teleport = GameObject.Find("Teleport");
//        baseNode = GameObject.Find("OVRCameraRig");
//		laser.enabled = false;

//        //are we in the right hand?
//        if (transform.name.StartsWith("Left"))
//            inRightHand = false;           
//	}

//	void Update(){
//        if (inRightHand) {
//		    triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
//		    if (OVRInput.Get (OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) triggerValue = 1.0f;
//        } else {
//            triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
//            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)) triggerValue = 1.0f;
//        }

//        if (triggerValue > 0.9f) {
//			laser.enabled = true;
//			Physics.Raycast (transform.position, transform.TransformVector(Vector3.forward), out hit, 10f);
//            scanForTeleport();
//            scanForBrainpart();
//			if (!sound.isPlaying) {
//				sound.clip = sounds[0];
//				sound.Play();
//			}
//		} else {
//			laser.enabled = false;
//            if (actionArmed) triggerBrainpart();
//            if (teleportArmed) doTeleport();
//            if (sound.clip == sounds[0]) sound.Stop();
//		}

//		if (Time.time - activationTime > 7.5f)
//			gui.text = "";
//	}

//    void scanForTeleport(){
//        if (hit.transform == null)
//            return;

//        if (hit.transform.gameObject.tag == "Floor") {
//            teleport.transform.position = hit.point;
//            teleportArmed = true;
//            actionArmed = false;
//            teleport.SetActive(true);            
//        }
//    }

//    void doTeleport(){
//        teleportArmed = false;
//        actionArmed = false;
//        teleport.SetActive(false);

//        sound.clip = sounds[1];
//        sound.Play();
        
//        baseNode.transform.DOMove(teleport.transform.position, 0.5f, false).SetEase(Ease.OutQuart);        
//    }

//    void scanForBrainpart(){
//		if (hit.transform == null) {
//            actionArmed = false;
//            return;
//        }

//		//analyze raycast
//		if (hit.transform.gameObject.tag == "brainpart") {
//			gui.text = hit.transform.name;
//			activationTime = Time.time;
//            actionArmed = true;
//            teleportArmed = false;
//            teleport.SetActive(false);
//        }
//	}

//    void triggerBrainpart(){
//        brainPart2 bp = hit.transform.GetComponent<brainPart2>();
//        bp.flyAway();
//        actionArmed = false;
//    }

//}
