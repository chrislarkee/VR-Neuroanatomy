//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class menuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    //using shared references reduces the ram burden a bit
    public static menuMaster master;
    public static ColorBlock originalColors;

    public menuMaster.Tools thisTool = menuMaster.Tools.None;
    public menuMaster.Modes thisMode = menuMaster.Modes.None;
    private Button b;
    
    void OnEnable() {
        b = GetComponent<Button>();

        if (master == null) {
            master = GameObject.Find("MenuBar").GetComponent<menuMaster>();
            originalColors = b.colors;
        }        

        master.registerButton(this);       
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //hover over the button, display its name in the info area. Modify the name in the heirarchy to tweak.
        InfoUnderCursor.setText(gameObject.name);
    }

    public void OnPointerClick(PointerEventData eventData) {
        //Debug.Log("Clicked: " + gameObject.name);

        //logic is this way so that the modes are not refreshed by the reset/undo buttons.
        if (thisTool != menuMaster.Tools.None && thisMode == menuMaster.Modes.None)
            menuMaster.currentTool = this.thisTool;
        else if (thisMode != menuMaster.Modes.None && thisTool == menuMaster.Tools.None)
            menuMaster.currentMode = this.thisMode;

        if (thisMode != menuMaster.Modes.None)
            master.switchModes();

        if (thisTool != menuMaster.Tools.None || thisMode != menuMaster.Modes.None)
            master.verifyStates();

        //audioManager.play("Next");
    }

    public void controlHighlight(bool active){
        //active = true will make the coloration stick on. Otherwise, white.
        //this is triggered by menuMaster.verifyStates.
        if (active) {
            //live blocks can't be modified directly, so we have to make a copy and modify that.
            ColorBlock newColors = originalColors;
            newColors.normalColor = originalColors.selectedColor;
            b.colors = newColors;
        } else
            b.colors = originalColors;
    }

}
