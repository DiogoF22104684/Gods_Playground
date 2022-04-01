using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem
{
    [Serializable]
    public struct MoveConfig
    {
        [SerializeField]
        BattlePropertyInfo costStat;
        [SerializeField]
        int costValue;

        [SerializeField]
        SelectorMode mode;
        [SerializeField]
        SelectorType type;
        [SerializeField]
        MechanicType mechanic;
        public SelectorMode Mode => mode;
        public SelectorType Type => type;

        public BattlePropertyInfo CostStat => costStat;
        public int CostValue => costValue;

        public MechanicType Mechanic => mechanic;
    }
}
