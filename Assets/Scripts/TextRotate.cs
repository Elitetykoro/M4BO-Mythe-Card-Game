using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotate : MonoBehaviour
{
    [SerializeField]private RectTransform[] TextTransform;

    private void Start()
    {
        TextTransform = GetComponentsInChildren<RectTransform>();
    }
    public void RotateTextToP2Pov()
    {
        for (int i = 0; i < TextTransform.Length; i++)
        {
            TextTransform[i].rotation = Quaternion.Euler(180f, 180f, 0f);
        }
    }
    public void RotateTextToP1Pov()
    {
        for (int i = 0; i < TextTransform.Length; i++)
        {
            TextTransform[i].rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }



}
