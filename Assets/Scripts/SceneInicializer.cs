using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInicializer : MonoBehaviour
{
    [SerializeField] private ContadorUI contadorUI;
    [SerializeField] protected PlanetUI uiPlanet;
    [SerializeField] protected BosqueUI uiBosque;
    [SerializeField] protected PescaUI uiPesca;
    [SerializeField] protected GranjaUI uiGranja;
    [SerializeField] private UpgradeManager[] upgradeManagers;
    [SerializeField] private GameManager gameManager;

    [Header("1. Planet.")]
    [Header("2. Bosque.")]
    [Header("3. Pesca.")]
    [Header("4. Granja.")]
    [SerializeField] int sceneNum;

    [SerializeField] private UpgradeManager2[] upgradeManagers2;
    //[SerializeField] private GameManager gameManager;

    void Start()
    {
        Debug.Log("skibi");
        if (sceneNum == 1) GameManager.Instance.SetUIPlanet(uiPlanet);
        if (sceneNum == 2) GameManager.Instance.SetUIBosque(uiBosque);
        if (sceneNum == 3) GameManager.Instance.SetUIPesca(uiPesca);
        if (sceneNum == 4) GameManager.Instance.SetUIGranja(uiGranja);
        //GameManager.Instance.SetUIManager(contadorUI); 
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
