using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerP2 : MonoBehaviour
{
    public List<GameObject> DeckP2 = new List<GameObject>();
    public Transform[] HandSlotsP2;
    public bool[] AvailableHandSlotsP2;


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
}