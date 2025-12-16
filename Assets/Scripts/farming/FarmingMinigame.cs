using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GranjaUI;

public class FarmingMinigame : MonoBehaviour
{
    public GameObject minigamePanel;
    public GameObject fishBites;
    //public float sliderSpeed = 1f;
    public int cutsNeededPerLog = 5;
    //public float secondsMax = 3, secondsMin = 1;

    
    private int currentCuts = 0;
    private int logsCut = 0;
    public float gameTimer = 20f;
    [SerializeField] public static float cooldownDuracion = 10f;
    [SerializeField] private float timer;
    private WoodcutPhase phase = WoodcutPhase.Inactive;
    //private float sliderPauseTimer = 0f;
    //private float fishTimer;
    //private bool fishReady;
    private Coroutine cooldownRoutine;

    public GameObject summaryPanel;
    public GameObject botton;
    public GameObject bottonAd;
    public GameObject bottonInicio;

    public GranjaUI uiManager;
    [SerializeField] private CollectingCoin coinCollector;

    // ################

    [SerializeField] Transform topPivot, botPivot, cow;

    float cowPos;
    float cowDest;
    public float cowTimer;

    [SerializeField] float timerMult = 3f;

    float cowSpd;
    [SerializeField] float smoothMotion = 1f;

    [SerializeField] Transform catcher;
    float catcherPos;
    [SerializeField] float catcherSize = 0.19f;
    [SerializeField] float catcherPower = 5f;
    float catcherProgress;
    float catcherPullVelocity;
    [SerializeField] float catcherPullPower = 0.01f;
    [SerializeField] float catcherGravityPower = 0.005f;
    [SerializeField] float catcherProgressDegradationPower = 0.1f;

    [SerializeField] Transform progressBarContainer;
    public AudioManager audioManager;

    private void Start()
    {
        smoothMotion = 1;
        minigamePanel.SetActive(false);
        //fishBites.SetActive(false);
        botton.SetActive(false);
        bottonAd.SetActive(false);
        //isCounting = false;
    }
    private void Update()
    {
        if (phase == WoodcutPhase.Cutting)
        {
            //UpdateSlider();
            timer -= Time.deltaTime;
            uiManager?.UpdateMinigameUI(tiempo: timer);

            Cow();
            Catcher();
            ProgressCheck();

            if (timer <= 0)
                EndMinigame();

        }
        
    }
    private void ProgressCheck()
    {
        Vector3 ls = progressBarContainer.localScale;
        ls.y = catcherProgress;
        progressBarContainer.localScale = ls;

        float min = catcherPos - catcherSize / 2;
        float max = catcherPos + catcherSize / 2;

        if (min < cowPos && cowPos < max)
        {
            catcherProgress += catcherPower * cloud1.farmGameSpeedMult * Time.deltaTime;
            //Debug.Log("DENTRO DE ZONA");
        }
        else
        {
            catcherProgress -= catcherProgressDegradationPower * Time.deltaTime;
        }
        if (catcherProgress >= 1)
        {
            catcherProgress = 0;
            currentCuts += 1;
            smoothMotion = smoothMotion * 0.9f;
            uiManager?.ShowCutFeedback(CutFeedbackType.Perfect);
            if (audioManager != null)
                audioManager.Play("Cow");
            if (currentCuts >= cutsNeededPerLog)
            {
                logsCut++;
                currentCuts = 0;
            }

            uiManager?.UpdateMinigameUI(cortes: currentCuts, cortesNecesarios: cutsNeededPerLog);
            uiManager?.UpdateMinigameUI(troncos: logsCut);
        }

        catcherProgress = Mathf.Clamp(catcherProgress, 0f, 1f);
    }
    void Catcher()
    {
        if (Input.GetMouseButton(0))
        {
            catcherPullVelocity += catcherPullPower * Time.deltaTime * 2;
        }
        catcherPullVelocity -= catcherGravityPower * Time.deltaTime;

        catcherPos += catcherPullVelocity;
        catcherPos = Mathf.Clamp(catcherPos, catcherSize / 2, 1 - catcherSize / 2);
        catcher.position = Vector3.Lerp(botPivot.position, topPivot.position, catcherPos);
    }

    

    void Cow()
    {
        cowTimer -= Time.deltaTime;
        if (cowTimer < 0f)
        {
            cowTimer = UnityEngine.Random.value * timerMult;

            cowDest = UnityEngine.Random.value;
        }

        cowPos = Mathf.SmoothDamp(cowPos, cowDest, ref cowSpd, smoothMotion);
        cow.position = Vector3.Lerp(botPivot.position, topPivot.position, cowPos);
    }
    public void StartMinigame()
    {
        bottonInicio.SetActive(false);
        //precisionSlider.gameObject.SetActive(true);

        //botton.SetActive(true);
        smoothMotion = 1;
        currentCuts = 0;
        logsCut = 0;
        timer = gameTimer;
        //sliderSpeed = 1f;
        phase = WoodcutPhase.Cutting;

        minigamePanel.SetActive(true);
        //uiManager?.ShowPanel(MinigamePanelType.Minigame);
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
    void EndMinigame()
    {
        if (audioManager != null)
            audioManager.Play("Finish");
        phase = WoodcutPhase.Summary;
        minigamePanel.SetActive(false);
        //precisionSlider.gameObject.SetActive(false);
        //fishBites.gameObject.SetActive(false);

        botton.SetActive(false);

        float incomePerSecond = GameManager.Instance.GetIncomePerSecond();

        int maderaGanada = Mathf.RoundToInt(logsCut * 1000 * incomePerSecond);



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
            Debug.Log("AHGFJDSHKGJHS 1");
        }

        if (bottonInicio != null)
        {
            bottonInicio.GetComponent<Button>().interactable = true;
            Debug.Log("AHGFJDSHKGJHS 2");
        }


        if (uiManager != null && uiManager.cooldownTimerText != null)
        {
            uiManager.cooldownTimerText.gameObject.SetActive(false);
            Debug.Log("AHGFJDSHKGJHS 3");
        }
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
