using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuClickon : MonoBehaviour
{
    Button b;

    // Start is called before the first frame update
    void Start() {
        b = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void clickOn()    {
        b.Select();
        b.onClick.Invoke();
    }

    public void hoverOn(){
        b.Select();
    }
}
