using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBuilder : MonoBehaviour
{

    private List<UpgradeManager.LevelUnlockObject> unlocks;
    private int currentLevel;

    public UpgradeBuilder WithUnlocks(List<UpgradeManager.LevelUnlockObject> unlocksList)
    {
        Debug.Log("skibiditoilet2");
        unlocks = unlocksList;
        return this;
    }

    public UpgradeBuilder AtLevel(int level)
    {
        Debug.Log("skibiditoilet3");
        currentLevel = level;
        return this;
    }

    public void Build()
    {
        Debug.Log("skibiditoilet4");
        foreach (var unlock in unlocks)
        {
            if (unlock.objectToActivate != null)
            {
                unlock.objectToActivate.SetActive(currentLevel >= unlock.requiredLevel);
            }
        }
    }
}
