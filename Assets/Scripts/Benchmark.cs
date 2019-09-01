using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benchmark : MonoBehaviour
{

    public GameObject CameraRig;
    public GameObject[] Checkpoints;
    private int counter = 0;

    public List<CheckpointData> checkpointsData = new List<CheckpointData>();

    public struct CheckpointData
    {
        public float Angle;
        public float Distance;
        public float Time;
    }

    // Start is called before the first frame update
    void Start()
    {
        Checkpoints = GameObject.FindGameObjectsWithTag("Checkpoints");
    }

    // Update is called once per frame
    void Update()
    {
       
        foreach (GameObject checkpoint in Checkpoints){

            if (Vector3.Distance(CameraRig.transform.position, checkpoint.transform.position) < 1)
            {
                counter++;

                if (this.gameObject.GetComponent<GetSpeed>().Speed == 0)
                {
                    CheckpointData data = new CheckpointData();

                    data.Angle = CalcAngle(checkpoint);
                    data.Distance = CalcDistance(checkpoint);
                    data.Time = 0f; //TEST
                    checkpointsData.Add(data);

                    Debug.Log(checkpoint.name + "hat: " + data.Angle + "° : " + data.Distance + "u : " + data.Time);



                }
            }
        }  
    }

    public float CalcAngle(GameObject checkpoint)
    {
        float angle = Vector3.Angle(CameraRig.transform.right, checkpoint.transform.right);
        return angle;
    }
    public float CalcDistance(GameObject checkpoint)
    {
        float distance = Vector3.Distance(CameraRig.transform.position, checkpoint.transform.position);
        return distance;
    }

}
