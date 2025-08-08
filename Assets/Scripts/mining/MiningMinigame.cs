using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static PescaUI;

public class MiningMinigame : MonoBehaviour
{
    //private float correct = 254.3163f;
    //public bool left;
   // public float correct = 494.2f;
    //public float Ycorrec = 787;
    [SerializeField] float leftMost = 784, rightMost = 1162;
    [SerializeField] float rockXPos = 800;
    [SerializeField] int bombRoll = 1;
    [SerializeField] bool isBomb = false;
    public GameObject minigamePanel;
    public GameObject rockHits;
    public GameObject bombHits; // this randomly replaces the rock
    public float sliderSpeed = 138f;
    public int cutsNeededPerLog = 1;
    public float secondsMax = 2, secondsMin = 0.5f;

    private bool isCounting = false;
    private bool increasing = true;
    private int currentCuts = 0;
    private int logsCut = 0;
    public float gameTimer = 20f;
    [SerializeField] public static float cooldownDuracion = 10f;
    [SerializeField] private float timer;
    private WoodcutPhase phase = WoodcutPhase.Inactive;
    private float sliderPauseTimer = 0f;
    private float rocksTimer;
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
        leftMost = 784;
        rightMost = 1162;
        bombRoll = 2;
        isBomb = false;
        minigamePanel.SetActive(false);
        rockHits.SetActive(false);
        bombHits.SetActive(false);
        botton.SetActive(false);
        bottonAd.SetActive(false);
        isCounting = false;
        
    }
    void Update()
    {
        //a
        //rockHits.transform.position = new Vector3(correct, Ycorrec, rockHits.transform.position.z);
        if (phase == WoodcutPhase.Cutting)
            
        {
            if (!isCounting)
            {
                isCounting = true;
                rockXPos = Random.Range(leftMost, rightMost);
                //if (left) rockXPos = leftMost;
                //else rockXPos = rightMost;
                rocksTimer = Random.Range(secondsMin, secondsMax);
                bombRoll = Random.Range(1, 4);
                if (bombRoll == 1)
                {
                    isBomb = true;
                    bombHits.transform.position = new Vector3(rockXPos, 800, bombHits.transform.position.z);
                }
                else
                {
                    isBomb = false;
                    rockHits.transform.position = new Vector3(rockXPos, 800, rockHits.transform.position.z);
                }
                //StartCoroutine(FishSpawn());
            }
            else
            {
                if (!isBomb)
                {
                    //rockHits.transform.position = new Vector3(rockHits.transform.position.x, rockHits.transform.position.y - , rockHits.transform.position.z);
                    //var dir = rockHits.transform.position - transform.position;
                    //transform.forward = dir;
                    rockHits.transform.position -= rockHits.transform.up * sliderSpeed * Time.deltaTime;
                    if (rockHits.transform.position.y <= 323)
                    {
                        rockHits.transform.position = new Vector3(2000, 2000, rockHits.transform.position.z);
                        isCounting = false;
                    }
                }
                else
                {
                    bombHits.transform.position -= bombHits.transform.up * sliderSpeed * Time.deltaTime;
                    if (bombHits.transform.position.y <= 323)
                    {
                        bombHits.transform.position = new Vector3(2000, 2000, rockHits.transform.position.z);
                        isCounting = false;
                    }
                }
            }
                //UpdateSlider();
                timer -= Time.deltaTime;
            uiManager?.UpdateMinigameUI(tiempo: timer);

            if (timer <= 0)
                EndMinigame();

            /*if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //AttemptCut();
                if (Input.mousePosition)
            }*/

        }
    }
    
    public void hitRock()
    {
        rockHits.transform.position = new Vector3(2000, 2000, rockHits.transform.position.z);
        //currentCuts++;
        logsCut++;
        sliderSpeed += 70f;
        uiManager?.ShowCutFeedback(CutFeedbackType.Perfect);
        uiManager?.UpdateMinigameUI(troncos: logsCut);
        isCounting = false;
    }
    public void hitBomb()
    {
        bombHits.transform.position = new Vector3(2000, 2000, rockHits.transform.position.z);
        if (logsCut > 0) logsCut--;
        uiManager?.ShowCutFeedback(CutFeedbackType.Fail);
        uiManager?.UpdateMinigameUI(troncos: logsCut);
        isCounting = false;
    }

    /*private IEnumerator FishSpawn()
    {
        yield return new WaitForSeconds(rocksTimer);
        if ( timer  >= 0 )
        {
            rockHits.SetActive(true);
            fishReady = true;
        }
        yield return new WaitForSeconds(sliderSpeed);
        rockHits.SetActive(false);
        fishReady = false;
        isCounting = false;
    }*/

    public void StartMinigame()
    {
        rockHits.transform.position = new Vector3(rockXPos, 800, bombHits.transform.position.z);
        bombHits.transform.position = new Vector3(rockXPos, 800, bombHits.transform.position.z);
        rockHits.SetActive(true);
        bombHits.SetActive(true);
        //isCounting = true;
        //Debug.Log("ROCKS SHOULD BE ACTIVE");
        bottonInicio.SetActive(false);
        //precisionSlider.gameObject.SetActive(true);
        botton.SetActive(true);
        currentCuts = 0;
        logsCut = 0;
        timer = gameTimer;
        sliderSpeed = 138f;
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
        
        if (fishReady) // && IF MOUSE HITS ROCK
        {
            fishReady = false;
            rockHits.SetActive(false);
            //currentCuts += 1;
            logsCut++;
            secondsMax *= 0.9f;
            secondsMin *= 0.5f;
            sliderSpeed += 70f;
            uiManager?.ShowCutFeedback(CutFeedbackType.Perfect);
        }
        else
        {
            currentCuts = Mathf.Max(0, logsCut - 1);
            uiManager?.ShowCutFeedback(CutFeedbackType.Fail);
        }


        //sliderPauseTimer = 0.3f;
        /*
        if (currentCuts >= cutsNeededPerLog)
        {
            logsCut++;
            currentCuts = 0;
                       
        }*/

        uiManager?.UpdateMinigameUI(cortes: currentCuts, cortesNecesarios: cutsNeededPerLog);
        uiManager?.UpdateMinigameUI(troncos: logsCut);

    }

    void EndMinigame()
    {
        
        phase = WoodcutPhase.Summary;

        //precisionSlider.gameObject.SetActive(false);
        rockHits.gameObject.SetActive(false);

        botton.SetActive(false);

        float incomePerSecond = GameManager.Instance.GetIncomePerSecond();

        int maderaGanada = Mathf.RoundToInt(logsCut * 200 * (incomePerSecond+1));



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
