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
    public TMP_Text cutsText;
    public TMP_Text logsText;
    public TMP_Text timerText;
    public GameObject summaryPanel;
    public TMP_Text summaryText;
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
            timerText.text = $"Tiempo: {timer:F1}s"; 
            if (timer <= 0)
                EndMinigame();
            if (Input.GetKeyDown(KeyCode.W))
                AttemptCut();
        }
    }

    public void StartMinigame()
    {
        cutsText.text = $"Cortes: 0/{cutsNeededPerLog}";
        logsText.text = $"Troncos: 0";
        phase = WoodcutPhase.Cutting;
        minigamePanel.SetActive(true);
        timer = gameTimer;
        logsCut = 0;
        currentCuts = 0;
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
            Debug.Log("¡Perfecto!");
            currentCuts += 2;
        }
        else if (Mathf.Abs(val - 0.55f) < 0.30f)
        {
            Debug.Log("Regular");
            currentCuts += 1;
        }
        else
        {
            Debug.Log("Fallaste");

        }

        Debug.Log($"Cortes actuales: {currentCuts}/{cutsNeededPerLog} - Troncos completos: {logsCut}");

        if (currentCuts >= cutsNeededPerLog)
        {
            logsCut++;
            Debug.Log("arbol cortado");
            sliderSpeed *= 1.25f;
            currentCuts = 0;
        }

        
        cutsText.text = $"Cortes: {currentCuts}/{cutsNeededPerLog}";
        logsText.text = $"Troncos: {logsCut}";

    }

    void EndMinigame()
    {
        phase = WoodcutPhase.Summary;
        minigamePanel.SetActive(false);
        summaryPanel.SetActive(true);

        
        float incomePerSecond = GameManager.Instance.GetIncomePerSecond();
        int maderaGanada = Mathf.RoundToInt(logsCut * 100 * incomePerSecond);

        
        GameManager.Instance.count += maderaGanada;
        summaryText.text = $"¡Minijuego terminado!\n" +
                           $"Troncos cortados: {logsCut}\n" +
                           $"Madera obtenida: {maderaGanada}";
        sliderSpeed = 2f;
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
