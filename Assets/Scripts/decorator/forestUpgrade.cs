using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestUpgrade : MonoBehaviour
{
    [SerializeField] BasicUpgradeManager _upgradeManager1;
    [SerializeField] BasicUpgradeManager _upgradeManager2;
    [SerializeField] BasicUpgradeManager _upgradeManager3;
    public float incTotal;
    private void Start()
    {
        Invoke("CountStart", 0.5f);
        //CountStart();
    }    

    public void CountStart()
    {
        forestUpg2._upgradeManager1 = _upgradeManager1;
        forestUpg2._upgradeManager2 = _upgradeManager2;
        forestUpg2._upgradeManager3 = _upgradeManager3;
        upgradeBase thisUpgrade = new genUpgrade();

        Debug.Log("BASE UPGRADE: " + thisUpgrade.Inc());

        thisUpgrade = new forestUpg2(thisUpgrade);

        Debug.Log("FULL UPGRADE: " + thisUpgrade.Inc());
        incTotal = thisUpgrade.Inc();
    }

    
}
