using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class floorTeleport : MonoBehaviour
{
    //teleportation
    private GameObject teleport;
    private GameObject baseNode;
    private GameObject headNode;

    //private Material mat;    

    // Start is called before the first frame update
    void Start() {
        teleport = transform.Find("Teleport").gameObject;
        baseNode = GameObject.Find("OVRCameraRig");
        headNode = GameObject.Find("CenterEyeAnchor");
        DOTween.Init();
    }

    public void hoverOn(OVRInput.Controller c)
    {
        if (Vector3.Distance(headNode.transform.position, teleport.transform.position) > 10f)
            return;

        teleport.transform.position = newLaser.hit.point;
        teleport.SetActive(true);
    }       

    public void clickOn(OVRInput.Controller c) {
        if (!teleport.activeSelf) return;

        teleport.SetActive(false);

        //do the teleport        
        //Vector3 offset = new Vector3(teleport.transform.position.x - headNode.transform.position.x, teleport.transform.position.y, teleport.transform.position.z - headNode.transform.position.z);
        Vector3 offset = new Vector3(teleport.transform.position.x, 0f, teleport.transform.position.z);
        baseNode.transform.DOMove(offset, 0.5f, false).SetEase(Ease.OutQuart);
    }
}
