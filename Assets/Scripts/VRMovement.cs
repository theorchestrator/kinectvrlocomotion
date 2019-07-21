using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMovement : MonoBehaviour
{


    public GameObject Player;
    private Vector3 lookDir;

    public bool m_isWalking;
    public bool coroutineStarted;

    public bool IsWalking
    {
        get { return m_isWalking; }
        set { m_isWalking = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        m_isWalking = false;

    }
    
    // Update is called once per frame
    void Update()
    {

        //Debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            m_isWalking = true;
        }
        
        lookDir = transform.InverseTransformDirection(Camera.main.transform.forward);

        if (m_isWalking == true)
        {

            //Beweg dich nach Vorne du Dildo
            Player.transform.Translate(new Vector3(lookDir.x, 0, lookDir.z) * Time.deltaTime);

            //Auf weiteren Input warten danach Bewegen wieder stoppen
            if(coroutineStarted == false)
            {
                coroutineStarted = true;
                StartCoroutine(WaitForInput());
            }
           
        }
  
    }

    IEnumerator WaitForInput()
    {
        yield return new WaitForSeconds(2);
        m_isWalking = false;
        coroutineStarted = false;
    }


}