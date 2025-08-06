using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changeScene : MonoBehaviour
{
    [SerializeField] UniversalRendererData feature1;
    float someValue;
    
    public Animator transition;
    public Material myMaterial;
    public Animator extraAnimation;
    public float transitionTime = 1f;
    public GameObject[] uiImages;
    
    // DEFINIR structure EN EL INSPECTOR
    // DEFAULT: 1
    // TOWN: 2
    [SerializeField] int structure = 1;

    private void Update()
    {
        if (myMaterial != null) myMaterial.SetFloat("_fresnel_power", someValue);
    }
    void Start()
    {
        feature1.rendererFeatures[2].SetActive(true);
        feature1.rendererFeatures[3].SetActive(false);
        feature1.rendererFeatures[4].SetActive(false);
        feature1.rendererFeatures[5].SetActive(false);
        someValue = 4.96f;
        foreach (GameObject img in uiImages)
        {
            if (img != null)
                img.SetActive(false);
        }

    }
    public void LoadLevelFromUI()
{
    StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + structure));
}
    public void ActivateUI()
    {
        foreach (GameObject img in uiImages)
        {
            if (img != null)
                img.SetActive(true);
        }
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        someValue = 0.5f;
        //if (myMaterial != null) Debug.LogError("AAAAAAAAAAAAAAAAA");
        yield return new WaitForSeconds(0.1f);

        foreach (GameObject img in uiImages)
        {
            if (img != null)
                img.SetActive(true);
        }

        
        //transition.SetTrigger("Start");
        
        //extraAnimation.SetTrigger("Play");
        

        yield return new WaitForSeconds(transitionTime);

        //SceneManager.LoadScene(levelIndex);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}
