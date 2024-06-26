using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public TMP_Text GoliathHpText;
    public TMP_Text DavidHpText;
    private int GoliathHealth = 35;
    private int DavidHealth = 30;
    




    private void Start()
    {
        if(PlayerPrefs.HasKey("GHP") == false)
        {
            PlayerPrefs.SetInt("GHP", GoliathHealth);
        }
        if (PlayerPrefs.HasKey("DHP") == false)
        {
            PlayerPrefs.SetInt("DHP", DavidHealth);
        }
    }

    private void Update()
    {
        
        EndCards = CardsOnBoard.ToArray();
        DavidHpText.text = ("David: " + PlayerPrefs.GetInt("DHP").ToString());
        GoliathHpText.text = ("Goliath: " + PlayerPrefs.GetInt("GHP").ToString());



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
            EndRound();



            //if (BlueScore > RedScore)
            //{
            //    WinTextBlue.SetActive(true);
            //}
            //else if (RedScore > BlueScore)
            //{
            //    WinTextRed.SetActive(true);
            //}
            //else if (RedScore == BlueScore)
            //{
            //    WinTextTie.SetActive(true);
            //}
        }
    }
    public void TurnChangeToP2()
    {

        if(TurnCount != 8)
        {
            StartCoroutine(RotateToP2());
        }
    }
    public void TurnChangeToP1()
    {
        if (TurnCount != 8)
        {
            StartCoroutine(RotateToP1());
        }
    }

    IEnumerator RotateToP2()
    {
        
        yield return new WaitForSeconds(0.6f);
        TurnCount++;
        P2Cam.SetActive(true);
        P1Cam.SetActive(false);
        for (int i = 0; i < CardsOnBoard.Count; i++)
        {
            CardsOnBoard[i].GetComponent<CardIndex>().EndTurnRotateP2POV();
        }
        yield return new WaitForEndOfFrame();
        yield break;
    }
    IEnumerator RotateToP1()
    {
        
        yield return new WaitForSeconds(0.6f);
        TurnCount++;
        P1Cam.SetActive(true);
        P2Cam.SetActive(false);
        for (int i = 0; i < CardsOnBoard.Count; i++)
        {
            CardsOnBoard[i].GetComponent<CardIndex>().EndTurnRotateP1POV();
        } 
        yield return new WaitForEndOfFrame();
        yield break;
    }

    private void EndRound()
    {
        PlayerPrefs.SetInt("GATK", RedScore);
        PlayerPrefs.SetInt("DATK", BlueScore);
        
        if (PlayerPrefs.GetInt("GHP") > 0 && PlayerPrefs.GetInt("DHP") > 0)
        {
            
            SceneManager.LoadScene("RoundinBetweenScene");
        }
        else if(PlayerPrefs.GetInt("GHP") <= 0||PlayerPrefs.GetInt("DHP") <= 0)
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("MainMenu");
        }


    }

}
