using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static PescaUI;

public class FishMinigame : MonoBehaviour
{
    [SerializeField] UniversalRendererData feature1;
    //public Camera _camData;
    //public bool shieldOn = true;
    //public GameObject shieldObj;

    public GameObject minigamePanel;
    public GameObject fishBites;
    public float sliderSpeed = 1f;
    public int cutsNeededPerLog = 3;
    public float secondsMax = 3, secondsMin = 1;

    private bool isCounting = false;
    private bool increasing = true;
    private int currentCuts = 0;
    private int logsCut = 0;
    public float gameTimer = 20f;
    [SerializeField] public static float cooldownDuracion = 10f;
    [SerializeField] private float timer;
    private WoodcutPhase phase = WoodcutPhase.Inactive;
    private float sliderPauseTimer = 0f;
    private float fishTimer;
    private bool fishReady;
    private Coroutine cooldownRoutine;

    public GameObject summaryPanel;
    public GameObject botton; 
    public GameObject bottonAd;
    public GameObject bottonInicio;

    public PescaUI uiManager;
    [SerializeField] private CollectingCoin coinCollector;
    
    private void Start()
    {
        //shieldOn = true;
        feature1.rendererFeatures[2].SetActive(true);
        feature1.rendererFeatures[3].SetActive(false);
        feature1.rendererFeatures[4].SetActive(false);
        feature1.rendererFeatures[5].SetActive(false);
        //shieldObj.SetActive(true);
        minigamePanel.SetActive(false);
        fishBites.SetActive(false);
        botton.SetActive(false);
        bottonAd.SetActive(false);
        isCounting = false;
        
    }
    void Update()
    {
        if (phase == WoodcutPhase.Cutting)
        {
            if (!isCounting)
            {
                isCounting = true;
                fishTimer = Random.Range(secondsMin, secondsMax);
                StartCoroutine(FishSpawn());
            }
            //UpdateSlider();
            timer -= Time.deltaTime;
            uiManager?.UpdateMinigameUI(tiempo: timer);

            if (timer <= 0)
                EndMinigame();

            if (Input.GetKeyDown(KeyCode.W))
                AttemptCut();
        }
    }
    

    private IEnumerator FishSpawn()
    {
        yield return new WaitForSeconds(fishTimer);
        if ( timer  >= 0 )
        {
            fishBites.SetActive(true);
            fishReady = true;
        }
        yield return new WaitForSeconds(sliderSpeed);
        fishBites.SetActive(false);
        fishReady = false;
        isCounting = false;
    }

    public void StartMinigame()
    {
        feature1.rendererFeatures[2].SetActive(false);
        feature1.rendererFeatures[3].SetActive(true);
        bottonInicio.SetActive(false);
        //precisionSlider.gameObject.SetActive(true);
        botton.SetActive(true);
        currentCuts = 0;
        logsCut = 0;
        timer = gameTimer;
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

    /*void UpdateSlider()
    {
        if (sliderPauseTimer > 0f)
        {
            sliderPauseTimer -= Time.deltaTime;
            return;
        }

        if (increasing)
            precisionSlider.value += Time.deltaTime * sliderSpeed;
        else
            precisionSlider.value -= Time.deltaTime * sliderSpeed;

        if (precisionSlider.value >= 1f) increasing = false;
        if (precisionSlider.value <= 0f) increasing = true;
    }*/

    public void AttemptCut()
    {
        if (phase != WoodcutPhase.Cutting) return;


        //if (sliderPauseTimer > 0f) return;

        //float val = precisionSlider.value;

        if (fishReady)
        {
            fishReady = false;
            fishBites.SetActive(false);            
            currentCuts += 1;
            secondsMax *= 0.9f;
            secondsMin *= 0.5f;
            sliderSpeed *= 0.8f;
            uiManager?.ShowCutFeedback(CutFeedbackType.Perfect);
        }
        else
        {
            currentCuts = Mathf.Max(0, currentCuts - 1);
            uiManager?.ShowCutFeedback(CutFeedbackType.Fail);
        }


        //sliderPauseTimer = 0.3f;

        if (currentCuts >= cutsNeededPerLog)
        {
            logsCut++;
            currentCuts = 0;
                       
        }

        uiManager?.UpdateMinigameUI(cortes: currentCuts, cortesNecesarios: cutsNeededPerLog);
        uiManager?.UpdateMinigameUI(troncos: logsCut);

    }

    void EndMinigame()
    {
        feature1.rendererFeatures[2].SetActive(true);
        feature1.rendererFeatures[3].SetActive(false);
        phase = WoodcutPhase.Summary;

        //precisionSlider.gameObject.SetActive(false);
        fishBites.gameObject.SetActive(false);

        botton.SetActive(false);

        float incomePerSecond = GameManager.Instance.GetIncomePerSecond();

        int maderaGanada = Mathf.RoundToInt(logsCut * 500 * incomePerSecond);



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
}
