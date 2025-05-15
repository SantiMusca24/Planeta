using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WoodMiniGame : MonoBehaviour
{
    public GameObject minigamePanel;
    public Slider precisionSlider;
    public float sliderSpeed = 2f;
    public int cutsNeededPerLog = 5;

    private bool increasing = true;
    private int currentCuts = 0;
    private int logsCut = 0;
    private float gameTimer = 20f;
    private float timer;
    private WoodcutPhase phase = WoodcutPhase.Inactive;
    
    public GameObject summaryPanel;
    
    public ContadorUI uiManager;
    
    private void Start()
    {
       minigamePanel.SetActive(false);

    }
    void Update()
    {
        if (phase == WoodcutPhase.Cutting)
        {
            UpdateSlider();
            timer -= Time.deltaTime;
            uiManager?.UpdateTimerText(timer);

            if (timer <= 0)
                EndMinigame();

            if (Input.GetKeyDown(KeyCode.W))
                AttemptCut();
        }
    }

    public void StartMinigame()
    {
        currentCuts = 0;
        logsCut = 0;
        timer = gameTimer;
        sliderSpeed = 2f;
        phase = WoodcutPhase.Cutting;

        uiManager?.ShowMinigamePanel();
        uiManager?.UpdateCutsText(currentCuts, cutsNeededPerLog);
        uiManager?.UpdateLogsText(logsCut);
        uiManager?.UpdateTimerText(timer);
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
            uiManager?.ShowPerfectCutFeedback();
        }
        else if (Mathf.Abs(val - 0.55f) < 0.30f)
        {
            currentCuts += 1;
            uiManager?.ShowRegularCutFeedback();
        }
        else
        {
            uiManager?.ShowFailCutFeedback();
        }

        if (currentCuts >= cutsNeededPerLog)
        {
            logsCut++;
            currentCuts = 0;
            sliderSpeed *= 1.25f;
        }

        uiManager?.UpdateCutsText(currentCuts, cutsNeededPerLog);
        uiManager?.UpdateLogsText(logsCut);

    }

    void EndMinigame()
    {
        phase = WoodcutPhase.Summary;

        float incomePerSecond = GameManager.Instance.GetIncomePerSecond();
        int maderaGanada = Mathf.RoundToInt(logsCut * 100 * incomePerSecond);

        GameManager.Instance.count += maderaGanada;

        string resumen = $"¡Minijuego terminado!\n" +
                         $"Troncos cortados: {logsCut}\n" +
                         $"Madera obtenida: {maderaGanada}";

        uiManager?.ShowSummaryPanel(resumen);
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
