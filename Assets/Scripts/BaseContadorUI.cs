using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class BaseContadorUI : MonoBehaviour
{
    [SerializeField] protected TMP_Text countText;
    [SerializeField] protected TMP_Text gemsText;
    //[SerializeField] protected TMP_Text incomePerSecondText;
    //[SerializeField] protected TMP_Text GoalText;
    //[SerializeField] protected int goalPoints = 1000;
    [SerializeField] private TMP_Text doubleIncomeTimerText;

    [SerializeField] protected ScrollRect scrollRectToReset;
    //[SerializeField] protected string upgradeNameToTrack = "Upgrade_1";
    //[SerializeField] protected int goalLevel = 10;
    //[SerializeField] protected TMP_Text upgradeGoalText;
    //[SerializeField] protected TMP_Text GoalText2;
    //[SerializeField] protected TMP_Text upgradeGoalText2;
    //[SerializeField] protected int goalPoints2 = 5000;
    //[SerializeField] protected string upgradeNameToTrack2 = "Upgrade_2";
    //[SerializeField] protected int goalLevel2 = 20;
    //[Header("Feedback Cortes")]
    //[SerializeField] protected TMP_Text perfectoText;
    //[SerializeField] protected TMP_Text regularText;
    //[SerializeField] protected TMP_Text falloText;
    //[SerializeField] protected float feedbackDuration = 0.7f;
    //[Header("Minijuego de madera")]
    //[SerializeField] public TMP_Text cortesText;
    //[SerializeField] public TMP_Text troncosText;
    //[SerializeField] public TMP_Text tiempoText;
    //[SerializeField] public TMP_Text resumenText;
    //[SerializeField] protected GameObject minigamePanel;
    //[SerializeField] protected GameObject resumenPanel;
    //[SerializeField] public TMP_Text cooldownTimerText;

    protected void Start()
    {
        Debug.Log("BASE START");
        Debug.Log("BASE STAART");
        Debug.Log("BASE STAAAART");
        Debug.Log("BASE STAAAAAAAAAAAART");
        UpdateUI();

        if (scrollRectToReset != null)
        {
            scrollRectToReset.verticalNormalizedPosition = 1f;
        }               
    }
    protected virtual void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.doubleIncomeActive)
        {
            doubleIncomeTimerText.gameObject.SetActive(true);
            doubleIncomeTimerText.text = FormatTime(GameManager.Instance.doubleIncomeTimer);
        }
        else
        {
            doubleIncomeTimerText.gameObject.SetActive(false);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public abstract void UpdateUI();
    
    /*public enum CutFeedbackType
    {
        Perfect,
        Regular,
        Fail
    }*/

    //public abstract void ShowCutFeedback(CutFeedbackType type);

    protected abstract IEnumerator ShowTemporaryText(TMP_Text text);
    /*public enum MinigamePanelType
    {
        Minigame,
        Summary
    }*/

    //public abstract void ShowPanel(MinigamePanelType panelType, string resumen = "");
    public abstract void UpdateMinigameUI(int? cortes = null, int? troncos = null, float? tiempo = null, int? cortesNecesarios = null);
    public abstract void OcultarMinigameTextos();
    public abstract void OcultarMinigameTextos2();
}
