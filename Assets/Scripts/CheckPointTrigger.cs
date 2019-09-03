using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            triggered = true;
        }
    }
}