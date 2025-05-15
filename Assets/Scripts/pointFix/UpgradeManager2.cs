using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeManager2 : MonoBehaviour
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

    [SerializeField] protected int thisLevel;

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

    public abstract void ClickAction();
    protected abstract int CalculatePrice();
    public abstract float CalculateIncomePerSecond();
    public abstract void AssignGameManager(GameManager gm);
    protected void CheckLevelUnlocks()
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
