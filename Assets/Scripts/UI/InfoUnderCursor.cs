using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUnderCursor : MonoBehaviour
{
    //components
    RectTransform rt;
    public static Text text;

    //data cache
    private Vector3 offset;
    private float timer = 0f;
    private float movement = 0;

    //loop cache
    private string oldText;
    private Vector3 oldPosition;    

    // Start is called before the first frame update
    void Start() {
        rt = GetComponent<RectTransform>();
        text = GetComponent<Text>();        
                      
        if (Application.isMobilePlatform) {
            text.alignment = TextAnchor.UpperCenter; 
            rt.anchoredPosition = new Vector3(0f, -50f, 0f);
            oldText = text.text;
            text.enabled = true;
        } else {
            oldPosition = Input.mousePosition;
            offset = new Vector3(Screen.width * 0.01f, Screen.height * 0.005f, 0f);
            text.enabled = false;
        }
    }

    public static void setText(string newText){
        try {
            InfoUnderCursor.text.text = newText;
        } catch { }
    }

    // Update is called once per frame
    void Update() {
        if (menuMaster.currentTool == menuMaster.Tools.Identify) {
            text.enabled = true;
            if (!Application.isMobilePlatform) rt.position = Input.mousePosition + offset;            
            return;
        }

        if (Application.isMobilePlatform) touchMode();
        else mouseMode();       
    }

    void mouseMode(){
        movement = Vector3.SqrMagnitude(Input.mousePosition - oldPosition);
        timer += Time.deltaTime;

        //threshold trigger
        if (movement > 60f) {
            timer = 0f;
        }

        if (timer > 1.1f) {
            rt.position = Input.mousePosition + offset;
            text.enabled = true;
        } else {
            text.enabled = false;
        }

        //cache for the next loop
        oldPosition = Input.mousePosition;
    }

    void touchMode(){
        if (text.text != oldText) {
            oldText = text.text;
            timer = 0f;
        }

        timer += Time.deltaTime;

        if (timer > 4f && text.enabled)
            text.enabled = false;
        else if (timer < 4f && !text.enabled)
            text.enabled = true;
    }
}
