using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PescaUI : BaseContadorUI
{
    [SerializeField] protected TMP_Text incomePerSecondText;
    [Header("Feedback Cortes")]
    [SerializeField] protected TMP_Text perfectoText;
    [SerializeField] protected TMP_Text regularText;
    [SerializeField] protected TMP_Text falloText;
    [SerializeField] protected float feedbackDuration = 0.7f;
    [Header("Minijuego de madera")]
    [SerializeField] public TMP_Text cortesText;
    [SerializeField] public TMP_Text cortesText2;
    [SerializeField] public TMP_Text troncosText;
    [SerializeField] public TMP_Text troncosText2;
    [SerializeField] public TMP_Text tiempoText;
    [SerializeField] public TMP_Text resumenText;
    [SerializeField] public TMP_Text resumenText2;
    [SerializeField] protected GameObject minigamePanel;
    [SerializeField] protected GameObject resumenPanel;
    [SerializeField] public TMP_Text cooldownTimerText;
    [SerializeField] public GameObject lago1;
    [SerializeField] public GameObject lago2;
    [SerializeField] public GameObject lago3;
    [SerializeField] public GameObject lago4;
    [SerializeField] public GameObject cartel1;
    [SerializeField] public GameObject cartel2;
    [SerializeField] public GameObject cartel3;
    [SerializeField] public GameObject cartelResumen;
    new void Start()
    {
        base.Start();
        if (perfectoText != null) perfectoText.gameObject.SetActive(false);
        if (regularText != null) regularText.gameObject.SetActive(false);
        if (falloText != null) falloText.gameObject.SetActive(false);
        if (falloText != null) falloText.gameObject.SetActive(false);
        if (tiempoText != null) tiempoText.gameObject.SetActive(false);
        if (cortesText2 != null) cortesText2.gameObject.SetActive(false);
        if (troncosText2 != null) troncosText2.gameObject.SetActive(false);
        if (resumenText != null) resumenText2.gameObject.SetActive(false);
        if (lago1 != null) lago1.gameObject.SetActive(false);
        if (lago2 != null) lago2.gameObject.SetActive(false);
        if (lago3 != null) lago3.gameObject.SetActive(false);
        if (lago4 != null) lago4.gameObject.SetActive(false);
        if (cartel1 != null) cartel1.gameObject.SetActive(false);
        if (cartel2 != null) cartel2.gameObject.SetActive(false);
        if (cartel3 != null) cartel3.gameObject.SetActive(false);
        if(cartelResumen!= null) cartelResumen.gameObject.SetActive(false);
    }
    public override void OcultarMinigameTextos()
    {
        if (resumenText != null) resumenText.gameObject.SetActive(false);
        resumenText2.gameObject.SetActive(false);
        if (cartelResumen != null) cartelResumen.gameObject.SetActive(false);
    }
    public override void OcultarMinigameTextos2()
    {
        if (tiempoText != null) tiempoText.gameObject.SetActive(false);
        if (cortesText != null) cortesText.gameObject.SetActive(false);
        if (troncosText != null) troncosText.gameObject.SetActive(false);
        lago1.gameObject.SetActive(false);
        lago2.gameObject.SetActive(false);
        lago3.gameObject.SetActive(false);
        lago4.gameObject.SetActive(false);
        if (cartel1 != null) cartel1.gameObject.SetActive(false);
        if (cartel2 != null) cartel2.gameObject.SetActive(false);
        if (cartel3 != null) cartel3.gameObject.SetActive(false);
    }
    public enum MinigamePanelType
    {
        Minigame,
        Summary
    }
    public void ShowPanel(MinigamePanelType panelType, string resumen = "")
    {
        if (minigamePanel != null) minigamePanel.SetActive(panelType == MinigamePanelType.Minigame);
        if (resumenPanel != null) resumenPanel.SetActive(panelType == MinigamePanelType.Summary);
        cortesText2.gameObject.SetActive(false);
        troncosText2.gameObject.SetActive(false);
        if (panelType == MinigamePanelType.Summary && resumenText != null)
        {
            resumenText.text = resumen;
            resumenText2.gameObject.SetActive(true);
            cartelResumen.gameObject.SetActive(true);
        }
    }
    public override void UpdateMinigameUI(int? cortes = null, int? troncos = null, float? tiempo = null, int? cortesNecesarios = null)
    {
        if (cortes.HasValue && cortesNecesarios.HasValue && cortesText != null)
        {
            cortesText.text = $"{cortes}/{cortesNecesarios}";
            cortesText2.gameObject.SetActive(true);
            troncosText2.gameObject.SetActive(true);
        }
            

        if (troncos.HasValue && troncosText != null)
            troncosText.text = $"{troncos}";

        if (tiempo.HasValue && tiempoText != null)
            tiempoText.text = $" {tiempo.Value:F1}s";
        lago1.gameObject.SetActive(true);
        lago2.gameObject.SetActive(true);
        lago3.gameObject.SetActive(true);
        lago4.gameObject.SetActive(true);
        cartel1.gameObject.SetActive(true);
        cartel2.gameObject.SetActive(true);
        cartel3.gameObject.SetActive(true);
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
    protected override IEnumerator ShowTemporaryText(TMP_Text text)
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(feedbackDuration);
        text.gameObject.SetActive(false);
    }
    public override void UpdateUI()
    {
        if (countText != null)
        {
            double currentPoints = GameManager.Instance.count;
            countText.text = AbreviateNumber.Format(currentPoints);
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
    }
}
