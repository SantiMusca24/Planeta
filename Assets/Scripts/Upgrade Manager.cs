using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeManager : MonoBehaviour
{
    [Header("Comoponents")]
    public TMP_Text priceText;
    public TMP_Text incomeInfoText;
    public TMP_Text levelText;
    public Button button;
    [Header("Managers")]
    public GameManager gameManager;
    public AudioManager audioManager;

    [Header("Values")]
    public int startPrice = 15;
    public float upgradePriceMultiplier;
    public float cookiesPerUpgrade = 0.1f;
    [Header("Settings")]
    public string upgradeName;
    [SerializeField] int levelToChange;
    [Header("Level Unlocks")]
    public List<LevelUnlockObject> unlocks = new List<LevelUnlockObject>();

    protected int level = 0;

    [System.Serializable]
    public class LevelUnlockObject
    {
        public int requiredLevel;
        public GameObject objectToActivate;
    }
    protected virtual void Update()
    {
        if (levelToChange == 1)
        {
            UpgradeManager2.level1 = level;
        }
        else if (levelToChange == 2)
        {
            UpgradeManager2.level2 = level;
        }
        else if (levelToChange == 3)
        {
            UpgradeManager2.level3 = level;
        }
        else if (levelToChange == 4)
        {
            UpgradeManager2.level4 = level;
        }
        else if (levelToChange == 5)
        {
            UpgradeManager2.level5 = level;
        }
        else if (levelToChange == 6)
        {
            UpgradeManager2.level6 = level;
        }
    }
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    protected virtual void Start()
    {
        sceneLoad.planetScene = false;
        if (!string.IsNullOrEmpty(upgradeName))
        {
            level = PlayerPrefs.GetInt(upgradeName + "_Level", 0);
        }
        audioManager = FindObjectOfType<AudioManager>();

        new UpgradeBuilder()
        .WithUnlocks(unlocks)
        .AtLevel(level)
        .Build();

        UpdateUI();
    }

    public abstract void ClickAction();
    public abstract void UpdateUI();
    protected abstract int CalculatePrice();
    public abstract float CalculateIncomePerSecond();
    public abstract void AssignGameManager(GameManager gm);
    public abstract void CheckLevelUnlocks();
}
