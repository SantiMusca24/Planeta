using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject bg, text1, text2, text3, text4, text5, text6, text7, text8, text9, text1a, text2a, text3a;
    public int count = 0;
    private void Start()
    {
        count = 0;
    }
    public void StartChain()
    {
        bg.SetActive(true);
        text1.SetActive(true);
    }
    public void Tap1()
    {
        count++;
        text1.SetActive(false);
        text2.SetActive(true);
    }
    public void Tap2()
    {
        count++;
        text2.SetActive(false);
        bg.SetActive(false);
    }
    public void Tap3()
    {
        count++;
        text3.SetActive(false);
        text4.SetActive(true);
    }
    public void Tap4()
    {
        count++;
        bg.SetActive(false);
        text4.SetActive(false);
        StartCoroutine(Wait1(3, text5));
    }
    public void Tap5()
    {
        count++;
        text5.SetActive(false);
        text6.SetActive(true);

    }
    public void Tap6()
    {
        count++;
        bg.SetActive(false);
        text6.SetActive(false);
        StartCoroutine(Wait1(3, text7));
    }
    public void Tap7()
    {
        count++;
        text7.SetActive(false);
        text8.SetActive(true);
    }
    public void Tap8()
    {
        count++;
        bg.SetActive(false);
        text8.SetActive(false);
        StartCoroutine(Wait1(5, text9));
    }
    public void Tap9()
    {
        count++;
        bg.SetActive(false);
        text9.SetActive(false);
        GameManager.Instance.sawTutorial1 = true;
        PlayerPrefs.SetInt("Tutorial1", 1);
        PlayerPrefs.Save();
    }

    IEnumerator Wait1(int seconds, GameObject text)
    {
        yield return new WaitForSeconds(seconds);
        bg.SetActive(true);
        text.SetActive(true);
    }
}
