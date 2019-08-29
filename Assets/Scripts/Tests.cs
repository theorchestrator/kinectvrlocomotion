using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Tests : MonoBehaviour
{

    public SteamVR_ActionSet m_ActionSet;
    public SteamVR_Action_Boolean m_BooleanAction;
    public SteamVR_Action_Vector2 m_TouchPosition;

    public bool StartTimerEnabled { get; set; }

    bool startTimerWasEnabled;
    bool stopTimerWasEnabled;
    public bool StopTimerEnabled { get; set; }

    public float ButtonTimer { get; set; }

    public float StopTimer { get; set; }

    void Awake()
    {
        m_BooleanAction = SteamVR_Actions.default_GrabPinch;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartTimerEnabled = false;
        m_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
       
        //For VR-Controller Input
        if (m_BooleanAction[SteamVR_Input_Sources.LeftHand].stateDown)
        {
            this.gameObject.GetComponent<VRMovement>().IsWalking = true;
            StartTimerEnabled = true;
            ButtonTimer = 0f;
        }

        if (StartTimerEnabled == true)
        {
            startTimerWasEnabled = true;
            ButtonTimer += Time.deltaTime;
        }

        if (StopTimerEnabled == true)
        {
            stopTimerWasEnabled = true;
            StopTimer += Time.deltaTime;   
        }

        if(stopTimerWasEnabled && StopTimerEnabled == false)
        {
            Debug.Log("Stopping latency: " + StopTimer * 1000); //Returns the delay between stopping and end of walking in Milliseconds (usually between xx-xx ms)
            stopTimerWasEnabled = false;
        }

        if (startTimerWasEnabled && StartTimerEnabled == false)
        {
            Debug.Log("Input latency: " + ButtonTimer * 1000); //Returns the delay between input and walking (output) in Milliseconds (usually between 13-17.2ms)
            startTimerWasEnabled = false;
        }


    }
}