using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Advertisements.Advertisement;

[RequireComponent(typeof(RewardedAd), typeof(InterstitialAd), typeof(BannerAd))]
public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance { get; private set; }
    public bool canUse;
    [SerializeField] RewardedAd _myRewardedAd;
    [SerializeField] InterstitialAd interstitialAd;
    [SerializeField] BannerAd bannerAd;
    


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        

    }
    void Start()
    {
        if (_myRewardedAd == null) _myRewardedAd = GetComponent<RewardedAd>();
        if (interstitialAd == null) interstitialAd = GetComponent<InterstitialAd>();
        if (_myRewardedAd == null) bannerAd = GetComponent<BannerAd>();
        _myRewardedAd.LoadRewardedAd();
        StartCoroutine(ShowBannerAd());
        //interstitialAd.ShowInterstitialAd();
    }

    public void ExecuteRewardedAd()
    {
        _myRewardedAd.ShowRewardedAd();
    }
    
    public void ShowInterstitialAd()
    {
        interstitialAd.ShowInterstitialAd();
    }

    IEnumerator ShowBannerAd()
    {
        while (true)
        {
            bannerAd.LoadBannerAd();
            yield return new WaitForSeconds(5);
            bannerAd.ShowBannerAd();
            yield return new WaitForSeconds(30);
            bannerAd.HideBannerAd();
            yield return new WaitForSeconds(30);

        }
    }
}
