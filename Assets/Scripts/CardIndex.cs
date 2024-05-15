using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardIndex : MonoBehaviour
{
    public Vector3 cardStartPos;
    public int cardIndex;
    public bool CanBeMoved = true;
    public int ATKleft, ATKright, ATKtop, ATKbottom;
    [SerializeField] private TMP_Text AtkTextTop, AtkTextBottom, AtkTextRight, AtkTextLeft;
    private void Start()
    {
        AtkTextTop.text = ATKtop.ToString();
        AtkTextBottom.text = ATKbottom.ToString();
        AtkTextLeft.text = ATKleft.ToString();
        AtkTextRight.text = ATKright.ToString();
    }
}
