using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerP1 : MonoBehaviour
{
    private List<GameObject> Deck = new List<GameObject>();
    public Transform[] HandSlotsP1;
    public bool[] AvailableHandSlotsP1;
    public List<GameObject> resetDeck = new List<GameObject>();


    private void Start()
    {
        DeckResetP1();
        StartCoroutine(StartDraw());
        
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public void DrawCard()
    {
        if (Deck.Count >= 1)
        {
            GameObject RandomCard = Deck[Random.Range(0, Deck.Count)];

            for (int i = 0; i < AvailableHandSlotsP1.Length; i++)
            {
                if (AvailableHandSlotsP1[i] == true)
                {
                    RandomCard.SetActive(true);
                    RandomCard.GetComponent<CardIndex>().HandIndex = i;
                    RandomCard.transform.position = HandSlotsP1[i].transform.position;
                    AvailableHandSlotsP1[i] = false;
                    Deck.Remove(RandomCard);
                    return;
                }
            }
        }
    }

    private IEnumerator StartDraw()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            DrawCard();
            yield return new WaitForSeconds(0.3f);
            DrawCard();
            yield return new WaitForSeconds(0.3f);
            DrawCard();
            yield return new WaitForSeconds(0.3f);
            yield break;
        }
    }
    public void DeckResetP1()
    {
        Deck.Clear();
        for(int i = 0; i < resetDeck.Count; i++)
        {
            Deck.Add(resetDeck[i]);
        }

        Debug.Log("cards in deck" + Deck.Count);
        for(int i = 0;i < Deck.Count; i++)
        {
            Deck[i].transform.position = new Vector3(0f, 0f, 0f);
            Deck[i].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Deck[i].SetActive(false);
            Deck[i].GetComponent<CardIndex>().IsYoursRed = false;
            Deck[i].GetComponent<CardIndex>().IsYoursBlue = true;
            Deck[i].GetComponent<CardIndex>().CanBeMoved = true;
        }
        for (int i = 0; i < AvailableHandSlotsP1.Length; i++)
        {
            AvailableHandSlotsP1[i] = true;
        }
        StopCoroutine(StartDraw());
        StartCoroutine(StartDraw());
    }
}