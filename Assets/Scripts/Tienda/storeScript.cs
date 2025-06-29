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
        if (GameManager.Instance.gems >= 100 && AdsManager.adsAllowed)
        {
            GameManager.Instance.gems -= 100;
            AdsManager.adsAllowed = false;
            AdsManager.Instance.bannerAd.HideBannerAd();
        }
        else if (!AdsManager.adsAllowed)
        {
            GameManager.Instance.gems += 100;
            AdsManager.adsAllowed = true;
        }
    }
    public void BuyGold1()
    {
        if (GameManager.Instance.gems >= 5)
        {
            GameManager.Instance.gems -= 5;            
            BuyGoldGen(1);
        }        
    }
    public void BuyGold5()
    {
        if (GameManager.Instance.gems >= 7)
        {
            GameManager.Instance.gems -= 7;
            BuyGoldGen(5);
        }
    }
    public void BuyGold10()
    {
        if (GameManager.Instance.gems >= 10)
        {
            GameManager.Instance.gems -= 10;
            BuyGoldGen(10);
        }
    }
    public void BuyGoldGen(float hoursMult)
    {
        GameManager.Instance.count += GameManager.Instance.IPS * (3600 * hoursMult);
    }

    public void BuyGems()
    {
        //menu de dinero real
        GameManager.Instance.gems += 5;
    }
}
