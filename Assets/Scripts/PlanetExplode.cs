using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlanetExplode : MonoBehaviour
{
    public GameObject debris1, debris2;
    public float nukeValue = 0.5f;
    public Material nukeMaterial;
    [SerializeField] UniversalRendererData feature1;
    //private Vector3 mOffset;
    public bool hasExploded = false;
    public GameObject explosion;
    public Transform explosionPoint;
    // Start is called before the first frame update
    private void Start()
    {
        feature1.rendererFeatures[2].SetActive(false);
        feature1.rendererFeatures[3].SetActive(false);
        feature1.rendererFeatures[4].SetActive(false);
        feature1.rendererFeatures[5].SetActive(false);
        hasExploded = false;
        nukeValue = 0.5f;
    }

    private void Update()
    {
        nukeMaterial.SetFloat("_separacion", nukeValue);
        if (!hasExploded && Input.GetKeyDown(KeyCode.F))
        {
            hasExploded = true;
            StartCoroutine(Explode());
        }
    }
    public IEnumerator Explode()
    {
        // change nuke shader _disolver
        Instantiate(explosion, explosionPoint);
        yield return new WaitForSeconds(0.5f);
        while (nukeValue > -0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            nukeValue -= 0.05f;
        }
        // spawn explosion prefab at explosion point
        
        //feature1.rendererFeatures[2].SetActive(false);
        feature1.rendererFeatures[5].SetActive(true);
        debris1.SetActive(false);
        debris2.SetActive(false);
    }
}
