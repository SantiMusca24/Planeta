using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorUI : MonoBehaviour
{
     [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text incomePerSecondText;

    void Start()
    {
        
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (countText != null)
        {
            countText.text = Mathf.RoundToInt(GameManager.Instance.count).ToString();
        }

        if (incomePerSecondText != null)
        {
            float incomePerSecond = GameManager.Instance.GetIncomePerSecond();
            incomePerSecondText.text = Mathf.Round(incomePerSecond).ToString() + " /s";
        }
    }
}
