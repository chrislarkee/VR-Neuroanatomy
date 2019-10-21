using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oculusFFR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (OVRManager.fixedFoveatedRenderingSupported)
            OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.Medium;
    }
}
