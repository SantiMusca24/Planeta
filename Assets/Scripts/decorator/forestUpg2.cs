using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestUpg2 : upgradeDecorator
{
    public static BasicUpgradeManager _upgradeManager1;
    public static BasicUpgradeManager _upgradeManager2;
    public static BasicUpgradeManager _upgradeManager3;


    public forestUpg2(upgradeBase upgrd) : base(upgrd)
    {
    }

    public override float Inc()
    {
        return 1 + ((_upgradeManager3.levelForestRedundant * 0.1f) * (_upgradeManager1.levelForestRedundant + _upgradeManager2.levelForestRedundant));
    }
}
