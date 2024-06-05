using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardIndex : MonoBehaviour
{
    public Vector3 cardStartPosP1;
    public Vector3 cardStartPosP2;
    public int cardIndex;
    public int HandIndex;
    public bool CanBeMoved = true;
    public int ATKleft, ATKright, ATKtop, ATKbottom;
    [SerializeField] private TMP_Text AtkTextTop, AtkTextBottom, AtkTextRight, AtkTextLeft;
    [SerializeField] private TMP_Text DSAtkTextTop, DSAtkTextBottom, DSAtkTextRight, DSAtkTextLeft;
    [SerializeField] public bool IsYoursBlue;
    [SerializeField] public bool IsYoursRed;
    private Vector3 rot;
    public bool IsInHand;
    public float timer;
    private Rigidbody RB;

    private GameObject GMP1;
    private GameObject GMP2;

    private void Start()
    {
        if(IsYoursRed)
        {
            rot += new Vector3(0f, 180f, 180f);
            transform.rotation = Quaternion.Euler(rot);
        }
        GMP1 = GameObject.Find("GameManagerPlayer1");
        GMP2 = GameObject.Find("GameManagerPlayer2");
        transform.position = new Vector3(20, 0, 0);
        this.gameObject.SetActive(false);
        RB = GetComponent<Rigidbody>();
        AtkTextTop.text = ATKtop.ToString();
        AtkTextBottom.text = ATKbottom.ToString();
        AtkTextLeft.text = ATKleft.ToString();
        AtkTextRight.text = ATKright.ToString();
        DSAtkTextTop.text = ATKtop.ToString();
        DSAtkTextBottom.text = ATKbottom.ToString();
        DSAtkTextLeft.text = ATKleft.ToString();
        DSAtkTextRight.text = ATKright.ToString();
    }

    private void Update()
    {
        cardStartPosP2 = GMP2.GetComponent<GameManagerP2>().HandSlotsP2[HandIndex].position;
        cardStartPosP1 = GMP1.GetComponent<GameManagerP1>().HandSlotsP1[HandIndex].position;
        transform.rotation = Quaternion.Euler(rot);
        if(timer <= 1)
        {
            timer += Time.deltaTime;
        }
    }

    public void FlipToBlue()
    {
        if (!IsYoursBlue)
        {
            rot.z = 0f;
            IsYoursBlue = true; 
            IsYoursRed = false;
        }  
    }
    public void FlipToRed()
    {
        if (!IsYoursRed) 
        {
            rot.z = 180f;
            IsYoursBlue = false;
            IsYoursRed = true;
        }
    }

    public void DrawNewCardP1()
    {
        GMP1.GetComponent<GameManagerP1>().AvailableHandSlotsP1[HandIndex] = true;
        GMP1.GetComponent<GameManagerP1>().DrawCard();
    }
    public void DrawNewCardP2()
    {
        GMP2.GetComponent<GameManagerP2>().AvailableHandSlotsP2[HandIndex] = true;
        GMP2.GetComponent<GameManagerP2>().DrawCard();
    }
    public void EndTurnRotateP2POV()
    {
        if (IsYoursBlue)
        {
            rot = new Vector3(0f, 180f, 0f);
        }
        if (IsYoursRed)
        {
            rot = new Vector3(0f, 180f, 180f);
        }
    }
    public void EndTurnRotateP1POV()
    {
        if (IsYoursBlue)
        {
            rot = new Vector3(0f, 0f, 0f);
        }
        if (IsYoursRed)
        {
            rot = new Vector3(0f, 0f, 180f);
        }
        
    }

}
