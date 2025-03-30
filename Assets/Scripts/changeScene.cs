using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
   
    public Animator transition;
    public Animator extraAnimation;
    public float transitionTime = 1f;
    public GameObject[] uiImages;

    void Start()
    {
        
        foreach (GameObject img in uiImages)
        {
            if (img != null)
                img.SetActive(false);
        }

    }
    void OnMouseDown()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
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

        
        yield return new WaitForSeconds(0.1f);

        foreach (GameObject img in uiImages)
        {
            if (img != null)
                img.SetActive(true);
        }

        
        transition.SetTrigger("Start");
        
        extraAnimation.SetTrigger("Play");
        

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);


    }
}
