using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private GameObject imagen1;
    [SerializeField] private GameObject imagen2;
    [SerializeField] private GameObject imagen3;
    [SerializeField] private float rewardAmount = 10000f;
    [SerializeField] string _androidRewarded = "Rewarded_Android";
    [SerializeField] private FishMinigame fishMinigame;
    [SerializeField] private WoodMiniGame woodMinigame;
    [SerializeField] private WoodMiniGame farmMinigame;
    [SerializeField] private int sceneName = 1;
    //[SerializeField] string _iOSId = "5876854";
    //string _actualGameIdInUse;
    //[SerializeField] bool _isTestingMode;

    // DESCOMENTAR Y ARREGLAR EL ERROR DE "this" CUANDO QUERRAMOS METERLO EN IOS
    /*void Awake()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        _actualGameIdInUse = _androidId;

#else
        _actualGameIdInUse = _iOSId;

#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_actualGameIdInUse, _isTestingMode, this);
        }
    }*/

    public void LoadRewardedAd()
    {
        Advertisement.Load(_androidRewarded, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(_androidRewarded, this);
        LoadRewardedAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("MOSTRAR BOTON REWARDED");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"ERROR CON ANUNCIO REWARDED {placementId}: {error}: {message} ");
    }

    public void OnUnityAdsShowClick(string placementId)
    {}

    // RECOMPENSA POR COMPLETAR EL ANUNCIO
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == _androidRewarded )
        {
            if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                if(AdsManager.AdRecompensa == true)
                {

                    
                    GameManager.Instance.count += rewardAmount;
                    PlayerPrefs.SetFloat("Count", GameManager.Instance.count);
                    PlayerPrefs.Save();
                    Debug.Log("RECOMPENSA FULL");
                }
                else if (sceneName == 1) 
                {

                    fishMinigame.CancelarCooldown();
                    imagen2.SetActive(true);
                }
                else if (sceneName == 2)
                {
                    woodMinigame.CancelarCooldown();
                    imagen2.SetActive(true);
                }
                else if (sceneName == 3)
                {
                    farmMinigame.CancelarCooldown();
                    //imagen2.SetActive(true);
                }

            }
            if (showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED))
            {
                if (AdsManager.AdRecompensa == true)
                {
                    GameManager.Instance.count += rewardAmount / 2;
                    PlayerPrefs.SetFloat("Count", GameManager.Instance.count);
                    PlayerPrefs.Save();
                }
                Debug.Log("RECOMPENSA MITAD");
            }
            imagen2.SetActive(true);
            GameManager.Instance.count += rewardAmount;
            PlayerPrefs.SetFloat("Count", GameManager.Instance.count);
            PlayerPrefs.Save();
            if (showCompletionState.Equals(UnityAdsShowCompletionState.UNKNOWN)) Debug.Log("ALGO SALIÓ MAL");
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {}

    public void OnUnityAdsShowStart(string placementId)
    {}


}
