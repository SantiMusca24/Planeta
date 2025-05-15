using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUpgradeManager2 : UpgradeManager2
{
    public override void ClickAction()
    {
        int price = CalculatePrice();
        bool purchaseSuccess = gameManager.PurchaseAction(price);
        if (purchaseSuccess)
        {
            level1++;
            CheckLevelUnlocks();
            if (!string.IsNullOrEmpty(upgradeName))
            {
                PlayerPrefs.SetInt(upgradeName + "_Level", level1);
                PlayerPrefs.Save();
            }
            gameManager.RefreshUI();

        }
    }
    protected override int CalculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, level1));
        return price;
    }

    public override float CalculateIncomePerSecond()
    {
        float total = 0;
        if (thisLevel == 1)
        {
            total = cookiesPerUpgrade * level1;
            //Debug.Log("STEP 3 LEVEL " + level1 + " TOTAL: " + total);
            //return cookiesPerUpgrade * level1;
        }
        else if (thisLevel == 2)
        {
            total = cookiesPerUpgrade * level2;
            //Debug.Log("STEP 3a LEVEL " + level2 + " TOTAL: " + total);
            //return cookiesPerUpgrade * level2;
        }
        else if (thisLevel == 3)
        {
            total = cookiesPerUpgrade * level3;
            //return cookiesPerUpgrade * level3;
        }
        else if (thisLevel == 4)
        {
            total = cookiesPerUpgrade * level4;
            //return cookiesPerUpgrade * level4;
        }
        else if (thisLevel == 5)
        {
            total = cookiesPerUpgrade * level5;
            //return cookiesPerUpgrade * level5;
        }
        else if (thisLevel == 6)
        {
            total = cookiesPerUpgrade * level6;
            //return cookiesPerUpgrade * level6;
        }
        else
        {
            total = 0;
            //return cookiesPerUpgrade * 0;
        }
        return total;
    }

    public override void AssignGameManager(GameManager gm)
    {
        Debug.Log("STEP 0");
        gameManager = gm;
    }

}
