using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class intro : MonoBehaviour
{
    private Transform camOrbit;
    //private Material background;
    private Color originalColor;
    private BrainMaster2 brain;
    private GameObject ui;

    public float rotationSpeed = 2f;
    private bool introActive = true;

    // Start is called before the first frame update
    void Start() {
        camOrbit = GameObject.Find("CameraOrbit").transform;

        //background = GameObject.Find("Background").GetComponent<MeshRenderer>().material;
        //originalColor = background.color;
        //background.color = new Color(0.1f, 0.1f, 0.1f);     //dark gray.

        ui = GameObject.Find("MenuBar");
        ui.SetActive(false);

        brain = GameObject.Find("blender-brain-opt").GetComponent<BrainMaster2>();
        StartCoroutine("introMode");
    }

    void Update(){
        if (Input.anyKeyDown) introActive = false;
    }

   IEnumerator introMode() {
        float timer = 100f;
        yield return new WaitForEndOfFrame();
        audioManager.play("Intro");
        
        while (introActive) {
            yield return new WaitForEndOfFrame();
            camOrbit.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);

            timer += Time.deltaTime;
            if (timer >= 10f){
                timer = 0f;
                brain.randomize();
            }
        }

        //exiting the intro. Put things back to normal!
        DOTween.KillAll();
        brain.putBackAll();
        //background.DOColor(originalColor, 3f); 
        
        yield return new WaitForSeconds(1f);
        ui.SetActive(true);

        //enabled = false;
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        introActive = false;
    }
}
