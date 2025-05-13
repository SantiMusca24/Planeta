using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoldBoton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [Tooltip("Tiempo entre acciones mientras el botón está presionado.")]
    public float holdInterval = 0.2f;

    [Tooltip("Acción a ejecutar cada vez que se activa el intervalo.")]
    public UnityEvent onHoldAction;

    private bool isHeld = false;
    private float timer = 0f;

    private void Update()
    {
        if (!isHeld) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = holdInterval;
            onHoldAction?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        timer = 0f; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
    }
}
