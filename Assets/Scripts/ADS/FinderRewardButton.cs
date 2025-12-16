using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderRewardButton : MonoBehaviour
{
    public meteoriteSpawn meteoriteSpawn;
    public void ExecuteRewardedAd()
    {
        if (meteoriteSpawn != null) meteoriteSpawn.StartShower();
        AdsManager.AdRecompensa = true;
        AdsManager.Instance.ExecuteRewardedAd();
    }
    public void ExecuteDoubleIncomeAd()
    {
        GameManager.Instance.doubleIncomeActive = true;
        GameManager.Instance.doubleIncomeTimer = 60f;
        Debug.Log("Ingreso doble activado por 30 minutos");
        AdsManager.AdRecompensa = true;
        AdsManager.Instance.ExecuteRewardedAd();
    }
    public void ExecuteRewardedAd2()
    {
        if (meteoriteSpawn != null) meteoriteSpawn.StartShower();
        AdsManager.AdRecompensa = false;
        AdsManager.Instance.ExecuteRewardedAd();
        gameObject.SetActive(false);
    }
}
