using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class TurnManager : MonoBehaviour
{
    [SerializeField] GameObject P2Cam;
    [SerializeField] GameObject P1Cam;
    [SerializeField] GameObject P2Cam2;
    [SerializeField] GameObject P1Cam2;
    [SerializeField] GameObject P1WinCam;
    [SerializeField] GameObject P2WinCam;
    [SerializeField]private GameObject[] CardsToRotateOnTurnEnd;
    [SerializeField]private Animator AnimatorDavid;
    [SerializeField]private Animator AnimatorGoliath;
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
    public bool BlueTurn = true;
    public bool RedTurn = false;

    




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
        if (PlayerPrefs.GetInt("GHP") <= 0)
        {
            StartCoroutine(DavidWin());
        }
        else if (PlayerPrefs.GetInt("DHP") <= 0)
        {
            StartCoroutine(GoliathWin());
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
            RedTurn = true;
            BlueTurn = false;
            StartCoroutine(RotateToP2());
        }
    }
    public void TurnChangeToP1()
    {
        if (TurnCount != 8)
        {
            RedTurn = false;
            BlueTurn = true;
            StartCoroutine(RotateToP1());
        }
    }

    IEnumerator RotateToP2()
    {
        
        yield return new WaitForSeconds(0.2f);
        TurnCount++;
        P1Cam2.SetActive(true);
        P1Cam.SetActive(false);
        yield return new WaitForSeconds(.5f);
        P2Cam2.SetActive(true);
        P1Cam2.SetActive(false);
        for (int i = 0; i < CardsOnBoard.Count; i++)
        {
            CardsOnBoard[i].GetComponent<CardIndex>().EndTurnRotateP2POV();
        }
        yield return new WaitForSeconds(.5f);
        P2Cam.SetActive(true);
        P2Cam2.SetActive(false);
        yield return new WaitForEndOfFrame();
        yield break;
    }
    IEnumerator RotateToP1()
    {

        yield return new WaitForSeconds(0.2f);
        TurnCount++;
        P2Cam2.SetActive(true);
        P2Cam.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        P1Cam2.SetActive(true);
        P2Cam2.SetActive(false);
        for (int i = 0; i < CardsOnBoard.Count; i++)
        {
            CardsOnBoard[i].GetComponent<CardIndex>().EndTurnRotateP1POV();
        }
        yield return new WaitForSeconds(0.5f);
        P1Cam.SetActive(true);
        P1Cam2.SetActive(false);
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
        


    }
    private IEnumerator DavidWin()
    {
        yield return new WaitForSeconds(0.6f);
        Debug.Log("DavidWin: Activating P1Cam2, Deactivating P2Cam2, P2Cam, P1Cam");
        P1Cam2.SetActive(true);
        P2Cam2.SetActive(false);
        P2Cam.SetActive(false);
        P1Cam.SetActive(false);

        AnimatorGoliath.SetBool("loss", true);
        yield return new WaitForSeconds(3f);

        Debug.Log("DavidWin: Activating P2Cam2, Deactivating P1Cam2");
        P1WinCam.SetActive(true);
        P1Cam2.SetActive(false);

        yield return new WaitForSeconds(3f);

        AnimatorDavid.SetBool("Win", true);
        yield return new WaitForSeconds(3f);

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator GoliathWin()
    {
        yield return new WaitForSeconds(0.6f);
        Debug.Log("GoliathWin: Activating P2Cam2, Deactivating P1Cam2, P2Cam, P1Cam");
        P2Cam2.SetActive(true);
        P1Cam2.SetActive(false);
        P2Cam.SetActive(false);
        P1Cam.SetActive(false);

        AnimatorDavid.SetBool("Loss", true);
        yield return new WaitForSeconds(3f);

        Debug.Log("GoliathWin: Activating P1Cam2, Deactivating P2Cam2");
        P2WinCam.SetActive(true);
        P2Cam2.SetActive(false);

        AnimatorGoliath.SetBool("win", true);
        yield return new WaitForSeconds(6f);

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

}
