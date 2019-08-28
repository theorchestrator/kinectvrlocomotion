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

    Vector3 planeVector;

    Vector3 forwardVecCam;
    Vector3 forwardVecLeftController;

    Vector3 rightVecCam;
    Vector3 rightVecLeftController;

    Vector3 planeNormalX; //X-Angle = YZ
    Vector3 planeNormalY = new Vector3(0, 1, 0); //Y-Angle = XZ
  //Vector3 planeNormalZ = new Vector3(0, 0, 1); //Z-Angle = XY

    Vector3 projectionX1;
    Vector3 projectionX2;

    Vector3 projectionY1;
    Vector3 projectionY2;

    float angleX;
    float angleY;

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
        
        //Set the projection plane to the perpendicular vector of the difference between controller and headset
        planeVector = (Camera.main.transform.position - leftController.transform.position);
        planeNormalX = Vector3.Cross(planeVector, Vector3.up).normalized;
   

        //Set the normal vectors of camera and controlelr
        forwardVecCam = Camera.main.transform.forward;
        forwardVecLeftController = leftController.transform.forward;

        rightVecCam = Camera.main.transform.right;
        rightVecLeftController = leftController.transform.right;


        //Calculate projections
        projectionX1 = Vector3.ProjectOnPlane(forwardVecCam, planeNormalX);
        projectionX2 = Vector3.ProjectOnPlane(forwardVecLeftController, planeNormalX);

        projectionY1 = Vector3.ProjectOnPlane(rightVecCam, planeNormalY);
        projectionY2 = Vector3.ProjectOnPlane(rightVecLeftController, planeNormalY);

        //Calculate final euler angle
        angleX = Vector3.Angle(projectionX1, projectionX2);
        angleY = Vector3.SignedAngle(projectionY1, projectionY2, planeNormalY);

        //Debug.Log((int)angleX + " " + (int)angleY);
        //Debug.Log(rCam + " " + rController);
        //Debug.Log((int)angleX + " " + (int)angleY /*+ " " + (int)angleZ*/);

        //Walk Backwards
        if (angleX >= 80 )
        {
            Backwards = true;
            //Debug.Log("Walking Backwards");
        }

        //Strafing
        //Strafe Left
        if (angleY < -10 && angleY > -90)
        {
            //Debug.Log("Strafe Left");
            StrafeAngle = (Mathf.Clamp(this.gameObject.GetComponent<VRMovement>().MapValue(angleY, -10f, -90f, 0f, 90f), 0f, 90f) * -1);
        }

        //Strafe Right
        if (angleY > 10 && angleY < 90)
        {
            //Debug.Log("Strafe Right");
            StrafeAngle = Mathf.Clamp(this.gameObject.GetComponent<VRMovement>().MapValue(angleY, 10f, 90f, 0f, 90f), 0f, 90f);
        }

        else if (angleX < 80)
        {
            Backwards = false;
        }

        //Debug.Log(StrafeAngle);
    }
}
