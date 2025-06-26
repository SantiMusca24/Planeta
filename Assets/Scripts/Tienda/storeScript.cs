using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class storeScript : MonoBehaviour
{
    [SerializeField] private ScriptableRendererFeature gemPurchaseFeature;
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
        GameManager.Instance.count += GameManager.Instance.GetIncomePerSecond() * (3600 * hoursMult);
    }

    public void BuyGems()
    {
        //menu de dinero real
        if (gemPurchaseFeature != null)
        {
            gemPurchaseFeature.SetActive(true);
            StartCoroutine(DisableFeatureAfterDelay(gemPurchaseFeature, 0.5f));
        }
        GameManager.Instance.gems += 5;
    }
    private IEnumerator DisableFeatureAfterDelay(ScriptableRendererFeature feature, float delay)
    {
        yield return new WaitForSeconds(delay);
        feature.SetActive(false);
    }
}
