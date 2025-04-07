using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spinDetect : MonoBehaviour
{

    [SerializeField] int currentCheck = 1;
    [SerializeField] static public int spins = 0;
    [SerializeField] private TMP_Text _spinsTx;

    // Start is called before the first frame update
    void Start()
    {
        currentCheck = 1;
        _spinsTx.text = "GIROS: " + spins;
    }

    // Update is called once per frame
    void Update()
    {
        
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
