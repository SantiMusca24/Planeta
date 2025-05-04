using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class upgradeFix : MonoBehaviour
{
    //[Header("Comoponents")]
    //public TMP_Text priceText;
    //public TMP_Text incomeInfoText;
    //public TMP_Text levelText;
    //public Button button;
    //[Header("Managers")]
    //public GameManager gameManager;

    //[Header("Values")]
    //public int startPrice = 15;
    //public float upgradePriceMultiplier;
    public float cookiesPerUpgrade = 0.1f;
    //[Header("Settings")]
    //public string upgradeName;

    //[Header("Level Unlocks")]
    //public List<LevelUnlockObject> unlocks = new List<LevelUnlockObject>();

    static public int level = 0;

    /*[System.Serializable]
    public class LevelUnlockObject
    {
        public int requiredLevel;
        public GameObject objectToActivate;
    }*/
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        //level = 1;
        /*if (!string.IsNullOrEmpty(upgradeName))
        {
            level = PlayerPrefs.GetInt(upgradeName + "_Level", 0);
        }


        foreach (var unlock in unlocks)
        {
            if (unlock.objectToActivate != null)
            {
                unlock.objectToActivate.SetActive(level >= unlock.requiredLevel);
            }
        }

        UpdateUI();*/
    }

    /* public void ClickAction() 
     {
         int price = CalculatePrice();
         bool purchaseSuccess = gameManager.PurchaseAction(price);
         if (purchaseSuccess) 
         {
             level++;
             CheckLevelUnlocks();
             if (!string.IsNullOrEmpty(upgradeName))
             {
                 PlayerPrefs.SetInt(upgradeName + "_Level", level);
                 PlayerPrefs.Save();
             }
             UpdateUI();
             gameManager.RefreshUI();

         }

     }*/



    /*public void UpdateUI()
    {
        if (levelText != null)
            levelText.text = "Level: " + level;
        priceText.text = CalculatePrice().ToString();
        incomeInfoText.text = level.ToString() + "x" + cookiesPerUpgrade + "/s";

        bool canAfford = gameManager.count >= CalculatePrice();
        button.interactable = canAfford;
    }*/
    /*int CalculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, level));
        return price;
    }*/

    public float CalculateIncomePerSecond()
    {
        Debug.Log("UPGRADE FIX MAGIC");
        return cookiesPerUpgrade * level;
    }
    /*public void AssignGameManager(GameManager gm)
    {
        gameManager = gm;
        UpdateUI();
    }*/
    /*void CheckLevelUnlocks()
    {
        foreach (var unlock in unlocks)
        {
            if (level >= unlock.requiredLevel && unlock.objectToActivate != null && !unlock.objectToActivate.activeSelf)
            {
                unlock.objectToActivate.SetActive(true);
            }
        }
    }*/
}
