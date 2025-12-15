using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class GameManager : MonoBehaviour
{
    public bool gotMoney = false;
    public float IPS;
    static public bool tapped = false;
    public float count = 0;
    public float gems = 0;
    protected float nextTimeCheck = 1;

    public bool doubleIncomeActive = false;
    public float doubleIncomeTimer = 0f;
    public static GameManager Instance;
    [SerializeField] protected ContadorUI uiManager;
    [SerializeField] protected PlanetUI uiPlanet;
    [SerializeField] protected BosqueUI uiBosque;
    [SerializeField] protected PescaUI uiPesca;
    [SerializeField] protected GranjaUI uiGranja;
    [SerializeField] protected UpgradeManager[] upgradeManagers;
    [SerializeField] protected UpgradeManager2[] upgradeManagers2;

    public bool sawTutorial1 = false;
    public bool sawTutorial2 = false;

    //public GameObject sas;

    static public int rotatePoints = 1; 

    [SerializeField] protected int updatesPerSecond = 10;
   
    private void Awake()
    {
        IPS = PlayerPrefs.GetFloat("IPS", 0f);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        count = PlayerPrefs.GetFloat("Count", 0f);
        gems = PlayerPrefs.GetFloat("Gems", 0f);

    }
     void Update()
    {
        rotatePoints = (UpgradeManager2.level1 + 1) * (UpgradeManager2.level2 + 1) * (UpgradeManager2.level3 + 1);
        

        if (tapped)
        {
            tapped = false;
            count += (1 + UpgradeManager2.level1) * cloud1.meteorReward;
        }

        if (nextTimeCheck< Time.timeSinceLevelLoad)
        {

            if (!sceneLoad.planetScene)
            {
                Debug.Log("BOSQUE PUNTOS");
                IdleCalculate();
            }
            else 
            {
                Debug.Log("TIERRA PUNTOS");
                IdleCalculate2(); 
            }
            nextTimeCheck = Time.timeSinceLevelLoad + (1f / updatesPerSecond);
             
        }
        if (doubleIncomeActive)
        {
            doubleIncomeTimer -= Time.deltaTime;
            if (doubleIncomeTimer <= 0f)
            {
                doubleIncomeActive = false;
                doubleIncomeTimer = 0f;
            }
        }
    }
    /*static public void tapped()
    {
        GameManager.Instance.count += UpgradeManager2.level1 * 999;
    }*/
    protected abstract void IdleCalculate2();
    protected abstract void IdleCalculate();
    public abstract float GetIncomePerSecond();
    public abstract void OnEnable();
    protected abstract void OnDisable();
    public abstract void RotateAction();
    protected abstract IEnumerator AutoIncrementCoroutine();
    public abstract bool PurchaseAction(int cost);
    public abstract void RefreshUI();
    //public abstract void SetUIManager(ContadorUI newUIManager);
    public abstract void SetUIPlanet(PlanetUI newUIManager);
    public abstract void SetUIBosque(BosqueUI newUIManager);
    public abstract void SetUIPesca(PescaUI newUIManager);
    public abstract void SetUIGranja(GranjaUI newUIManager);

    public abstract void ResetPlayerPrefs();
    public abstract void ForceIncomeUpdate();
    public abstract void RefreshUpgradeManagers();
    public abstract void RefreshUpgradeManagers2();
    public abstract void SaveProgress();
    protected abstract void OnApplicationQuit();
    
}
