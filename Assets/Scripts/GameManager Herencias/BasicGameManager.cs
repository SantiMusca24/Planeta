using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGameManager : GameManager
{
    public override void ForceIncomeUpdate()
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

    public override float GetIncomePerSecond()
    {
        float sum = 0;
        foreach (var upgradeManager in upgradeManagers)
        {
            sum += upgradeManager.CalculateIncomePerSecond();
        }
        return sum;
    }

    public override void OnEnable()
    {
        spinDetect.OnPlanetRotated += RotateAction;
        StartCoroutine(AutoIncrementCoroutine());
    }

    public override bool PurchaseAction(int cost)
    {
        if (count >= cost)
        {
            count -= cost;
            uiManager.UpdateUI();
            return true;
        }
        return false;
    }

    public override void RefreshUI()
    {
        uiManager.UpdateUI();
    }

    public override void RefreshUpgradeManagers()
    {
        upgradeManagers = FindObjectsOfType<UpgradeManager>();
        ForceIncomeUpdate();
    }

    public override void RefreshUpgradeManagers2()
    {
        upgradeManagers2 = FindObjectsOfType<UpgradeManager2>();
        ForceIncomeUpdate();
    }

    public override void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        count = 0;
        uiManager.UpdateUI();
    }

    public override void RotateAction()
    {
        count += rotatePoints;
        uiManager.UpdateUI();

        uiManager.SpawnFloatingText(rotatePoints);
    }

    public override void SaveProgress()
    {
        PlayerPrefs.SetFloat("Count", count);
        PlayerPrefs.Save();
    }

    public override void SetUIManager(ContadorUI newUIManager)
    {
        uiManager = newUIManager;
    }

    protected override IEnumerator AutoIncrementCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            RotateAction();
        }
    }

    protected override void IdleCalculate()
    {
        float sum = 0;
        foreach (var upgradeManager in upgradeManagers)
        {
            sum += upgradeManager.CalculateIncomePerSecond();
            upgradeManager.UpdateUI();
        }
        Debug.Log("SUMA 1 BOSQUE " + sum);
        count += sum / updatesPerSecond;
        Debug.Log("SUMA 2 BOSQUE " + sum);
        uiManager.UpdateUI();
    }

    protected override void IdleCalculate2()
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

    protected override void OnApplicationQuit()
    {
        SaveProgress();
    }

    protected override void OnDisable()
    {
        spinDetect.OnPlanetRotated -= RotateAction;
    }
}
