using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class cardPlacement : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private GameObject heldCard;
    private Vector3 position;
    [SerializeField]private bool isHoldingCard;
    private GameObject hoveringGrid;
    [SerializeField] private int hoveringGridIndex;
    //private bool isHoldigCard = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        


        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit))
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

            if (hit.collider.CompareTag("Card"))
            {
                Debug.Log("Your mouse is above a card");
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
            else if(!Input.GetMouseButton(0) && isHoldingCard)
            {
                    isHoldingCard = false;
                    heldCard.GetComponent<CardIndex>().CanBeMoved = false;
                    if (hoveringGrid != null && !hoveringGrid.GetComponent<GridIndex>().IsFilledWithCard)
                    {
                        
                        StopCoroutine(HoldCard());
                        hoveringGrid.GetComponent<GridIndex>().PlacedCard = heldCard;
                        heldCard.transform.position = hoveringGrid.transform.position;
                        hoveringGrid.GetComponent<GridIndex>().IsFilledWithCard = true;
                        heldCard.GetComponent<BoxCollider>().enabled = true;
                        heldCard = null;

                    }
                    else if(hit.collider.CompareTag("Table"))
                    {

                        StopCoroutine(HoldCard());
                        heldCard.transform.position = heldCard.GetComponent<CardIndex>().cardStartPos;
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
            position = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 25)) - heldCard.transform.position) / 2;
            Debug.Log("You are holding a card");
            heldCard.transform.position += position;
            yield return new WaitForEndOfFrame();
        }
    }

}

