using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetUI : BaseContadorUI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnFloatingText(double amount)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0f);

        TMP_Text floatingText = Instantiate(floatingTextPrefab, floatingTextContainer);


        floatingText.rectTransform.anchoredPosition = randomOffset;


        floatingText.text = "+" + AbreviateNumber.Format(amount);
        floatingText.color = Color.green;


        Animator animator = floatingText.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("pop up");
        }

        Destroy(floatingText.gameObject, 1f);
    }
}
