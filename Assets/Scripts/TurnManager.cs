using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] GameObject P2Cam;
    [SerializeField] GameObject P1Cam;    
    public void TurnChangeToP2()
    {
        P2Cam.SetActive(true);
        P1Cam.SetActive(false);
    }
    public void TurnChangeToP1()
    {
        P1Cam.SetActive(true);
        P2Cam.SetActive(false);
    }

}
