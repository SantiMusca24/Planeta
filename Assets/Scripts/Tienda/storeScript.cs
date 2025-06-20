using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class storeScript : MonoBehaviour
{
    void Start()
    {
        
    }
    public void CloseStore()
    {
        gameObject.SetActive(false);
    }
    public void RemoveAds()
    {
        //menu de dinero real
        AdsManager.adsAllowed = false;
        AdsManager.Instance.bannerAd.HideBannerAd();
    }
    public void BuyGold()
    {
        //menu de dinero real
        GameManager.Instance.count += UpgradeManager.levelPublic * 1000;
    }
    public void BuyGems()
    {
        //menu de dinero real
        GameManager.Instance.gems += 5;
    }
}
