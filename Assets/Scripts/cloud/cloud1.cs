
using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System.Collections;

public class cloud1 : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    public static cloud1 instance;

    static public float maxMeteorWait;
    static public float minMeteorWait;
    static public bool meteorsEnable;
    static public int meteorSpd;
    static public int meteorReward;
    static public float maxAsteroidHeight;
    static public float minAsteroidHeight;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task Start()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        StartCoroutine(UpdateMyRemoteData());
    }

    IEnumerator UpdateMyRemoteData()
    {
        while (true)
        {
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
            yield return new WaitForSeconds(5);
        }

    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());

        maxMeteorWait = RemoteConfigService.Instance.appConfig.GetFloat("MaxAsteroidWait");
        minMeteorWait = RemoteConfigService.Instance.appConfig.GetFloat("MinAsteroidWait");
        meteorsEnable = RemoteConfigService.Instance.appConfig.GetBool("MeteorsEnabled"); 
        meteorSpd = RemoteConfigService.Instance.appConfig.GetInt("MeteorSpeed");
        meteorReward = RemoteConfigService.Instance.appConfig.GetInt("MeteorPoints");
        maxAsteroidHeight = RemoteConfigService.Instance.appConfig.GetFloat("MaxMeteorHeight");
        minAsteroidHeight = RemoteConfigService.Instance.appConfig.GetFloat("MinMeteorHeight");


    }
}