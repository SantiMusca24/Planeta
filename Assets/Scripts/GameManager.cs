using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    public float count = 0;
    float nextTimeCheck = 1;

    public static GameManager Instance;
    [SerializeField] private ContadorUI uiManager;
    [SerializeField] UpgradeManager[] upgradeManagers;
    [SerializeField] UpgradeManager2[] upgradeManagers2;
    //public GameObject sas;

    static public int rotatePoints = 1; 

    [SerializeField] int updatesPerSecond = 10;
   
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
     void Update()
    {
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
    void IdleCalculate2()
    {        
        float sum = 0;
        //Debug.Log("STEP 1 SUM " +sum);
        foreach (var upgradeManager in upgradeManagers2)
        {
            //Debug.Log("STEP 2");
            sum += upgradeManager.CalculateIncomePerSecond();
        }
        Debug.Log("SUMA 1 PLANETA " + sum);
        count += sum / updatesPerSecond;
        Debug.Log("SUMA 2 PLANETA " + sum);
        uiManager.UpdateUI();
    }
    void IdleCalculate()
    {
        float sum = 0;
        foreach (var upgradeManager in upgradeManagers)
        {
            sum += upgradeManager.CalculateIncomePerSecond();
            upgradeManager.UpdateUI();
        }
        Debug.Log("SUMA 1 BOSQUE " + sum);
        count += sum /updatesPerSecond;
        Debug.Log("SUMA 2 BOSQUE " + sum);
        uiManager.UpdateUI();
    }
    public float GetIncomePerSecond()
    {
        float sum = 0;
        foreach (var upgradeManager in upgradeManagers)
        {
            sum += upgradeManager.CalculateIncomePerSecond();
        }
        return sum;
    }
    public void OnEnable()
    {
       spinDetect.OnPlanetRotated += RotateAction;
        StartCoroutine(AutoIncrementCoroutine());
        
    }

    void OnDisable()
    {
        spinDetect.OnPlanetRotated -= RotateAction;
    }

    public void RotateAction()
    {
        count += rotatePoints;
        uiManager.UpdateUI();
        
        uiManager.SpawnFloatingText(rotatePoints);

    }

    
    IEnumerator AutoIncrementCoroutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(1f); 
            RotateAction(); 
        }
    }
    public bool PurchaseAction(int cost)
    {
        if(count >= cost)
        {
            count -= cost;
            uiManager.UpdateUI();
            return true;
        }
        return false;
    }
    public void RefreshUI()
    {
        uiManager.UpdateUI();
    }
    public void SetUIManager(ContadorUI newUIManager)
    {
        uiManager = newUIManager;
    }
   
    
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public void ForceIncomeUpdate()
    {
        if (!sceneLoad.planetScene)
        {
            Debug.Log("BOSQUE PUNTOS B");
            IdleCalculate();
        }
        else
        {
            Debug.Log("TIERRA PUNTOS B");
            IdleCalculate2();
        }
        nextTimeCheck = Time.timeSinceLevelLoad + (1f / updatesPerSecond);
    }
    public void RefreshUpgradeManagers()
    {
        upgradeManagers = FindObjectsOfType<UpgradeManager>();
        ForceIncomeUpdate();
    }
    public void RefreshUpgradeManagers2()
    {
        upgradeManagers2 = FindObjectsOfType<UpgradeManager2>();
        ForceIncomeUpdate();
    }

}
