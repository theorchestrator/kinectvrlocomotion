using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benchmark : MonoBehaviour
{

    public GameObject CameraRig;
    public List<GameObject> PendingStartCheckpoints = new List<GameObject>();
    private List<GameObject> PendingFinishCheckpoints = new List<GameObject>();
    private List<GameObject> FinishedCheckpoints = new List<GameObject>();
    private int counter = 0;

    private int timeStarted = 0;

    public Dictionary<GameObject,CheckpointData> checkpointsData = new Dictionary<GameObject, CheckpointData>();

    public struct CheckpointData
    {
        public float Angle;
        public float Distance;
        public int EnterTime;
        public int FinishTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        PendingStartCheckpoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoints"));
        timeStarted = System.Environment.TickCount;
    }

    // Update is called once per frame
    void Update()
    {
       
        for(int i = 0; i < PendingStartCheckpoints.Count; i++)
        {
            GameObject checkpoint = PendingStartCheckpoints[i];
           
            if (checkpoint.GetComponent< CheckPointTrigger>().triggered)
            {
                CheckpointData data = new CheckpointData();
                data.Angle = CalcAngle(checkpoint);
                data.EnterTime = GetRelativeTime();
                checkpointsData.Add(checkpoint,data);
                PendingFinishCheckpoints.Add(checkpoint);
                PendingStartCheckpoints.Remove(checkpoint);
                Debug.Log(checkpoint.name + " hat: " + data.Angle + "° : " + data.Distance + "u : " + data.EnterTime);
                break;
            }
        }

        if (this.gameObject.GetComponent<GetSpeed>().Speed == 0)
        {
            for (int i = 0; i < PendingFinishCheckpoints.Count; i++)
            {
                GameObject checkPoint = PendingFinishCheckpoints[i];
                CheckpointData data;
                checkpointsData.TryGetValue(checkPoint, out data);
                data.FinishTime = GetRelativeTime();
                data.Distance = CalcDistance(checkPoint);
                FinishedCheckpoints.Add(checkPoint);
                PendingFinishCheckpoints.Remove(checkPoint);
                Debug.Log(checkPoint.name + "hat: " + data.Angle + "° : " + data.Distance + "u : " + data.FinishTime);
            }
        }
    }

    public int GetRelativeTime() {
        return System.Environment.TickCount - timeStarted;
    }
    public float CalcAngle(GameObject checkpoint)
    {
        float angle = Vector3.Angle(CameraRig.transform.right, checkpoint.transform.right);
        return angle;
    }
    public float CalcDistance(GameObject checkpoint)
    {
        float distance = Vector3.Distance(new Vector3(CameraRig.transform.position.x, 0, CameraRig.transform.position.z), new Vector3(checkpoint.transform.position.x, 0, checkpoint.transform.position.z));
        return distance;
    }

}
