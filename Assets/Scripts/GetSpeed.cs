using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpeed : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject CameraRig;

    Vector3 PrevFramePos = Vector3.zero;

    
    public float Speed { get; set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Vector3.Distance(PrevFramePos, CameraRig.transform.position);
        Speed = movement / Time.deltaTime;
        PrevFramePos = CameraRig.transform.position;

        //Debug.Log(Speed);
    }
}
