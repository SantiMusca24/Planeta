using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetUI : BaseContadorUI
{
    [Header("Floating Text")]
    [SerializeField] protected Transform floatingTextContainer;
    [SerializeField] protected TMP_Text floatingTextPrefab;
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
    public override void UpdateMinigameUI(int? cortes = null, int? troncos = null, float? tiempo = null, int? cortesNecesarios = null)
    {
        throw new System.NotImplementedException();
    }
    public override void OcultarMinigameTextos()
    {
        throw new System.NotImplementedException();
    }
    public override void OcultarMinigameTextos2()
    {
        throw new System.NotImplementedException();
    }
    public override void ShowPanel(MinigamePanelType panelType, string resumen = "")
    {
        throw new System.NotImplementedException();
    }
}
