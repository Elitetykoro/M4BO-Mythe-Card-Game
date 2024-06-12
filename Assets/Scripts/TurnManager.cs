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
    public int DavidHealthPoints = 30;
    public int GoliathHealthPoints = 45;
    [SerializeField] GameObject gameManagerP1;
    [SerializeField] GameObject gameManagerP2;
    GameObject[] Grids;


    private void Start()
    {
        Grids = GameObject.FindGameObjectsWithTag("Grid");
    }
    private void Update()
    {
        
        EndCards = CardsOnBoard.ToArray();



        if (TurnCount == 8)
        {
            RoundEnd();
        }
        if (DavidHealthPoints <= 0) WinTextBlue.SetActive(true);
        else if (GoliathHealthPoints <= 0) WinTextRed.SetActive(true);
    }
    public void TurnChangeToP2()
    {
        TurnCount++;
        if (TurnCount < 8)
        {
            StartCoroutine(RotateToP2());
            P2Cam.SetActive(true);
            P1Cam.SetActive(false);
        }
    }
    public void TurnChangeToP1()
    {     
        TurnCount++;
        if (TurnCount < 8)
        {
            StartCoroutine(RotateToP1());
            P1Cam.SetActive(true);
            P2Cam.SetActive(false);
        }
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
    public void RoundEnd()
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
        DavidHealthPoints -= RedScore;
        GoliathHealthPoints -= BlueScore;
        gameManagerP1.GetComponent<GameManagerP1>().DeckResetP1();
        gameManagerP2.GetComponent<GameManagerP2>().DeckResetP2();
        for (int i = 0; i < Grids.Length; i++)
        {
            Grids[i].GetComponent<GridIndex>().ClearGrid();
        }
        CardsOnBoard.Clear();
        TurnCount = 0;
    }



}
