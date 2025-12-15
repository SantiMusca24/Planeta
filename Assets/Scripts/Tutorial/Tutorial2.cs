using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2 : MonoBehaviour
{
    [SerializeField] TutorialScript tutorial;
    private void Start()
    {
        int check = PlayerPrefs.GetInt("Tutorial2", 0);
        if (check == 0)
        {
            tutorial.StartChain2();
        }
    }
}
