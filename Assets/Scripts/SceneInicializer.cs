using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInicializer : MonoBehaviour
{
    [SerializeField] private ContadorUI contadorUI;
    [SerializeField] private UpgradeManager[] upgradeManagers;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        
        GameManager.Instance.SetUIManager(contadorUI); 
        GameManager.Instance.RefreshUI();
        GameManager.Instance.RefreshUpgradeManagers();
        foreach (var upgradeManager in upgradeManagers)
        {
            upgradeManager.AssignGameManager(GameManager.Instance);
        }

    }
}
