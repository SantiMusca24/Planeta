using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject bg, text1, text2, text3, text4, text5, text6, text7, text8, text9, text1a, text2a, text3a, text4a, text5a, text6a;
    public GameObject[] icons;
    public int count = 0;
    bool canClick = true;
    private void Start()
    {
        count = 0;
        canClick = true;
    }
    public void StartChain()
    {
        StartCoroutine(ClickWait(1));
        bg.SetActive(true);
        text1.SetActive(true);
        foreach (var icon in icons)
        {
            icon.SetActive(false);
        }
    }
    public void Tap1()
    {
        if (!canClick) return;
        StartCoroutine(ClickWait(1));
        count++;
        count++;
        icons[0].SetActive(true);
        icons[11].SetActive(true);
        icons[3].SetActive(true);
        icons[4].SetActive(true);
        text1.SetActive(false);
        text2.SetActive(true);
    }
    public void Tap2()
    {
        //if (!canClick) return;
        //count++;
        text2.SetActive(false);
        text3.SetActive(true);
        bg.SetActive(false);
    }
    public void Tap3()
    {
        if (!canClick) return;
        StartCoroutine(ClickWait(1));
        count++;
        text3.SetActive(false);
        text4.SetActive(true);
    }
    public void Tap4()
    {
        if (!canClick) return;
        count++;
        bg.SetActive(false);
        StartCoroutine(ClickWait(1));
        text4.SetActive(false);
        text9.SetActive(true);
        //StartCoroutine(Wait1(3, text5, icons[7]));
    }
    public void Tap5()
    {
        count++;
        icons[8].SetActive(true);
        text5.SetActive(false);
        text6.SetActive(true);

    }
    public void Tap6()
    {
        count++;
        icons[8].SetActive(true);
        bg.SetActive(false);
        text6.SetActive(false);
        StartCoroutine(Wait1(3, text7, icons[9]));
    }
    public void Tap7()
    {
        if (!canClick) return;
        StartCoroutine(ClickWait(1));
        count++;
        text7.SetActive(false);
        text8.SetActive(true);
    }
    public void Tap8()
    {
        if (!canClick) return;
        count++;
        bg.SetActive(false);
        text8.SetActive(false);
        StartCoroutine(Wait1(5, text9));
    }
    public void Tap9()
    {
        if (!canClick) return;
        count++;
        bg.SetActive(false);
        text9.SetActive(false);
        GameManager.Instance.sawTutorial1 = true;
        PlayerPrefs.SetInt("Tutorial1", 1);
        PlayerPrefs.Save();
    }
    public void StartChain2()
    {
        foreach (var icon in icons)
        {
            icon.SetActive(false);
        }
        //bg.SetActive(true);
        text1a.SetActive(true);
    }
    public void Tap1a()
    {
        if (count != 0) return;
        int check = PlayerPrefs.GetInt("Tutorial2", 0);
        if (check != 0) return;
        count++;
        text1a.SetActive(false);
        text2a.SetActive(true);
        //icons[0].SetActive(true); MINIGAME
        //icons[1].SetActive(true); AD
    }
    public void Tap2a()
    {
        if (count != 1) return;
        int check = PlayerPrefs.GetInt("Tutorial2", 0);
        if (check != 0) return;
        count++;
        StartCoroutine(ClickWait(1));
        text2a.SetActive(false);
        text3a.SetActive(true);
        //icons[2].SetActive(true); PLANET ICON
    }
    public void Tap3a()
    {
        if (count != 2) return;
        if (!canClick) return;
        count++;
        bg.SetActive(false);
        text3a.SetActive(false);
        text4a.SetActive(true);
        icons[0].SetActive(true); // MINIGAME
    }
    public void Tap4a()
    {
        if (count != 3) return;
        int check = PlayerPrefs.GetInt("Tutorial2", 0);
        if (check != 0) return;
        count++;
        StartCoroutine(ClickWait(1));
        text4a.SetActive(false);
        text5a.SetActive(true);
        icons[1].SetActive(true); // AD
        //icons[2].SetActive(true); PLANET ICON
    }
    public void Tap5a()
    {
        if (count != 4) return;
        int check = PlayerPrefs.GetInt("Tutorial2", 0);
        if (check != 0) return;
        count++;
        //StartCoroutine(ClickWait(1));
        text5a.SetActive(false);
        text6a.SetActive(true);
        icons[2].SetActive(true); // PLANET ICON
    }
    public void Tap6a()
    {
        PlayerPrefs.SetInt("Tutorial2", 1);
        if (count != 5) return;
        int check = PlayerPrefs.GetInt("Tutorial2", 0);
        if (check != 0) return;
        count++;
        text6a.SetActive(false);
        //icons[2].SetActive(true); PLANET ICON
    }
    IEnumerator Wait1(int seconds, GameObject text, GameObject icon = null)
    {
        yield return new WaitForSeconds(seconds);
        bg.SetActive(true);
        StartCoroutine(ClickWait(1));
        text.SetActive(true);
        if (icon != null) icon.SetActive(true);
    }
    public void ClickWaitMethod(int seconds)
    {
        StartCoroutine(ClickWait(seconds));
    }
    IEnumerator ClickWait(int seconds)
    {
        canClick = false;
        yield return new WaitForSeconds(seconds);
        canClick = true;
    }
}
