using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitialize : MonoBehaviour, IUnityAdsInitializationListener
{

    [SerializeField] string _androidId = "5876855";
    [SerializeField] string _iOSId = "5876854";
    string _actualGameIdInUse;
    [SerializeField] bool _isTestingMode;

    public void OnInitializationComplete()
    {
        Debug.Log("PODEMOS TIRAR ANUNCIO");
        AdsManager.Instance.canUse = true;
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"ERROR CON ANUNCIO: {error}: {message} ");
    }

    // Start is called before the first frame update
    void Awake()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        _actualGameIdInUse = _androidId;

#else
        _actualGameIdInUse = _iOSId;

#endif

        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_actualGameIdInUse, _isTestingMode, this);
        }
    }
}
