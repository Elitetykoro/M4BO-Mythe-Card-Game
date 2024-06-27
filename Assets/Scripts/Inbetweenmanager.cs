using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Inbetweenmanager : MonoBehaviour
{
    [SerializeField] private TMP_Text WhoWonText;
    [SerializeField] private TMP_Text GoliathHpText;
    [SerializeField] private TMP_Text DavidHpText;

    
    void Start()
    {
        GoliathHpText.text = PlayerPrefs.GetInt("GHP").ToString();
        DavidHpText.text = PlayerPrefs.GetInt("DHP").ToString();
        StartCoroutine(DamageText());
        if (PlayerPrefs.GetInt("DATK") > PlayerPrefs.GetInt("GATK"))
        {
            WhoWonText.text = "Round Won By David";
        }
        else if (PlayerPrefs.GetInt("GATK") > PlayerPrefs.GetInt("DATK"))
        {
            WhoWonText.text = "Round Won By Goliath";
        }

    }
    private void Update()
    {
        
        if (Input.anyKey)
        {
            SceneManager.LoadScene("main");
        }
    }
    IEnumerator DamageText()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            for (int i = PlayerPrefs.GetInt("DATK"); i > 0; i--)
            {
                yield return new WaitForSeconds(0.1f);
                PlayerPrefs.SetInt("GHP", PlayerPrefs.GetInt("GHP") - 1);
                GoliathHpText.text = PlayerPrefs.GetInt("GHP").ToString();
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = PlayerPrefs.GetInt("GATK"); i > 0; i--)
            {
                yield return new WaitForSeconds(0.1f);
                PlayerPrefs.SetInt("DHP", PlayerPrefs.GetInt("DHP") - 1);
                DavidHpText.text = PlayerPrefs.GetInt("DHP").ToString();
            }
            yield return new WaitForEndOfFrame();
            if(PlayerPrefs.GetInt("DHP") <= 0|| PlayerPrefs.GetInt("GHP") <= 0)
            {
                SceneManager.LoadScene("main");
            }
            else
            {
                SceneManager.LoadScene("main");
            }
            yield break;
        }
    }
}
