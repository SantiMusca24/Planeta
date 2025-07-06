using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestUpg1 : upgradeDecorator
{
    [SerializeField] public static BasicUpgradeManager _upgradeManager1;
    public forestUpg1(upgradeBase upgrd) : base(upgrd)
    {
    }

    public override float Inc()
    {
        return 1 + _upgradeManager1.levelForestRedundant;
    }

}
