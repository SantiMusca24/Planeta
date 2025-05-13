using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager2 : MonoBehaviour
{
    [Header("Managers")]
    public GameManager gameManager;

    [Header("Values")]
    public int startPrice = 15;
    public float upgradePriceMultiplier;
    public float cookiesPerUpgrade = 0.1f;
    [Header("Settings")]
    public string upgradeName;

    [Header("Level Unlocks")]
    //public List<LevelUnlockObject> unlocks = new List<LevelUnlockObject>();

    [SerializeField] int thisLevel;

    static public int level1 = 0, level2 = 0, level3 = 0, level4 = 0, level5 = 0, level6 = 0;

    [System.Serializable]
    public class LevelUnlockObject
    {
        public int requiredLevel;
        public GameObject objectToActivate;
    }
    private void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        if (!string.IsNullOrEmpty(upgradeName))
        {
            level1 = PlayerPrefs.GetInt(upgradeName + "_Level", 0);
        }

        
        /*foreach (var unlock in unlocks)
        {
            if (unlock.objectToActivate != null)
            {
                unlock.objectToActivate.SetActive(level1 >= unlock.requiredLevel);
            }
        }*/
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ClickAction() 
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
    
    int CalculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, level1));
        return price;
    }

    public float CalculateIncomePerSecond()
    {

        if (thisLevel == 1)
        {
            //total = cookiesPerUpgrade * level1;
            //Debug.Log("STEP 3 LEVEL " + level1 + " TOTAL: " + total);
            return cookiesPerUpgrade * level1;
        }
        else if (thisLevel == 2)
        {
            //total = cookiesPerUpgrade * level2;
            //Debug.Log("STEP 3a LEVEL " + level2 + " TOTAL: " + total);
            return cookiesPerUpgrade * level2;
        }
        else if (thisLevel == 3)
        {
            //total = cookiesPerUpgrade * level3;
            return cookiesPerUpgrade * level3;
        }
        else if (thisLevel == 4)
        {
            //total = cookiesPerUpgrade * level4;
            return cookiesPerUpgrade * level4;
        }
        else if (thisLevel == 5)
        {
            //total = cookiesPerUpgrade * level5;
            return cookiesPerUpgrade * level5;
        }
        else if (thisLevel == 6)
        {
            //total = cookiesPerUpgrade * level6;
            return cookiesPerUpgrade * level6;
        }
        else
        {
            return cookiesPerUpgrade * 0;
        }

        /*
            //float total = 0;
            switch (thisLevel)
            {
                case 1:
                    //total = cookiesPerUpgrade * level1;
                    //Debug.Log("STEP 3 LEVEL " + level1 + " TOTAL: " + total);
                    return cookiesPerUpgrade * level1;
                case 2:
                    //total = cookiesPerUpgrade * level2;
                    //Debug.Log("STEP 3a LEVEL " + level2 + " TOTAL: " + total);
                    return cookiesPerUpgrade * level2;
                case 3:
                    //total = cookiesPerUpgrade * level3;
                    return cookiesPerUpgrade * level3;
                case 4:
                    //total = cookiesPerUpgrade * level4;
                    return cookiesPerUpgrade * level4;
                case 5:
                    //total = cookiesPerUpgrade * level5;
                    return cookiesPerUpgrade * level5;
                case 6:
                    //total = cookiesPerUpgrade * level6;
                    return cookiesPerUpgrade * level6;
                default:
                    return cookiesPerUpgrade * 0;
            }*/
    }
    public void AssignGameManager(GameManager gm)
    {
        Debug.Log("STEP 0");
        gameManager = gm;
    }
    void CheckLevelUnlocks()
    {
        /*foreach (var unlock in unlocks)
        {
            if (level1 >= unlock.requiredLevel && unlock.objectToActivate != null && !unlock.objectToActivate.activeSelf)
            {
                unlock.objectToActivate.SetActive(true);
            }
        }*/
    }
}
