using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderRewardButton : MonoBehaviour
{
    public void ExecuteRewardedAd()
    {
        AdsManager.AdRecompensa = true;
        AdsManager.Instance.ExecuteRewardedAd();
    }
    public void ExecuteRewardedAd2()
    {
        AdsManager.AdRecompensa = false;
        AdsManager.Instance.ExecuteRewardedAd();
        gameObject.SetActive(false);
    }
}
