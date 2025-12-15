using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    [SerializeField] TutorialScript tutorial;
    public void Back()
    {
        SceneManager.LoadScene("SampleScene");
    }
    private void Start()
    {
        int check = PlayerPrefs.GetInt("Tutorial2", 0);
        if (check == 0)
        {
            tutorial.StartChain2();
        }
    }

}
