using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroGame : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image blackFadeImage;

    [Header("Timing")]
    [SerializeField] private float fadeDuration = 2f;

    [Header("GameObjects a activar después (opcional)")]
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject canvasAActivar;
    [SerializeField] private float fadeInDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeOutIntro());
        if (canvasAActivar != null)
            canvasAActivar.SetActive(false);
    }

    private IEnumerator FadeOutIntro()
    {
        titleText.gameObject.SetActive(true);

        
        Color color = blackFadeImage.color;
        color.a = 1f;
        blackFadeImage.color = color;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            blackFadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        blackFadeImage.color = new Color(color.r, color.g, color.b, 0f);
        blackFadeImage.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);

        if (gameplayUI != null) gameplayUI.SetActive(true);

        
        if (canvasAActivar != null)
        {
            canvasAActivar.SetActive(true);
            CanvasGroup group = canvasAActivar.GetComponent<CanvasGroup>();

            if (group != null)
            {
                group.alpha = 0f;
                float t2 = 0f;
                while (t2 < fadeInDuration)
                {
                    t2 += Time.deltaTime;
                    group.alpha = Mathf.Lerp(0f, 1f, t2 / fadeInDuration);
                    yield return null;
                }
                group.alpha = 1f;
            }
            
        }
    }
}
