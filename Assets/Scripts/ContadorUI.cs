using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorUI : MonoBehaviour
{
     [SerializeField] TMP_Text countText;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (countText != null)
        {
            countText.text = GameManager.Instance.count.ToString();
        }
    }
}
