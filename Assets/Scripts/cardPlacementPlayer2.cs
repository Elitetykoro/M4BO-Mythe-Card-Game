using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class cardPlacementPlayer2 : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private GameObject heldCard;
    private Vector3 position;
    [SerializeField] private bool isHoldingCard;
    private GameObject hoveringGrid;
    [SerializeField] private int hoveringGridIndex;
    public GameObject[] GridSquares;
    public bool ThisPlayerActive = true;
    public GameObject TMR;
    public GameObject PlacementParticle;
    //private bool isHoldigCard = false;
    // Start is called before the first frame update
    private void Start()
    {
        TMR = GameObject.Find("TurnManager");
    }
    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Grid"))
            {
                hoveringGrid = hit.collider.gameObject;
                hoveringGridIndex = hoveringGrid.GetComponent<GridIndex>().gridIndex;
                Debug.Log(hoveringGridIndex.ToString());
            }
            else
            {
                hoveringGrid = null;
            }


            if (Input.GetMouseButton(0) && !isHoldingCard && hit.collider.CompareTag("Card"))
            {
                heldCard = hit.collider.gameObject;
                if (heldCard.GetComponent<CardIndex>().CanBeMoved == true)
                {
                    isHoldingCard = true;
                    heldCard.GetComponent<BoxCollider>().enabled = false;
                    StartCoroutine(HoldCard());
                }
                else
                {
                    Debug.Log("nah");
                }

            }
            else if (!Input.GetMouseButton(0) && isHoldingCard)
            {
                isHoldingCard = false;
                heldCard.GetComponent<CardIndex>().CanBeMoved = false;
                if (hoveringGrid != null && !hoveringGrid.GetComponent<GridIndex>().IsFilledWithCard)
                {

                    StopCoroutine(HoldCard());
                    Vector3 ParticlePos = hoveringGrid.transform.position + new Vector3(0, 1, 0);
                    int placedIndex = hoveringGrid.GetComponent<GridIndex>().gridIndex;
                    hoveringGrid.GetComponent<GridIndex>().PlacedCard = heldCard;
                    heldCard.transform.position = hoveringGrid.transform.position;
                    hoveringGrid.GetComponent<GridIndex>().IsFilledWithCard = true;
                    Instantiate(PlacementParticle, ParticlePos, Quaternion.Euler(90,0,0));
                    heldCard.GetComponent<BoxCollider>().enabled = true;
                    switch (placedIndex)
                    {
                        case 1:
                            if (GridSquares[1].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[0].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright > GridSquares[1].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft)
                                {
                                    FlipCard(GridSquares[1].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[3].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[0].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom > GridSquares[3].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop)
                                {
                                    FlipCard(GridSquares[3].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            break;
                        case 2:
                            if (GridSquares[0].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[1].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft > GridSquares[0].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright)
                                {
                                    FlipCard(GridSquares[0].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[2].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[1].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright > GridSquares[2].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft)
                                {
                                    FlipCard(GridSquares[2].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[4].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[1].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom > GridSquares[4].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop)
                                {
                                    FlipCard(GridSquares[4].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            break;
                        case 3:
                            if (GridSquares[5].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[2].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom > GridSquares[5].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop)
                                {
                                    FlipCard(GridSquares[5].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[1].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[2].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft > GridSquares[1].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright)
                                {
                                    FlipCard(GridSquares[1].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            break;
                        case 4:
                            if (GridSquares[0].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[3].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop > GridSquares[0].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom)
                                {
                                    FlipCard(GridSquares[0].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[4].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[3].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright > GridSquares[4].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft)
                                {
                                    FlipCard(GridSquares[4].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[6].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[3].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom > GridSquares[6].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop)
                                {
                                    FlipCard(GridSquares[6].GetComponent<GridIndex>().PlacedCard);
                                }
                            }

                            break;
                        case 5:
                            if (GridSquares[5].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[4].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright > GridSquares[5].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft)
                                {
                                    FlipCard(GridSquares[5].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[1].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[4].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop > GridSquares[1].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom)
                                {
                                    FlipCard(GridSquares[1].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[3].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[4].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft > GridSquares[3].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright)
                                {
                                    FlipCard(GridSquares[3].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[7].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[4].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom > GridSquares[7].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop)
                                {
                                    FlipCard(GridSquares[7].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            break;
                        case 6:
                            if (GridSquares[8].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[5].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom > GridSquares[8].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop)
                                {
                                    FlipCard(GridSquares[8].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[4].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[5].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft > GridSquares[4].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright)
                                {
                                    FlipCard(GridSquares[4].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[2].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[5].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop > GridSquares[2].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom)
                                {
                                    FlipCard(GridSquares[2].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            break;
                        case 7:
                            if (GridSquares[3].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[6].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop > GridSquares[3].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom)
                                {
                                    FlipCard(GridSquares[3].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[7].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[6].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright > GridSquares[7].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft)
                                {
                                    FlipCard(GridSquares[7].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            break;
                        case 8:
                            if (GridSquares[4].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[7].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop > GridSquares[4].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom)
                                {
                                    FlipCard(GridSquares[4].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[8].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[7].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright > GridSquares[8].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft)
                                {
                                    FlipCard(GridSquares[8].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[6].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[7].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft > GridSquares[6].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright)
                                {
                                    FlipCard(GridSquares[6].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            break;
                        case 9:
                            if (GridSquares[5].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[8].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKtop > GridSquares[5].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKbottom)
                                {
                                    FlipCard(GridSquares[5].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            if (GridSquares[7].GetComponent<GridIndex>().IsFilledWithCard)
                            {
                                if (GridSquares[8].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKleft > GridSquares[7].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().ATKright)
                                {
                                    FlipCard(GridSquares[7].GetComponent<GridIndex>().PlacedCard);
                                }
                            }
                            break;
                        default:
                            Debug.Log("incorrect grid index");
                            break;
                    }
                    heldCard.GetComponent<CardIndex>().DrawNewCardP2();
                    TMR.GetComponent<TurnManager>().CardsOnBoard.Add(heldCard);
                    heldCard = null;
                    TMR.GetComponent<TurnManager>().TurnChangeToP1();
                   

                    //GridSquares[0].GetComponent<GridIndex>().PlacedCard.GetComponent<CardIndex>().

                }
                else if (hit.collider.CompareTag("Table") || hit.collider.CompareTag("Card"))
                {
                    StopCoroutine(HoldCard());
                    heldCard.transform.position = heldCard.GetComponent<CardIndex>().cardStartPosP2;
                    heldCard.GetComponent<BoxCollider>().enabled = true;
                    heldCard.GetComponent<CardIndex>().CanBeMoved = true;
                    heldCard = null;
                }


            }
        }



    }

    IEnumerator HoldCard()
    {
        while (true)
        {
            position = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 23)) - heldCard.transform.position) / 2;
            //Debug.Log("You are holding a card");
            heldCard.transform.position += position;
            yield return new WaitForEndOfFrame();
        }
    }

  

    void FlipCard(GameObject CardToBeFlipped)
    {
        Debug.Log(CardToBeFlipped.GetComponent<CardIndex>().cardIndex + " is going to be flipped");
        CardToBeFlipped.GetComponent<CardIndex>().FlipToRed();
    }

}