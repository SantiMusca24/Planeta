using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    public float count = 0;
    float nextTimeCheck = 1;

    public static GameManager Instance;
    [SerializeField] private ContadorUI uiManager;
    [SerializeField] UpgradeManager[] upgradeManagers;
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
            IdleCalculate();
            nextTimeCheck = Time.timeSinceLevelLoad + (1f / updatesPerSecond);
             
        }
    }
    void IdleCalculate()
    {
        float sum = 0;
        foreach (var upgradeManager in upgradeManagers)
        {
            sum += upgradeManager.CalculateIncomePerSecond();
            upgradeManager.UpdateUI();
        }
        count += sum /updatesPerSecond;
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
        RefreshUpgradeManagers();
    }

    void OnDisable()
    {
        spinDetect.OnPlanetRotated -= RotateAction;
    }

    public void RotateAction()
    {
        count++;
        uiManager.UpdateUI();
        
        uiManager.SpawnFloatingText(1);

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
        IdleCalculate();
        nextTimeCheck = Time.timeSinceLevelLoad + (1f / updatesPerSecond);
    }
    public void RefreshUpgradeManagers()
    {
        upgradeManagers = FindObjectsOfType<UpgradeManager>();

        foreach (var upgrade in upgradeManagers)
        {
            upgrade.AssignGameManager(this);
        }

        ForceIncomeUpdate();
    }

}
