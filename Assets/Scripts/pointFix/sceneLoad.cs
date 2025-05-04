using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoad : MonoBehaviour
{
    static public bool planetScene;
    [SerializeField] private UpgradeManager2[] upgradeManagers2;
    [SerializeField] private GameManager gameManager;
    //public Skibidi1 

    // Start is called before the first frame update
    void Start()
    {
        // Find all existing UpgradeManager2 components in the scene (including those from DontDestroyOnLoad)
        //upgradeManagers2 = FindObjectsOfType<UpgradeManager2>();


        /*GameManager.Instance.RefreshUpgradeManagers2();
        foreach (var upgradeManager2 in upgradeManagers2)
        {
            upgradeManager2.AssignGameManager(GameManager.Instance);
        }*/
        planetScene = true;
        Debug.Log("planetScene true");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
