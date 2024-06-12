using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class GridIndex : MonoBehaviour
{
    public int gridIndex;
    public GameObject PlacedCard;
    public bool IsFilledWithCard;
   
    public void ClearGrid()
    {
        PlacedCard = null;
        IsFilledWithCard = false;
    }
}
