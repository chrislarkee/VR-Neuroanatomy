using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlicerControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public slicingPlane slicer;
    private Slider slider;
    private menuMaster.Tools previousTool;

    // Start is called before the first frame update
    void Start() {
        slider = GetComponent<Slider>();
    }
    public void OnEnable() {
        slicer.gameObject.SetActive(true);
    }

    public void OnDisable() {
        try {
            slicer.gameObject.SetActive(false);
        } catch {
        }
    }

    public void reverseSync(float newValue){
        slider.value = newValue;
    }

    public void OnMouseEnter() {
        InfoUnderCursor.setText("Slicing plane position (A/D)");
    }

    public void OnDrag(PointerEventData eventData) {
        //forward sync
        slicer.updateSlice(slider.value);
    }

    //When the user is moving the slider, don't rotate/zoom/etc.
    public void OnBeginDrag(PointerEventData eventData) {
        previousTool = menuMaster.currentTool;
        menuMaster.currentTool = menuMaster.Tools.None;
    }

    public void OnEndDrag(PointerEventData eventData) {
        menuMaster.currentTool = previousTool;
    } 
}
