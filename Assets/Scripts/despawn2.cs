using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class despawn2 : MonoBehaviour
{
    [SerializeField] private float duration = 1f; 
    [SerializeField] private string shaderProperty = "_Grow";

    [SerializeField] private ParticleSystem particleEffect; 
    [SerializeField] private ScriptableRendererFeature clickShader; 
    [SerializeField] private float extraEffectDuration = 1f; 

    private Material material;

    private void Awake()
    {
       
        Renderer renderer = GetComponent<Renderer>();
        material = new Material(renderer.material);
        renderer.material = material;

        
        if (clickShader != null)
            clickShader.SetActive(false);
        if (particleEffect != null)
            particleEffect.Stop();
    }

    public void TriggerDissolve()
    {
        StartCoroutine(AnimateDissolve());
        StartCoroutine(ActivateExtraEffects());
    }

    private void OnMouseDown()
    {
        TriggerDissolve();
    }

    private IEnumerator AnimateDissolve()
    {
        float time = 0f;
        float startValue = 1f;
        float endValue = -1f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            float value = Mathf.Lerp(startValue, endValue, t);
            material.SetFloat(shaderProperty, value);
            yield return null;
        }

        material.SetFloat(shaderProperty, endValue);
    }

    private IEnumerator ActivateExtraEffects()
    {
        
        if (clickShader != null)
            clickShader.SetActive(true);
        if (particleEffect != null)
            particleEffect.Play();

        yield return new WaitForSeconds(extraEffectDuration);

        
        if (clickShader != null)
            clickShader.SetActive(false);
        if (particleEffect != null)
            particleEffect.Stop();
    }
}
