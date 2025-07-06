using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class upgradeDecorator : upgradeBase
{
    protected upgradeBase _upgradeBase;

    public upgradeDecorator(upgradeBase upgrd)
    {
        _upgradeBase = upgrd;
    }
}
