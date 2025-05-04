using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUI : MonoBehaviour
{
    
    public RectTransform panel;
    public Vector2 onScreenPosition = Vector2.zero;
    public Vector2 offScreenPosition = new Vector2(0f, -1000f);
    public float slideSpeed = 500f;

    
    public RectTransform extraUIElement;
    public Vector2 extraOnScreenPosition;
    public Vector2 extraOffScreenPosition;

    private bool isVisible = false;
    private Coroutine panelCoroutine;
    private Coroutine extraCoroutine;
    public UnityEngine.UI.Image extraImage;
    public Sprite onScreenSprite;
    public Sprite offScreenSprite;

    private void Start()
    {
        if (panel != null)
            panel.anchoredPosition = offScreenPosition;

        if (extraUIElement != null)
            extraUIElement.anchoredPosition = extraOffScreenPosition;
    }

    public void TogglePanel()
    {
        isVisible = !isVisible;

        if (panelCoroutine != null) StopCoroutine(panelCoroutine);
        if (extraCoroutine != null) StopCoroutine(extraCoroutine);

        panelCoroutine = StartCoroutine(Slide(panel, isVisible ? onScreenPosition : offScreenPosition));
        extraCoroutine = StartCoroutine(Slide(extraUIElement, isVisible ? extraOnScreenPosition : extraOffScreenPosition));

        
        if (extraImage != null)
        {
            extraImage.sprite = isVisible ? onScreenSprite : offScreenSprite;
        }
    }

    private System.Collections.IEnumerator Slide(RectTransform target, Vector2 targetPosition)
    {
        while (Vector2.Distance(target.anchoredPosition, targetPosition) > 0.1f)
        {
            target.anchoredPosition = Vector2.MoveTowards(
                target.anchoredPosition,
                targetPosition,
                slideSpeed * Time.deltaTime
            );
            yield return null;
        }

        target.anchoredPosition = targetPosition;
    }
}
