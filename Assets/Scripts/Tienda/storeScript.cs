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
    public void BuyGold1()
    {
        BuyGoldGen(1);
    }
    public void BuyGold5()
    {
        BuyGoldGen(5);
    }
    public void BuyGold10()
    {
        BuyGoldGen(10);
    }
    public void BuyGoldGen(float hoursMult)
    {
        GameManager.Instance.count += GameManager.Instance.GetIncomePerSecond() * (3600 * hoursMult);
    }

    public void BuyGems()
    {
        //menu de dinero real
        GameManager.Instance.gems += 5;
    }
}
