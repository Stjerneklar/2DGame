using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorutinesTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitAndExecute");

        Invoke("StopExecution", 3f);
    }

    void StopExecution()
    {
        StopCoroutine("WaitAndExecute");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(0.5f);
        print("Printed After Wait Time");

        StartCoroutine("WaitAndExecute");
    }
}
