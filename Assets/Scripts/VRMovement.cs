using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMovement : MonoBehaviour
{

    public GameObject Player;

    private Vector3 lookDir;
    private bool m_isMoving;
    public float speedMultiplier;

    public bool IsWalking { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        IsWalking = false;
    }


    // Update is called once per frame
    void Update()
    {

        //Set lookDir to the current direction the player / camera is looking at
        lookDir = transform.InverseTransformDirection(Camera.main.transform.forward);

        //Mimic the kinect bool input
        if (Input.GetKeyDown(KeyCode.K))
        {
            IsWalking = true;
        }

        if (IsWalking == true)
        {
            //Waiting for more walk input from kinect 
            StopAllCoroutines();
            StartCoroutine("WaitForInput");
            m_isMoving = true;
            IsWalking = false;
        }

        if (m_isMoving == true)
        {

            //Walk in the X/Z direction of the current direction the player / camera is looking at
            Player.transform.Translate(new Vector3(lookDir.x, 0, lookDir.z) * Time.deltaTime * speedMultiplier);

        }
    }

    IEnumerator WaitForInput()
    {
        //Wait x seconds then disable movement when coroutine is not restarted by additional movement
        yield return new WaitForSeconds(1.2f);
        m_isMoving = false;
    }

}