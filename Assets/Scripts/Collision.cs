using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public CharacterController characterController;
    public GameObject Camera;

    Vector3 localCamPos;

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        localCamPos = (Camera.transform.position - characterController.gameObject.transform.position);
        characterController.center = localCamPos;  
    }
}
