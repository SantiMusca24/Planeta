using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIPOPIN : MonoBehaviour
{

    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private Ease easeType = Ease.OutBack;

    private Vector3 originalScale;

    private void Awake()
    {
        
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
       
        transform.localScale = startScale;

        
        transform.DOScale(originalScale, duration).SetEase(easeType);
    }
}
