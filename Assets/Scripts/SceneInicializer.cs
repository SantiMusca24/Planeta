using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInicializer : MonoBehaviour
{
    [SerializeField] private ContadorUI contadorUI;
    [SerializeField] private UpgradeManager[] upgradeManagers;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private UpgradeManager2[] upgradeManagers2;
    //[SerializeField] private GameManager gameManager;

    void Start()
    {
        
        GameManager.Instance.SetUIManager(contadorUI); 
        GameManager.Instance.RefreshUI();
        GameManager.Instance.RefreshUpgradeManagers();
        foreach (var upgradeManager in upgradeManagers)
        {
            upgradeManager.AssignGameManager(GameManager.Instance);
        }
        GameManager.Instance.RefreshUpgradeManagers2();
        foreach (var upgradeManager2 in upgradeManagers2)
        {
            upgradeManager2.AssignGameManager(GameManager.Instance);
        }

    }
}
