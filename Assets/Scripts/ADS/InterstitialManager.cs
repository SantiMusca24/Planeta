using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialManager : MonoBehaviour
{
    int randomNum;

    void Awake()
    {
        randomNum = Random.Range(0, 2);
        if (randomNum == 0) AdsManager.Instance.ShowInterstitialAd();
    }
}
