using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private float duration = 1f; // Duración del efecto
    [SerializeField] private string shaderProperty = "_DissolveAmount";

    private Material material;

    private void OnEnable()
    {
        // Crear una instancia del material para no afectar otros objetos
        Renderer renderer = GetComponent<Renderer>();
        material = new Material(renderer.material);
        renderer.material = material;

        // Iniciar la animación del dissolve
        StartCoroutine(AnimateDissolve());
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

        material.SetFloat(shaderProperty, endValue); // Asegura que termine en -1
    }
}
