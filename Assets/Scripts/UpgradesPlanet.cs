using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesPlanet : MonoBehaviour
{
    [System.Serializable]
    public class UpgradeLevelCheck
    {
        public string upgradeName;
        public int requiredLevel;
        public List<GameObject> objectsToActivate;
    }

    
    public List<UpgradeLevelCheck> checks;

    void Start()
    {
        foreach (var check in checks)
        {
            int savedLevel = PlayerPrefs.GetInt(check.upgradeName + "_Level", 0);
            bool shouldActivate = savedLevel >= check.requiredLevel;

            foreach (var obj in check.objectsToActivate)
            {
                if (obj != null)
                    obj.SetActive(shouldActivate);
            }
        }
    }
}
