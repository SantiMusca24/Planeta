using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{   

    [SerializeField] string _androidRewarded = "Rewarded_Android";
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
            if (showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
            {
                // PREFERIBLE HACER UN SINGLETON PARA LA RECOMPENSA
                Debug.Log("RECOMPENSA FULL");
            }
            if (showCompletionState.Equals(UnityAdsCompletionState.SKIPPED)) Debug.Log("RECOMPENSA MITAD");
            if (showCompletionState.Equals(UnityAdsCompletionState.UNKNOWN)) Debug.Log("ALGO SALIÓ MAL");
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {}

    public void OnUnityAdsShowStart(string placementId)
    {}


}
