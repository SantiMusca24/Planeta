using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoldBoton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [Tooltip("Tiempo base entre acciones mientras el botón está presionado.")]
    public float initialHoldInterval = 0.3f;

    [Tooltip("Mínimo intervalo que puede alcanzar la aceleración.")]
    public float minHoldInterval = 0.05f;

    [Tooltip("Cuánto disminuye el intervalo por segundo mientras se mantiene presionado.")]
    public float accelerationRate = 0.05f;

    [Tooltip("Acción a ejecutar cada vez que se activa el intervalo.")]
    public UnityEvent onHoldAction;

    private bool isHeld = false;
    private float timer = 0f;
    private float currentHoldInterval;

    private void Update()
    {
        if (!isHeld) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = currentHoldInterval;
            onHoldAction?.Invoke();

            
            currentHoldInterval = Mathf.Max(minHoldInterval, currentHoldInterval - accelerationRate * Time.deltaTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        currentHoldInterval = initialHoldInterval;
        timer = 0.3f; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
    }
}
