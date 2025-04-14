using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorUI : MonoBehaviour
{
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text incomePerSecondText;
    [Header("Floating Text")]
    [SerializeField] Transform floatingTextContainer;
    [SerializeField] TMP_Text floatingTextPrefab;

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
    public void SpawnFloatingText(int amount)
    {
        
        Vector3 randomOffset = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0f);

        TMP_Text floatingText = Instantiate(floatingTextPrefab, floatingTextContainer);

        
        floatingText.rectTransform.anchoredPosition = randomOffset;

        floatingText.text = "+" + amount.ToString();
        floatingText.color = Color.green;
        
        Animator animator = floatingText.GetComponentInParent<Animator>();
        if (animator != null)
        {
            animator.Play("pop Up");
        }

        Destroy(floatingText.gameObject, 1f);
    }
}
