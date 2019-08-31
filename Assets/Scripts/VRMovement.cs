using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script is used to interpret the gesture recognition into VR-Movement taking into account the speed of the gesture recognition
/// </summary>
public class VRMovement : MonoBehaviour
{
    public GameObject Player;

    private Vector3 lookDir;
    private Vector3 walkDir;

    private bool m_isMoving;
    private bool timerEnabled = false;

    private float timeSinceLastRecognition;
    private float speedMultiplier;
    private float speed;

    public float baseSpeed;
    public float waitPeriod;

    public bool IsWalking { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        speedMultiplier = 1.0f;

        if (waitPeriod == 0f)
        {
            waitPeriod = 1f;
        }

        IsWalking = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Set lookDir to the current direction the player / camera is looking at
        lookDir = transform.InverseTransformDirection(Camera.main.transform.forward);

        //The actual movement speed is a combined value of a base speed and a multiplier
        //The multiplier is the calculated duration between two recognised gestures
        speed = baseSpeed * speedMultiplier;

        //Add the strafe angle to the current look/walk direction
        //If the controller backwards motion is detected, the direction will be inverted for "negative" acceleration
        if (this.GetComponent<GetRotation>().Backwards == true)
        {
            walkDir = Quaternion.Euler(0, this.gameObject.GetComponent<GetRotation>().StrafeAngle, 0) * Quaternion.Euler(0, 180, 0) * new Vector3(lookDir.x, 0, lookDir.z);
        }
        else
        {
            walkDir = Quaternion.Euler(0, this.gameObject.GetComponent<GetRotation>().StrafeAngle, 0) * new Vector3(lookDir.x, 0, lookDir.z);
        }


        //Debug the Microsoft Kinect bool input
        if (Input.GetKeyDown(KeyCode.K))
        {
            IsWalking = true;
        }

        if (timerEnabled == true)
        {
            timeSinceLastRecognition += Time.deltaTime;
        }

        if (IsWalking == true)
        {
            timerEnabled = true;

            //Waiting for more walk input from kinect 
            StopAllCoroutines();
            StartCoroutine("WaitForInput");

            //Debug the speed variables
            //Debug.Log("Rectime: " + timeSinceLastRecognition + "Multiplier: " + speedMultiplier + "Walk Speed: " + speed);

            //Map the speed variables to the player velocity range
            if (timeSinceLastRecognition != 0)
            {
                #region Old Speed Code
                //if (timeSinceLastRecognition > 0.7f)
                //{
                //    speedMultiplier = 0.5f;
                //}

                //else if (timeSinceLastRecognition > 0.5f && timeSinceLastRecognition < 0.7f)
                //{
                //    speedMultiplier = 1f;
                //}

                //else if (timeSinceLastRecognition < 0.4f)
                //{
                //    speedMultiplier = 2f;
                //}
                #endregion
                Mathf.Clamp(speedMultiplier = MapValue(timeSinceLastRecognition, waitPeriod, 0.5f, 0.7f, 1.8f), 0.7f, 2f);
            }

            timeSinceLastRecognition = 0;
            m_isMoving = true;
            IsWalking = false;

        }

        //Walk in the X/Z direction of the current direction the player / camera is looking at
        if (m_isMoving == true)
        {
            this.gameObject.GetComponent<Tests>().StartTimerEnabled = false;
            //Player.transform.Translate(walkDir * (Time.deltaTime * speed));
            Player.GetComponent<CharacterController>().Move(walkDir * (Time.deltaTime * speed));
        }
    }

    //Wait x amount of seconds then disable movement when coroutine is not restarted by additional movement
    IEnumerator WaitForInput()
    {
        yield return new WaitForSeconds(waitPeriod);
        m_isMoving = false;
        timerEnabled = false;
        timeSinceLastRecognition = 0;
        this.gameObject.GetComponent<Tests>().StopTimerEnabled = false;
    }

    //This is the mapping function to map a value from one range to another
    public float MapValue(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}