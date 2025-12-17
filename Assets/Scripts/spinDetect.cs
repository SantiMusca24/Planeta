using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class spinDetect : MonoBehaviour
{

    [SerializeField] int currentCheck = 1;
    [SerializeField] static public int spins = 0;
    [SerializeField] private TMP_Text _spinsTx;
    [SerializeField] TutorialScript tutorial;
    int spinsTutorial = 0;
    public AudioManager audioManager;
    public static event Action OnPlanetRotated;
    void Start()
    {
        currentCheck = 1;
        _spinsTx.text = "GIROS: " + spins;
        int check = PlayerPrefs.GetInt("Tutorial1", 0);
        if (check == 0)
        {
            tutorial.StartChain();
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        switch (currentCheck)
        {
            case 1:
                Debug.Log("fuck1");
                if (other.name == "checkpoint3") currentCheck = 2;
                break;
            case 2:
                if (other.name == "checkpoint1")
                {
                    currentCheck = 1;
                    spins++;
                    if (audioManager != null)
                    {
                        audioManager.Play("Ding");
                    }
                    _spinsTx.text = "GIROS: " + spins;
                    OnPlanetRotated?.Invoke();
                    if (tutorial.count == 2)
                    {
                        spinsTutorial++;
                        if (spinsTutorial >= 5)
                        {
                            tutorial.ClickWaitMethod(2);
                            tutorial.icons[0].SetActive(true);
                            tutorial.icons[1].SetActive(true);
                            tutorial.icons[2].SetActive(true);
                            tutorial.icons[11].SetActive(true);
                            tutorial.icons[3].SetActive(true);
                            tutorial.icons[12].SetActive(true);
                            tutorial.icons[4].SetActive(true);
                            tutorial.icons[5].SetActive(true);
                            tutorial.icons[6].SetActive(true);
                            tutorial.bg.SetActive(true);
                            tutorial.text3.SetActive(true);
                        }
                    }
                }
                break;
                /*case 3:
                    Debug.Log("fuck3");
                    if (other.name == "checkpoint4") currentCheck = 4;
                    break;
                case 4:
                    Debug.Log("fuck4");
                    if (other.name == "checkpoint1")
                    {
                        currentCheck = 1;
                        spins++;
                        _spinsTx.text = "GIROS: " + spins;
                    }
                    break;*/

        }
        
            
    }

}
