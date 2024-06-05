using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    [SerializeField] GameObject P2Cam;
    [SerializeField] GameObject P1Cam;
    [SerializeField]private GameObject[] CardsToRotateOnTurnEnd;
    private Vector3 rot;
    [SerializeField]private int TurnCount;
    private int BlueScore;
    private int RedScore;
    public List<GameObject> CardsOnBoard;
    private GameObject[] EndCards;
    public GameObject WinTextBlue;
    public GameObject WinTextRed;
    public GameObject WinTextTie;
    

 

    private void Update()
    {
        
        EndCards = CardsOnBoard.ToArray();
        


        if (TurnCount == 8)
        {
            EndCards = CardsOnBoard.ToArray();
            for (int i = 0; i < EndCards.Length; i++)
            {
                if (EndCards[i].GetComponent<CardIndex>().IsYoursBlue)
                {
                    BlueScore++;
                }
                if (EndCards[i].GetComponent<CardIndex>().IsYoursRed)
                {
                    RedScore++;
                }
            }

            if(BlueScore > RedScore)
            {
                WinTextBlue.SetActive(true);
            }else if(RedScore > BlueScore)
            {
                WinTextRed.SetActive(true);
            }else if(RedScore == BlueScore)
            {
                WinTextTie.SetActive(true);
            }
        }
    }
    public void TurnChangeToP2()
    {
        TurnCount++;
        StartCoroutine(RotateToP2());
        P2Cam.SetActive(true);
        P1Cam.SetActive(false);
    }
    public void TurnChangeToP1()
    {     
        TurnCount++;
        StartCoroutine(RotateToP1());
        P1Cam.SetActive(true);
        P2Cam.SetActive(false);
    }

    IEnumerator RotateToP2()
    {
        for(int i = 0; i <CardsOnBoard.Count; i++)
        {
            CardsOnBoard[i].GetComponent<CardIndex>().EndTurnRotateP2POV();
        }
        yield return new WaitForEndOfFrame();
        yield break;
    }
    IEnumerator RotateToP1()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < CardsOnBoard.Count; i++)
        {
            CardsOnBoard[i].GetComponent<CardIndex>().EndTurnRotateP1POV();
        }
        yield return new WaitForEndOfFrame();
        yield break;
    }



}
