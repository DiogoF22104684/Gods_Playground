using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveFunction : ScriptableObject
{
    public abstract void Function(BattleEntity attacker, BattleEntity target,
        float roll);
}
