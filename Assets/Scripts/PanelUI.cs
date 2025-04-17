using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUI : MonoBehaviour
{
    public RectTransform panel; 
    public Vector2 onScreenPosition = Vector2.zero;
    public Vector2 offScreenPosition = new Vector2(-5000f, 0f); 
    public float slideSpeed = 500f;

    private bool isVisible = false;
    private Coroutine currentCoroutine;

    private void Start()
    {
        
        if (panel != null)
        {
            panel.anchoredPosition = offScreenPosition;
        }
    }
    public void TogglePanel()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        isVisible = !isVisible;
        currentCoroutine = StartCoroutine(SlidePanel(isVisible ? onScreenPosition : offScreenPosition));
    }

    private System.Collections.IEnumerator SlidePanel(Vector2 targetPosition)
    {
        while (Vector2.Distance(panel.anchoredPosition, targetPosition) > 0.1f)
        {
            panel.anchoredPosition = Vector2.MoveTowards(
                panel.anchoredPosition,
                targetPosition,
                slideSpeed * Time.deltaTime
            );
            yield return null;
        }

        panel.anchoredPosition = targetPosition;
    }
}
