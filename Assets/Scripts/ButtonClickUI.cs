using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonClickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
     [Header("General")]
    [SerializeField] private bool animateOnEnable = true;
    [SerializeField] private bool animateOnClick = true;

    [Header("Appear Animation")]
    [SerializeField] private float appearDuration = 0.5f;
    [SerializeField] private Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private Ease appearEase = Ease.OutBack;

    [Header("Click Animation")]
    [SerializeField] private float pressedScale = 1.1f;
    [SerializeField] private float clickDuration = 0.1f;
    [SerializeField] private Ease clickEase = Ease.OutQuad;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        if (!animateOnEnable) return;

        transform.localScale = startScale;
        transform.DOScale(originalScale, appearDuration).SetEase(appearEase);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!animateOnClick) return;

        transform.DOKill();
        transform.DOScale(originalScale * pressedScale, clickDuration).SetEase(clickEase);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!animateOnClick) return;

        transform.DOKill();
        transform.DOScale(originalScale, clickDuration).SetEase(clickEase);
    }
}
