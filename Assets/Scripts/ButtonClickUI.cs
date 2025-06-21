using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonClickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float pressedScale = 1.1f;
    [SerializeField] private float animationDuration = 0.1f;
    [SerializeField] private Ease animationEase = Ease.OutQuad;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(originalScale * pressedScale, animationDuration).SetEase(animationEase);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(originalScale, animationDuration).SetEase(animationEase);
    }
}
