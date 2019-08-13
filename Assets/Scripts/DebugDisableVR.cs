using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DebugDisableVR : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        XRSettings.enabled = false;
    }
}
