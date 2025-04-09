using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("Comoponents")]
    public TMP_Text priceText;
    public TMP_Text incomeInfoText;
    public Button button;
    [Header("Managers")]
    public GameManager gameManager;

    [Header("Values")]
    public int startPrice = 15;
    public float upgradePriceMultiplier;
    public float cookiesPerUpgrade = 0.1f;
    [Header("Settings")]
public string upgradeName;



    int level = 0;


    private void Start()
    {
        if (!string.IsNullOrEmpty(upgradeName))
        {
            level = PlayerPrefs.GetInt(upgradeName + "_Level", 0);
        }

        UpdateUI();
    }

    public void ClickAction() 
    {
        int price = CalculatePrice();
        bool purchaseSuccess = gameManager.PurchaseAction(price);
        if (purchaseSuccess) 
        {
            level++;
            if (!string.IsNullOrEmpty(upgradeName))
            {
                PlayerPrefs.SetInt(upgradeName + "_Level", level);
                PlayerPrefs.Save();
            }
            UpdateUI();
            gameManager.RefreshUI();
            gameManager.ForceIncomeUpdate();
        }

    }

    
    public void UpdateUI()
    {
       priceText.text = CalculatePrice().ToString();
        incomeInfoText.text = level.ToString() + "x" + cookiesPerUpgrade + "/s";

        bool canAfford = gameManager.count >= CalculatePrice();
        button.interactable = canAfford;
    }
    int CalculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, level));
        return price;
    }

    public float CalculateIncomePerSecond()
    {
        return cookiesPerUpgrade * level;
    }
    public void AssignGameManager(GameManager gm)
    {
        gameManager = gm;
        UpdateUI();
    }

}
