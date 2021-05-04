using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class onMouse : MonoBehaviour
{ 
        private List<brainPart2> parts = new List<brainPart2>();
        public static brainPart2 previousRemoval;
        public static GameObject activeSelection;

    public static bool TaskOnClick { get; internal set; }

    //gameobject.find 

    //insert other stuff i need here, 

    public void OnMouseDown()   
    {
        UnityEngine.Debug.Log("OnMouseDown");
        transform.Rotate(Vector3.up * Time.deltaTime * 200f);
    }

    public void onMouseEnter()
    {
        UnityEngine.Debug.Log("OnMouseEnter");
    }

    public void onMouseDrag()
    {
        UnityEngine.Debug.Log("OnMouseDrag");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}