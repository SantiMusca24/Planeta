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

    public static event Action OnPlanetRotated;
    void Start()
    {
        currentCheck = 1;
        _spinsTx.text = "GIROS: " + spins;
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
                    _spinsTx.text = "GIROS: " + spins;
                    OnPlanetRotated?.Invoke();
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
