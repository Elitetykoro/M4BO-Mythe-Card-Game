using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerP2 : MonoBehaviour
{
    private List<GameObject> DeckP2 = new List<GameObject>();
    public Transform[] HandSlotsP2;
    public bool[] AvailableHandSlotsP2;
    public List<GameObject> resetDeck = new List<GameObject>();


    private void Start()
    {
        DeckResetP2();
        StartCoroutine(StartDraw());
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public void DrawCard()
    {
        if (DeckP2.Count >= 1)
        {
            GameObject RandomCard = DeckP2[Random.Range(0, DeckP2.Count)];

            for (int i = 0; i < AvailableHandSlotsP2.Length; i++)
            {
                if (AvailableHandSlotsP2[i] == true)
                {
                    RandomCard.SetActive(true);
                    RandomCard.GetComponent<CardIndex>().HandIndex = i;
                    RandomCard.transform.position = HandSlotsP2[i].transform.position;
                    AvailableHandSlotsP2[i] = false;
                    DeckP2.Remove(RandomCard);
                    return;
                }
            }
        }
    }

    private IEnumerator StartDraw()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            DrawCard();
            yield return new WaitForSeconds(0.5f);
            DrawCard();
            yield return new WaitForSeconds(0.5f);
            DrawCard();
            yield return new WaitForSeconds(0.5f);
            yield break;
        }
    }
    public void DeckResetP2()
    {
        DeckP2.Clear();
        for (int i = 0; i < resetDeck.Count; i++)
        {
            DeckP2.Add(resetDeck[i]);
        }
        for (int i = 0; i < DeckP2.Count; i++)
        {
            DeckP2[i].transform.position = new Vector3(0f, 0f, 0f);
            DeckP2[i].transform.rotation = Quaternion.Euler(0f, 180f, 180f);
            DeckP2[i].SetActive(false);
            DeckP2[i].GetComponent<CardIndex>().IsYoursRed = true;
            DeckP2[i].GetComponent<CardIndex>().IsYoursBlue = false;
            DeckP2[i].GetComponent<CardIndex>().CanBeMoved = true;
        }
        for (int i = 0; i < AvailableHandSlotsP2.Length; i++)
        {
            AvailableHandSlotsP2[i] = true;
        }
        StopCoroutine(StartDraw());
        StartCoroutine(StartDraw());
    }
}