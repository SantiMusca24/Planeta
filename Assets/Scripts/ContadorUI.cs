using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContadorUI : MonoBehaviour
{
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text incomePerSecondText;
    [SerializeField] TMP_Text GoalText;
    [SerializeField] private int goalPoints = 1000;
    [Header("Floating Text")]
    [SerializeField] Transform floatingTextContainer;
    [SerializeField] TMP_Text floatingTextPrefab;

    [SerializeField] ScrollRect scrollRectToReset;
    [SerializeField] private string upgradeNameToTrack = "Upgrade_1";
    [SerializeField] private int goalLevel = 10;
    [SerializeField] private TMP_Text upgradeGoalText;
    [SerializeField] private TMP_Text GoalText2;
    [SerializeField] private TMP_Text upgradeGoalText2;
    [SerializeField] private int goalPoints2 = 5000;
    [SerializeField] private string upgradeNameToTrack2 = "Upgrade_2";
    [SerializeField] private int goalLevel2 = 20;

    void Start()
    {
        UpdateUI();

        if (scrollRectToReset != null)
        {
            scrollRectToReset.verticalNormalizedPosition = 1f;
        }

        if (GoalText2 != null) GoalText2.gameObject.SetActive(false);
        if (upgradeGoalText2 != null) upgradeGoalText2.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        if (countText != null)
        {
            int currentPoints = Mathf.RoundToInt(GameManager.Instance.count);
            countText.text = currentPoints.ToString();

            if (GoalText != null)
            {
                GoalText.text = "Puntos: " + currentPoints + " / " + goalPoints;

                if (currentPoints >= goalPoints)
                {
                    GoalText.color = Color.green;
                }
                else
                {
                    GoalText.color = Color.white;
                }
            }
        }

        if (incomePerSecondText != null)
        {
            float incomePerSecond = GameManager.Instance.GetIncomePerSecond();
            incomePerSecondText.text = Mathf.Round(incomePerSecond).ToString() + " /s";
        }

        
        if (!string.IsNullOrEmpty(upgradeNameToTrack) && upgradeGoalText != null)
        {
            int currentLevel = PlayerPrefs.GetInt(upgradeNameToTrack + "_Level", 0);
            upgradeGoalText.text = "Mejora Arboles: " + upgradeNameToTrack + " nivel " + currentLevel + " / " + goalLevel;

            if (currentLevel >= goalLevel)
            {
                upgradeGoalText.color = Color.green;
            }
            else
            {
                upgradeGoalText.color = Color.white;
            }
        }
        bool firstGoalCompleted = false;
        bool firstUpgradeCompleted = false;

        if (GoalText != null && GoalText.color == Color.green)
            firstGoalCompleted = true;

        if (upgradeGoalText != null && upgradeGoalText.color == Color.green)
            firstUpgradeCompleted = true;

        if (firstGoalCompleted && firstUpgradeCompleted)
        {
            
            if (GoalText != null) GoalText.gameObject.SetActive(false);
            if (upgradeGoalText != null) upgradeGoalText.gameObject.SetActive(false);

            
            if (GoalText2 != null)
            {
                GoalText2.gameObject.SetActive(true);
                int currentPoints = Mathf.RoundToInt(GameManager.Instance.count);
                GoalText2.text = "Puntos: " + currentPoints + " / " + goalPoints2;
                GoalText2.color = (currentPoints >= goalPoints2) ? Color.green : Color.white;
            }

            if (upgradeGoalText2 != null)
            {
                upgradeGoalText2.gameObject.SetActive(true);
                int currentLevel2 = PlayerPrefs.GetInt(upgradeNameToTrack2 + "_Level", 0);
                upgradeGoalText2.text = "Mejora Árboles: " + upgradeNameToTrack2 + " nivel " + currentLevel2 + " / " + goalLevel2;
                upgradeGoalText2.color = (currentLevel2 >= goalLevel2) ? Color.green : Color.white;
            }
        }
    }
    public void SpawnFloatingText(int amount)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0f);

        TMP_Text floatingText = Instantiate(floatingTextPrefab, floatingTextContainer);

        
        floatingText.rectTransform.anchoredPosition = randomOffset;

       
        floatingText.text = "+" + amount.ToString();
        floatingText.color = Color.green;

        
        Animator animator = floatingText.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("pop up");
        }

        Destroy(floatingText.gameObject, 1f);
    }
}
