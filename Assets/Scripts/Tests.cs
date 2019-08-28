using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Tests : MonoBehaviour
{

    public SteamVR_ActionSet m_ActionSet;
    public SteamVR_Action_Boolean m_BooleanAction;
    public SteamVR_Action_Vector2 m_TouchPosition;

    public bool TimerEnabled { get; set; }

    public float ButtonTimer { get; set; }
    

    void Awake()
    {
        m_BooleanAction = SteamVR_Actions.default_GrabPinch;
    }

    // Start is called before the first frame update
    void Start()
    {
        TimerEnabled = false;
        m_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
       

        //For VR-Controller Input
        if (m_BooleanAction[SteamVR_Input_Sources.LeftHand].stateDown)
        {
            this.gameObject.GetComponent<VRMovement>().IsWalking = true;
            TimerEnabled = true;
            ButtonTimer = 0f;

        }


        if (TimerEnabled == true)
        {
            ButtonTimer += Time.deltaTime;
            Debug.Log(ButtonTimer * 1000); //Returns the delay between input and walking (output) in Milliseconds (usually between 13-16.2ms)
        }

     
    }
}