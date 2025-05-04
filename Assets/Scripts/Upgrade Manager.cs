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
    public TMP_Text levelText;
    public Button button;
    [Header("Managers")]
    public GameManager gameManager;

    [Header("Values")]
    public int startPrice = 15;
    public float upgradePriceMultiplier;
    public float cookiesPerUpgrade = 0.1f;
    [Header("Settings")]
    public string upgradeName;
    [SerializeField] int levelToChange;
    [Header("Level Unlocks")]
    public List<LevelUnlockObject> unlocks = new List<LevelUnlockObject>();

    int level = 0;

    [System.Serializable]
    public class LevelUnlockObject
    {
        public int requiredLevel;
        public GameObject objectToActivate;
    }
    private void Update()
    {
        switch (levelToChange)
        {
            case 1:
                Debug.Log("UPGRADE MANAGER LEVEL " + level);
                //Debug.Log("FIX LEVEL A " + UpgradeManager2.level1);
                UpgradeManager2.level1 = level;
                //Debug.Log("FIX LEVEL B " + UpgradeManager2.level1);
                break;
            case 2:
                Debug.Log("UPGRADE MANAGER LEVEL b " + level);
                //Debug.Log("FIX LEVEL Ab " + UpgradeManager2.level2);
                UpgradeManager2.level2 = level;
                //Debug.Log("FIX LEVEL Bb " + UpgradeManager2.level2);
                break;
            case 3:
                UpgradeManager2.level3 = level;
                break;
            case 4:
                UpgradeManager2.level4 = level;
                break;
            case 5:
                UpgradeManager2.level5 = level;
                break;
            case 6:
                UpgradeManager2.level6 = level;
                break;

        }

    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {       
        sceneLoad.planetScene = false;
        if (!string.IsNullOrEmpty(upgradeName))
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

        UpdateUI();
    }

    public void ClickAction() 
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

    }
    


    public void UpdateUI()
    {
        if (levelText != null)
            levelText.text = "Level: " + level;
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
    void CheckLevelUnlocks()
    {
        foreach (var unlock in unlocks)
        {
            if (level >= unlock.requiredLevel && unlock.objectToActivate != null && !unlock.objectToActivate.activeSelf)
            {
                unlock.objectToActivate.SetActive(true);
            }
        }
    }
}
