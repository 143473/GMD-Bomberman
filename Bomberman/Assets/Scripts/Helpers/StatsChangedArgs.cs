using System;
using Unity.VisualScripting;
using UnityEngine.Events;

namespace Utils
{
    public class StatsChangedArgs : UnityEvent<string, Stats, bool>
    {
        public string PlayerName { get; set; }
        public Stats Stat { get; set; }
        public float NumericStatValue { get; set; }
        public bool BooleanStatValue { get; set; }
        public bool IsCurse { get; set; }

        public StatsChangedArgs(string playerName = null, Stats stat = default, float numericStatValue = default, bool booleanStatValue = default, bool isCurse = default)
        {
            PlayerName = playerName;
            Stat = stat;
            NumericStatValue = numericStatValue;
            BooleanStatValue = booleanStatValue;
            IsCurse = isCurse;
        }
    }
}