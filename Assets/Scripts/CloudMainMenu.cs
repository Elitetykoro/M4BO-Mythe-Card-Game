using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMainMenu : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(-Random.Range(1,10),0,0) * Time.deltaTime;
        if (transform.position.x < -115) 
        {
            transform.position = new Vector3(115,transform.position.y,90);
        }
    }
}
