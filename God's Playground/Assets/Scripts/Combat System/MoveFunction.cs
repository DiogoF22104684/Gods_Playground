using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveFunction : ScriptableObject
{
    public abstract void Function(BattleEntity attacker, IEnumerable<BattleEntity> target,
        float roll);
}
