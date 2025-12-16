using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static BosqueUI;

public class WoodMiniGame : Rewind
{
    public GameObject minigamePanel;
    public Slider precisionSlider;
    public float sliderSpeed = 2f;
    public int cutsNeededPerLog = 5;

    private bool increasing = true;
    private int currentCuts = 0;
    private int logsCut = 0;
    private float gameTimer = 10f;
    [SerializeField] public static float cooldownDuracion = 10f;
    [SerializeField] private float timer;
    private WoodcutPhase phase = WoodcutPhase.Inactive;
    private float sliderPauseTimer = 0f;
    private Coroutine cooldownRoutine;

    public GameObject summaryPanel;
    public GameObject botton;
    public GameObject bottonAd;
    public GameObject bottonInicio;

    public BosqueUI uiManager;
    [SerializeField] private CollectingCoin coinCollector;

    private void Start()
    {
        currentCuts = 0;
        logsCut = 0;
        minigamePanel.SetActive(false);
       precisionSlider.gameObject.SetActive(false);
       botton.SetActive(false);
        bottonAd.SetActive(false);

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
        bottonAd.SetActive(false);
        precisionSlider.gameObject.SetActive(true);
        botton.SetActive(true);
        //currentCuts = 0;
        //logsCut = 0;
        timer = cloud1.woodGameTime;
        sliderSpeed = 1f;
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
        if (sliderPauseTimer > 0f)
        {
            sliderPauseTimer -= Time.deltaTime;
            return;
        }

        if (increasing)
            precisionSlider.value += Time.deltaTime * sliderSpeed * cloud1.woodGameSpeedMult;
        else
            precisionSlider.value -= Time.deltaTime * sliderSpeed;

        if (precisionSlider.value >= 1f) increasing = false;
        if (precisionSlider.value <= 0f) increasing = true;
    }

    public void AttemptCut()
    {
        if (phase != WoodcutPhase.Cutting) return;

       
        if (sliderPauseTimer > 0f) return;

        float val = precisionSlider.value;

        if (Mathf.Abs(val - 0.55f) < 0.1f)
        {
            currentCuts += 2;
            uiManager?.ShowCutFeedback(CutFeedbackType.Perfect);
        }
        else if (Mathf.Abs(val - 0.55f) < 0.25f)
        {
            currentCuts += 1;
            uiManager?.ShowCutFeedback(CutFeedbackType.Regular);
        }
        else
        {
            currentCuts = Mathf.Max(0, currentCuts - 1);
            uiManager?.ShowCutFeedback(CutFeedbackType.Fail);
        }

        
        sliderPauseTimer = 0.3f;

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
        StartToRec();
        
        phase = WoodcutPhase.Summary;
        
        precisionSlider.gameObject.SetActive(false);
        
        botton.SetActive(false);
        
        float incomePerSecond = GameManager.Instance.GetIncomePerSecond();
        
        int maderaGanada = Mathf.RoundToInt(logsCut * 100 * incomePerSecond);

        

        string resumen = 
                         $" {maderaGanada}";

        if (uiManager?.resumenText != null)
            uiManager.resumenText.gameObject.SetActive(true);
        
        bottonInicio.SetActive(true);
        bottonAd.SetActive(true);
        bottonInicio.GetComponent<Button>().interactable = false;

        uiManager?.OcultarMinigameTextos2();

        cooldownRoutine = StartCoroutine(BottomCooldowm(cooldownDuracion));

        uiManager?.ShowPanel(MinigamePanelType.Summary, resumen);
        
        StartCoroutine(OcultarTextos());
        if (maderaGanada > 0)
        {
           coinCollector.CollectCoin();
            StartCoroutine(SumarPuntosExponencialmente(maderaGanada));
        }
        logsCut = 0;
        currentCuts = 0;
    }
    private IEnumerator BottomCooldowm(float segundos)
    {

        float t = segundos;

        if (uiManager != null && uiManager.cooldownTimerText != null)
            uiManager.cooldownTimerText.gameObject.SetActive(true);

        while (t > 0)
        {
            if (uiManager != null && uiManager.cooldownTimerText != null)
                uiManager.cooldownTimerText.text = "" + Mathf.CeilToInt(t).ToString() + "s";

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
    private IEnumerator SumarPuntosExponencialmente(int totalPuntos)
    {
        yield return new WaitForSeconds(2.5f); 

    int puntosActuales = 0;
    float delay = 0.05f;

    float valorInicial = GameManager.Instance.count;

    while (puntosActuales < totalPuntos)
    {
        
        int incremento = Mathf.Max(1, Mathf.RoundToInt((totalPuntos - puntosActuales) * 0.15f));
        puntosActuales += incremento;

        if (puntosActuales > totalPuntos)
            puntosActuales = totalPuntos;

        GameManager.Instance.count = valorInicial + puntosActuales;

        yield return new WaitForSeconds(delay);
    }
    }
    public void CancelarCooldown()
    {
        if (cooldownRoutine != null)
        {
            StopCoroutine(cooldownRoutine);
            cooldownRoutine = null;
        }

        if (bottonInicio != null)
            bottonInicio.GetComponent<Button>().interactable = true;

        if (uiManager != null && uiManager.cooldownTimerText != null)
            uiManager.cooldownTimerText.gameObject.SetActive(false);
    }
    public enum WoodcutPhase
    {
        Inactive,
        Starting,
        Cutting,
        BetweenLogs,
        Summary
    }

    /*public override IEnumerator StartToRec()
    {
        while (true)
        {
            memento.Rec(new object[] { currentCuts, logsCut });
            yield return new WaitForSeconds(0.1f);
        }
    }*/
    public override void StartToRec()
    {
        memento.Rec(new object[] { currentCuts, logsCut });
    }

    protected override void BeRewind(ParamsMemento wrappers)
    {
        currentCuts = (int)wrappers.parameters[0];
        logsCut = (int)wrappers.parameters[1];
    }
}
