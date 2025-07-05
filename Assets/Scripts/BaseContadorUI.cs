using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class BaseContadorUI : MonoBehaviour
{
    [SerializeField] protected TMP_Text countText;
    [SerializeField] protected TMP_Text gemsText;
    [SerializeField] protected TMP_Text incomePerSecondText;
    [SerializeField] protected TMP_Text GoalText;
    [SerializeField] protected int goalPoints = 1000;
    

    [SerializeField] protected ScrollRect scrollRectToReset;
    [SerializeField] protected string upgradeNameToTrack = "Upgrade_1";
    [SerializeField] protected int goalLevel = 10;
    [SerializeField] protected TMP_Text upgradeGoalText;
    [SerializeField] protected TMP_Text GoalText2;
    [SerializeField] protected TMP_Text upgradeGoalText2;
    [SerializeField] protected int goalPoints2 = 5000;
    [SerializeField] protected string upgradeNameToTrack2 = "Upgrade_2";
    [SerializeField] protected int goalLevel2 = 20;
    [Header("Feedback Cortes")]
    [SerializeField] protected TMP_Text perfectoText;
    [SerializeField] protected TMP_Text regularText;
    [SerializeField] protected TMP_Text falloText;
    [SerializeField] protected float feedbackDuration = 0.7f;
    [Header("Minijuego de madera")]
    [SerializeField] public TMP_Text cortesText;
    [SerializeField] public TMP_Text troncosText;
    [SerializeField] public TMP_Text tiempoText;
    [SerializeField] public TMP_Text resumenText;
    [SerializeField] protected GameObject minigamePanel;
    [SerializeField] protected GameObject resumenPanel;
    [SerializeField] public TMP_Text cooldownTimerText;

    void Start()
    {
        UpdateUI();

        if (scrollRectToReset != null)
        {
            scrollRectToReset.verticalNormalizedPosition = 1f;
        }

        if (GoalText2 != null) GoalText2.gameObject.SetActive(false);
        if (upgradeGoalText2 != null) upgradeGoalText2.gameObject.SetActive(false);
        if (perfectoText != null) perfectoText.gameObject.SetActive(false);
        if (regularText != null) regularText.gameObject.SetActive(false);
        if (falloText != null) falloText.gameObject.SetActive(false);
        if (falloText != null) falloText.gameObject.SetActive(false);
        if (tiempoText != null) tiempoText.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        if (countText != null)
        {
            double currentPoints = GameManager.Instance.count;
            countText.text = AbreviateNumber.Format(currentPoints);

            if (GoalText != null)
            {
                GoalText.text = "Puntos: " + AbreviateNumber.Format(currentPoints) + " / " + AbreviateNumber.Format(goalPoints);

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
        if (gemsText != null)
        {
            double currentPoints = GameManager.Instance.gems;
            gemsText.text = AbreviateNumber.Format(currentPoints);
        }
        if (incomePerSecondText != null)
        {
            float incomePerSecond = GameManager.Instance.GetIncomePerSecond();
            incomePerSecondText.text = AbreviateNumber.Format(incomePerSecond) + " /s";
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
                GoalText2.text = AbreviateNumber.Format(currentPoints) + " / " + AbreviateNumber.Format(goalPoints2);
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
    
    public enum CutFeedbackType
    {
        Perfect,
        Regular,
        Fail
    }

    public void ShowCutFeedback(CutFeedbackType type)
    {
        TMP_Text feedbackText = null;

        switch (type)
        {
            case CutFeedbackType.Perfect:
                feedbackText = perfectoText;
                break;
            case CutFeedbackType.Regular:
                feedbackText = regularText;
                break;
            case CutFeedbackType.Fail:
                feedbackText = falloText;
                break;
        }

        if (feedbackText != null)
            StartCoroutine(ShowTemporaryText(feedbackText));
    }

    protected IEnumerator ShowTemporaryText(TMP_Text text)
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(feedbackDuration);
        text.gameObject.SetActive(false);
    }
    public enum MinigamePanelType
    {
        Minigame,
        Summary
    }

    public abstract void ShowPanel(MinigamePanelType panelType, string resumen = "");
    public abstract void UpdateMinigameUI(int? cortes = null, int? troncos = null, float? tiempo = null, int? cortesNecesarios = null);
    public abstract void OcultarMinigameTextos();
    public abstract void OcultarMinigameTextos2();
}
