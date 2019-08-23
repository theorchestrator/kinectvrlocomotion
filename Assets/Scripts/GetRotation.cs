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

    float diff;

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


        Vector3 forwardCam = Camera.main.transform.forward;
        Vector3 forwardController = leftController.transform.forward;

        Vector3 rCam = Camera.main.transform.right;
        Vector3 rController = leftController.transform.right;

        
        Vector3 planeNormalX = new Vector3(1, 0, 0); //X-Angle = YZ
        Vector3 planeNormalY = new Vector3(0, 1, 0); //Y-Angle = XZ
      //Vector3 planeNormalZ = new Vector3(0, 0, 1); //Z-Angle = XY

        Vector3 projectionX1 = Vector3.ProjectOnPlane(forwardCam, planeNormalX);
        Vector3 projectionX2 = Vector3.ProjectOnPlane(forwardController, planeNormalX);

        Vector3 projectionY1 = Vector3.ProjectOnPlane(rCam, planeNormalY);
        Vector3 projectionY2 = Vector3.ProjectOnPlane(rController, planeNormalY);


        float angleX = Vector3.Angle(projectionX1, projectionX2);
        float angleY = Vector3.SignedAngle(projectionY1, projectionY2, planeNormalY);

        //Debug.Log((int)angleX + " " + (int)angleY);

        //Debug.Log(rCam + " " + rController);
        //Debug.Log((int)angleX + " " + (int)angleY /*+ " " + (int)angleZ*/);


        //Walk Backwards
        if (angleX > 80)
        {
            Backwards = true;
            Debug.Log("Walking Backwards");
        }


        //Strafing
        //Strafe Left
        if (angleY < 35)
        {
            Debug.Log("Strafe Left");
            StrafeAngle = (Mathf.Clamp(this.gameObject.GetComponent<VRMovement>().MapValue(angleY, 35f, -25f, 0f, 90f), 0f, 90f) * -1);
        }

        //Strafe Right
        if (angleY > 40)
        {
            Debug.Log("Strafe Right");
            StrafeAngle = Mathf.Clamp(this.gameObject.GetComponent<VRMovement>().MapValue(angleY, 40f, 90f, 0f, 90f), 0f, 90f);
        }

        else if (angleX < 90)
        {
            Backwards = false;
        }

        //Debug.Log(StrafeAngle);
    }
}
