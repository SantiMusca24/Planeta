using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ContadorUI;

public class WoodMiniGame : MonoBehaviour
{
    public GameObject minigamePanel;
    public Slider precisionSlider;
    public float sliderSpeed = 2f;
    public int cutsNeededPerLog = 5;

    private bool increasing = true;
    private int currentCuts = 0;
    private int logsCut = 0;
    private float gameTimer = 5f;
    private float timer;
    private WoodcutPhase phase = WoodcutPhase.Inactive;
    
    public GameObject summaryPanel;
    public GameObject botton;
    public GameObject bottonInicio;

    public ContadorUI uiManager;
    
    private void Start()
    {
       minigamePanel.SetActive(false);
       precisionSlider.gameObject.SetActive(false);
       botton.SetActive(false);

    }
    void Update()
    {
        if (phase == WoodcutPhase.Cutting)
        {
            UpdateSlider();
            timer -= Time.deltaTime;
            uiManager?.UpdateMinigameUI(tiempo: timer);

            if (timer <= 0)
                EndMinigame();

            if (Input.GetKeyDown(KeyCode.W))
                AttemptCut();
        }
    }

    public void StartMinigame()
    {
        bottonInicio.SetActive(false);
        precisionSlider.gameObject.SetActive(true);
        botton.SetActive(true);
        currentCuts = 0;
        logsCut = 0;
        timer = gameTimer;
        sliderSpeed = 2f;
        phase = WoodcutPhase.Cutting;

        uiManager?.ShowPanel(MinigamePanelType.Minigame);
        uiManager?.UpdateMinigameUI(
            cortes: currentCuts,
            cortesNecesarios: cutsNeededPerLog,
            troncos: logsCut,
            tiempo: timer);

        if (uiManager?.cortesText != null)
            uiManager.cortesText.gameObject.SetActive(true);

        if (uiManager?.troncosText != null)
            uiManager.troncosText.gameObject.SetActive(true);

        if (uiManager?.tiempoText != null)
            uiManager.tiempoText.gameObject.SetActive(true);

        
    }

    void UpdateSlider()
    {
        if (increasing)
            precisionSlider.value += Time.deltaTime * sliderSpeed;
        else
            precisionSlider.value -= Time.deltaTime * sliderSpeed;

        if (precisionSlider.value >= 1f) increasing = false;
        if (precisionSlider.value <= 0f) increasing = true;
    }

    public void AttemptCut()
    {
        if (phase != WoodcutPhase.Cutting) return;

        float val = precisionSlider.value;

        if (Mathf.Abs(val - 0.55f) < 0.15f)
        {
            currentCuts += 2;
            uiManager?.ShowCutFeedback(CutFeedbackType.Perfect);
        }
        else if (Mathf.Abs(val - 0.55f) < 0.30f)
        {
            currentCuts += 1;
            uiManager?.ShowCutFeedback(CutFeedbackType.Regular);
        }
        else
        {
            uiManager?.ShowCutFeedback(CutFeedbackType.Fail);
        }

        if (currentCuts >= cutsNeededPerLog)
        {
            logsCut++;
            currentCuts = 0;
            sliderSpeed *= 1.25f;
        }
        
        uiManager?.UpdateMinigameUI(cortes: currentCuts, cortesNecesarios: cutsNeededPerLog);
        uiManager?.UpdateMinigameUI(troncos: logsCut);

    }

    void EndMinigame()
    {
        phase = WoodcutPhase.Summary;
        
        precisionSlider.gameObject.SetActive(false);
        
        botton.SetActive(false);
        
        float incomePerSecond = GameManager.Instance.GetIncomePerSecond();
        
        int maderaGanada = Mathf.RoundToInt(logsCut * 100 * incomePerSecond);

        GameManager.Instance.count += maderaGanada;

        string resumen = $"¡Minijuego terminado!\n" +
                         $"Troncos cortados: {logsCut}\n" +
                         $"Madera obtenida: {maderaGanada}";

        if (uiManager?.resumenText != null)
            uiManager.resumenText.gameObject.SetActive(true);
        
        bottonInicio.SetActive(true);
        
        bottonInicio.GetComponent<Button>().interactable = false;

        
        StartCoroutine(ReactivarBotonInicioDespuesDeCooldown(10f));
        
        uiManager?.ShowPanel(MinigamePanelType.Summary, resumen);
        
        StartCoroutine(OcultarTextos());
    }
    private IEnumerator ReactivarBotonInicioDespuesDeCooldown(float segundos)
    {

        float t = segundos;

        if (uiManager != null && uiManager.cooldownTimerText != null)
            uiManager.cooldownTimerText.gameObject.SetActive(true);

        while (t > 0)
        {
            if (uiManager != null && uiManager.cooldownTimerText != null)
                uiManager.cooldownTimerText.text = "Espera: " + Mathf.CeilToInt(t).ToString() + "s";

            yield return new WaitForSeconds(1f);
            t -= 1f;
        }

        if (bottonInicio != null)
            bottonInicio.GetComponent<Button>().interactable = true;

        if (uiManager != null && uiManager.cooldownTimerText != null)
            uiManager.cooldownTimerText.gameObject.SetActive(false);
    }
    private IEnumerator OcultarTextos()
    {
        yield return new WaitForSeconds(5f);

        uiManager?.OcultarMinigameTextos();
    }
    public enum WoodcutPhase
    {
        Inactive,
        Starting,
        Cutting,
        BetweenLogs,
        Summary
    }
}
