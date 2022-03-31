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
        SelectorMode mode;
        [SerializeField]
        SelectorType type;
        [SerializeField]
        MechanicType mechanic;
        public SelectorMode Mode => mode;
        public SelectorType Type => type;

        public MechanicType Mechanic => mechanic;
    }
}
