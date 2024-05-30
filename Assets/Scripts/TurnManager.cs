using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] GameObject P2Cam;
    [SerializeField] GameObject P1Cam;
    [SerializeField]private GameObject[] CardsToRotateOnTurnEnd;
    private Vector3 rot;

    private void Update()
    {
        CardsToRotateOnTurnEnd = GameObject.FindGameObjectsWithTag("Card");
    }
    public void TurnChangeToP2()
    {
        for (int i = 0; i < CardsToRotateOnTurnEnd.Length; i++)
        {
            CardsToRotateOnTurnEnd[i].GetComponent<CardIndex>().EndTurnRotate();
        }
        P2Cam.SetActive(true);
        P1Cam.SetActive(false);
        
    }
    public void TurnChangeToP1()
    {
        for (int i = 0; i < CardsToRotateOnTurnEnd.Length; i++)
        {
            CardsToRotateOnTurnEnd[i].GetComponent<CardIndex>().EndTurnRotate();
        }
        P1Cam.SetActive(true);
        P2Cam.SetActive(false);
    }

}
