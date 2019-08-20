using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// This script is used to read the rotation of the controller to strafe (walk sideways) or walk backwards for kinect based locomotion
/// </summary>
public class GetRotation : MonoBehaviour
{

    public GameObject leftController;
    public GameObject rightController;

    public float backwardsValue;
    public float strafeValue;

    public bool Backwards { get; set; }

    public float StrafeAngle { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Backwards = false;

        if (backwardsValue == 0)
        {
            backwardsValue = 0.6f;
        }

        if (strafeValue == 0)
        {
            backwardsValue = 0.3f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Walk Backwards
        if (leftController.transform.localRotation.z > backwardsValue)
        {
            Backwards = true;
            Debug.Log("Walking Backwards");
        }

        //Strafe Left
        if (leftController.transform.localRotation.x > strafeValue)
        {
            Debug.Log("Walking Left");
            StrafeAngle = (Mathf.Clamp(this.gameObject.GetComponent<VRMovement>().MapValue(leftController.transform.localRotation.x, 0.3f, 0.6f, 0f, 90f), 0f, 90f) *-1 );
        }

        //Strafe Right
        if (leftController.transform.localRotation.x < (strafeValue *-1))
        {
            Debug.Log("Walking Right");
            StrafeAngle = Mathf.Clamp(this.gameObject.GetComponent<VRMovement>().MapValue(leftController.transform.localRotation.x, -0.3f, -0.6f, 0f, 90f), 0f, 90f);
        }

        else if(leftController.transform.localRotation.z < backwardsValue)
        {
            Backwards = false;
        }


        //Debug.Log("Left Controller: " +( leftController.transform.localRotation) + " Right Controller: " + rightController.transform.rotation);
        //Debug.Log(strafeAngle + "" + Backwards);

    }
}
