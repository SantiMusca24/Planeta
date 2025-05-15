using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class GameManager : MonoBehaviour
{
    static public bool tapped = false;
    public float count = 0;
    protected float nextTimeCheck = 1;

    public static GameManager Instance;
    [SerializeField] protected ContadorUI uiManager;
    [SerializeField] protected UpgradeManager[] upgradeManagers;
    [SerializeField] protected UpgradeManager2[] upgradeManagers2;
    //public GameObject sas;

    static public int rotatePoints = 1; 

    [SerializeField] protected int updatesPerSecond = 10;
   
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        count = PlayerPrefs.GetFloat("Count", 0f);
    }
     void Update()
    {

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
    public abstract void SetUIManager(ContadorUI newUIManager);
    public abstract void ResetPlayerPrefs();
    public abstract void ForceIncomeUpdate();
    public abstract void RefreshUpgradeManagers();
    public abstract void RefreshUpgradeManagers2();
    public abstract void SaveProgress();
    protected abstract void OnApplicationQuit();
    
}
