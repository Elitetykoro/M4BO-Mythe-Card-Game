using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerP1 : MonoBehaviour
{
    public List<GameObject> Deck = new List<GameObject>();
    public Transform[] HandSlotsP1;
    public bool[] AvailableHandSlotsP1;


    private void Start()
    {
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
}