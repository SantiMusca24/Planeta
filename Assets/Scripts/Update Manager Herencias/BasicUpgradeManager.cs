using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUpgradeManager : UpgradeManager
{
    static public int levelForest;
    public int levelForestRedundant;
    [SerializeField] forestUpgrade _forestUpgrade;

    /*public BasicUpgradeManager(upgradeBase upgrd) : base(upgrd)
    {
    }*/
    new void Start()
    {
        base.Start();
        levelForest = level;
        levelForestRedundant = level;
        Debug.Log("FOREST LEVEL AAAAAA: " + levelForest);
    }
    new void Update()
    {
        base.Update();
        levelForest = level;
        levelForestRedundant = level;
        Debug.Log("level: " + level + ". rotatePoints: " + GameManager.rotatePoints);
        Debug.Log("FOREST LEVEL AAAAAA: " + levelForest);
    }
    public override void ClickAction()
    {
        int price = CalculatePrice();
        bool purchaseSuccess = gameManager.PurchaseAction(price);
        if (purchaseSuccess)
        {
            levelPublic = level;
            if (audioManager != null)
                audioManager.Play("Coin");
            level++;
            CheckLevelUnlocks();
            if (!string.IsNullOrEmpty(upgradeName))
            {
                PlayerPrefs.SetInt(upgradeName + "_Level", level);
                PlayerPrefs.Save();
            }
            UpdateUI();
            gameManager.RefreshUI();
            _forestUpgrade.CountStart();
        }
    }

    public override void UpdateUI()
    {
        if (levelText != null)
            levelText.text = "" + level;
        priceText.text = CalculatePrice().ToString();
        incomeInfoText.text = level.ToString() + "x" + cookiesPerUpgrade + "/s";

        bool canAfford = gameManager.count >= CalculatePrice();
        button.interactable = canAfford;
    }

    public override float CalculateIncomePerSecond()
    {
        if (levelToChange != 3) return cookiesPerUpgrade * level * _forestUpgrade.incTotal;
        else return 0;

    }

    public override void CheckLevelUnlocks()
    {
        Debug.Log("BOCA");
        new UpgradeBuilder()
        .WithUnlocks(unlocks)
         .AtLevel(level)
         .Build();
    }

    protected override int CalculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, level));
        return price;
    }

    public override void AssignGameManager(GameManager gm)
    {
        gameManager = gm;
        UpdateUI();
    }

    /*public override float Inc()
    {
        Debug.Log("LEVEL FOREST: " + levelForest);
        return _upgradeBase.Inc() + levelForest;
    }*/
}
