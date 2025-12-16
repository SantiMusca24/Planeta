using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
public class storeScript : MonoBehaviour
{
    public GameObject adInfo, goldInfo1, goldInfo2, goldInfo3, gemInfo;
    public TMP_Text gold1, gold5, gold10;
    public GameObject confirmPopup;
    public TMP_Text confirmText;
    private Action pendingAction;

    [Header("Solo asignar en escena del planeta")]
    [SerializeField] TutorialScript tutorial;
    [SerializeField] private CollectingCoin coinCollector;
    [SerializeField] private CollectingCoin coinCollector2;
    public GameObject errorPopup;
    public TMP_Text errorText;
    void Start()
    {

    }
    public void CloseStore()
    {
        if (tutorial != null)
        {
            if (tutorial.count == 4)
            {
                tutorial.Tap5();
            }
        }

        gameObject.SetActive(false);
    }
    public void ShowError(float duracion = 2f)
    {
        
        errorPopup.SetActive(true);
        StartCoroutine(HideErrorAfterSeconds(duracion));
    }

    private IEnumerator HideErrorAfterSeconds(float duracion)
    {
        yield return new WaitForSeconds(duracion);
        errorPopup.SetActive(false);
    }
    public void ShowConfirm( Action accion)
    {
       
        pendingAction = accion;
        confirmPopup.SetActive(true);
    }
    public void OnConfirmYes()
    {
        if (pendingAction != null)
            pendingAction();

        confirmPopup.SetActive(false);
        pendingAction = null;
    }

    public void OnConfirmNo()
    {
        confirmPopup.SetActive(false);
        pendingAction = null;
    }
    public void TryBuyRemoveAds()
    {
        if (GameManager.Instance.gems < 100)
        {
            ShowError();
            return;
        }
        ShowConfirm(RemoveAds);
    }
    public void RemoveAds()
    {
        //menu de dinero real
        if (GameManager.Instance.gems >= 100 && AdsManager.adsAllowed)
        {
            GameManager.Instance.gems -= 100;
            AdsManager.adsAllowed = false;
            AdsManager.Instance.bannerAd.HideBannerAd();
        }
        else if (!AdsManager.adsAllowed)
        {
            GameManager.Instance.gems += 100;
            AdsManager.adsAllowed = true;
        }
    }
    public void TryBuyGold1()
    {
        if (GameManager.Instance.gems < 5)
        {
            ShowError();
            return;
        }
        ShowConfirm(BuyGold1);
    }
    public void BuyGold1()
    {
        if (GameManager.Instance.gems >= 5)
        {
            GameManager.Instance.gems -= 5;
            BuyGoldGen(1);
            coinCollector2.CollectCoin();
        }
    }
    public void TryBuyGold5()
    {
        if (GameManager.Instance.gems < 7)
        {
            ShowError();
            return;
        }
        ShowConfirm(BuyGold5);
    }
    public void BuyGold5()
    {
        if (GameManager.Instance.gems >= 7)
        {
            GameManager.Instance.gems -= 7;
            BuyGoldGen(5);
            coinCollector2.CollectCoin();
        }
    }
    public void TryBuyGold10()
    {
        if (GameManager.Instance.gems < 10)
        {
            ShowError();
            return;
        }
        ShowConfirm(BuyGold10);
    }
    public void BuyGold10()
    {
        if (GameManager.Instance.gems >= 10)
        {
            GameManager.Instance.gems -= 10;
            BuyGoldGen(10);
            coinCollector2.CollectCoin();
        }
    }
    public void BuyGoldGen(float hoursMult)
    {
        GameManager.Instance.count += GameManager.Instance.IPS * (3600 * hoursMult);
    }
    public void TryBuyGems()
    {
        ShowConfirm(BuyGems);

    }
    public void BuyGems()
    {
        //menu de dinero real
        int gemasASumar = 5;
        StartCoroutine(SumarGemasExponencialmente(gemasASumar));
        coinCollector.CollectCoin();
    }
    public void ClosePopup()
    {
        adInfo.SetActive(false);
        goldInfo1.SetActive(false);
        goldInfo2.SetActive(false);
        goldInfo3.SetActive(false);
        gemInfo.SetActive(false);
    }
    public void InfoAds()
    {
        adInfo.SetActive(true);
    }
    public void InfoGold1()
    {
        gold1.text = "( " + GameManager.Instance.IPS * (3600 * 1) + " )";
        goldInfo1.SetActive(true);
    }
    public void InfoGold2()
    {
        gold5.text = "( " + GameManager.Instance.IPS * (3600 * 5) + " )";
        goldInfo2.SetActive(true);
    }
    public void InfoGold3()
    {
        gold10.text = "( " + GameManager.Instance.IPS * (3600 * 10) + " )";
        goldInfo3.SetActive(true);
    }
    public void InfoGems()
    {
        gemInfo.SetActive(true);
    }
    public void TryBuyDoubleIncome()
    {
        int price = 20;
        if (GameManager.Instance.gems < price)
        {
            ShowError();
            return;
        }
        ShowConfirm(BuyDoubleIncome);
    }
    public void BuyDoubleIncome()
    {
        int price = 20;

        if (GameManager.Instance.gems >= price)
        {
            GameManager.Instance.gems -= price;
            GameManager.Instance.doubleIncomeActive = true;
            GameManager.Instance.doubleIncomeTimer = 60f;
            Debug.Log("Ingreso doble activado por 30 minutos");
        }
        else
        {
            Debug.Log("No hay suficientes gemas para activar ingreso doble.");
        }
    }
    private IEnumerator SumarGemasExponencialmente(int gemasTotales)
    {
        yield return new WaitForSeconds(2.7f); 

        int gemasIniciales = Mathf.RoundToInt(GameManager.Instance.gems);
        int gemasActuales = 0;
        float delay = 0.05f;

        while (gemasActuales < gemasTotales)
        {
            int incremento = Mathf.Max(1, Mathf.RoundToInt((gemasTotales - gemasActuales) * 0.15f));
            gemasActuales += incremento;
            if (gemasActuales > gemasTotales)
                gemasActuales = gemasTotales;

            GameManager.Instance.gems = gemasIniciales + gemasActuales;
            yield return new WaitForSeconds(delay);
        }
    }
}
