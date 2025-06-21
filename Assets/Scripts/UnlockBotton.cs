using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockBotton : MonoBehaviour
{
    [Header("Costo de desbloqueo")]
    public int unlockCost = 100;

    [Header("Referencias UI")]
    public Button unlockButton;            
    public Button upgradeButton;            
    public TMP_Text unlockCostText;
    public GameObject imagen;
    [Header("ID único para guardado")]
    public string unlockID = "Upgrade1Unlocked";

    private void Start()
    {
       
        if (PlayerPrefs.GetInt(unlockID, 0) == 1)
        {
            imagen.SetActive(false);
            unlockButton.gameObject.SetActive(false);
            upgradeButton.interactable = true;
        }
        else
        {
            
            if (unlockCostText != null)
                unlockCostText.text = unlockCost.ToString();

            upgradeButton.interactable = false;
            unlockButton.onClick.AddListener(TryUnlockUpgrade);
        }
    }

    private void TryUnlockUpgrade()
    {
        if (GameManager.Instance.count >= unlockCost)
        {
            GameManager.Instance.count -= unlockCost;
            GameManager.Instance.RefreshUI();

            PlayerPrefs.SetInt(unlockID, 1); 
            PlayerPrefs.Save();
            imagen.SetActive(false);
            unlockButton.gameObject.SetActive(false);
            upgradeButton.interactable = true;
        }
        else
        {
            Debug.Log("No hay suficientes puntos para desbloquear esta mejora.");
        }
    }
}
