using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class JoystickMovement : MonoBehaviour
{

    public float m_SpeedMultiplier = 1.5f;
    public float m_Speed = 1f;

    public GameObject Player;

    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private Vector3 lookDir;
    private Vector3 walkDir;

    private float direction;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Calculate current forward direction
        lookDir = Camera.main.transform.forward;
        direction = Mathf.Atan2(m_MoveValue.axis.x, m_MoveValue.axis.y) * Mathf.Rad2Deg;

        //Add joystick angle as Y-rotation to the actual walk Direction
        walkDir = Quaternion.Euler(0, direction, 0) * new Vector3(lookDir.x, 0, lookDir.z);


        //If the Thumbstick is pressed multiply the speed by a given multiplier 
        if (m_MovePress.GetStateDown(SteamVR_Input_Sources.Any))
        {
            m_Speed *= m_SpeedMultiplier;
        }
        else if (m_MovePress.GetStateUp(SteamVR_Input_Sources.Any))
        {
            m_Speed /= m_SpeedMultiplier;
        }

        //Move in the actual walk direction with a certain speed
        if (m_MoveValue.axis.y > 0.01 || m_MoveValue.axis.y < -0.01)
        {
            //Debug.Log(walkDir);
            Player.transform.Translate(walkDir * (Time.deltaTime * m_Speed));
        }
    }
}
