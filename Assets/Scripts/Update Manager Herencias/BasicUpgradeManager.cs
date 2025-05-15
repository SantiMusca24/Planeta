using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUpgradeManager : UpgradeManager
{

    public override void ClickAction()
    {
        int price = CalculatePrice();
        bool purchaseSuccess = gameManager.PurchaseAction(price);
        if (purchaseSuccess)
        {
            level++;
            GameManager.rotatePoints += level + 1;
            CheckLevelUnlocks();
            if (!string.IsNullOrEmpty(upgradeName))
            {
                PlayerPrefs.SetInt(upgradeName + "_Level", level);
                PlayerPrefs.Save();
            }
            UpdateUI();
            gameManager.RefreshUI();
        }
    }

    public override void UpdateUI()
    {
        if (levelText != null)
            levelText.text = "Level: " + level;
        priceText.text = CalculatePrice().ToString();
        incomeInfoText.text = level.ToString() + "x" + cookiesPerUpgrade + "/s";

        bool canAfford = gameManager.count >= CalculatePrice();
        button.interactable = canAfford;
    }

    public override float CalculateIncomePerSecond()
    {
        return cookiesPerUpgrade * level;
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

}
