using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changeScene : MonoBehaviour
{
   
    public Animator transition;
    public Animator extraAnimation;
    public float transitionTime = 1f;
    public GameObject[] uiImages;
    
    // DEFINIR structure EN EL INSPECTOR
    // DEFAULT: 1
    // TOWN: 2
    [SerializeField] int structure = 1;


    void Start()
    {
        
        foreach (GameObject img in uiImages)
        {
            if (img != null)
                img.SetActive(false);
        }

    }
    public void LoadLevelFromUI()
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA 1");
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
        int check = PlayerPrefs.GetInt("Tutorial1", 0);
        int check2 = PlayerPrefs.GetInt("Tutorial2", 0);
        if (check == 0) yield break;
        if (check2 == 0 && levelIndex != 1) yield break;
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
