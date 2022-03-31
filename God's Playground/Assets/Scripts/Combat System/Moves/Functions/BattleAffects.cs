using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

[System.Serializable]
public abstract class BattleAffects 
{
    public abstract void Function(BattleEntity attacker,
            IEnumerable<BattleEntity> target, float roll);
}
