using System;
using System.Collections.Generic;
using System.Linq;
using PickUps.Curses;
using TRYINGSTUFFOUT.CursesV2;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utils
{
    public class FinalBombermanStats : MonoBehaviour
    {
        [FormerlySerializedAs("bombermanStatsV3So")] [SerializeField] private BombermanStatsSO bombermanStatsSo;
        [FormerlySerializedAs("maxBombermanStatsV3So")] [SerializeField] private BombermanStatsSO maxBombermanStatsSo;
        [FormerlySerializedAs("minBombermanStatsV3So")] [SerializeField] private BombermanStatsSO minBombermanStatsSo;
        
        public List<CurseModifier> curses;

        public IDictionary<Stats, float> numericStats;
        public IDictionary<Stats, bool> boolStats;
        
        private void Awake()
        {
            numericStats = new Dictionary<Stats, float>();
            boolStats = new Dictionary<Stats, bool>();

            numericStats.Add(Stats.Lives, bombermanStatsSo.lives);
            
            foreach (var baseStat in bombermanStatsSo.BombermanStats)
            {
                if(baseStat.isBoolean)
                    boolStats.Add(baseStat.stat, GetBoolFromFloat(baseStat.value));
                else
                    numericStats.Add(baseStat.stat, baseStat.value);
            }
        }
        
        private bool GetBoolFromFloat(float value)
        {
            if (value == 0)
                return false;
            return true;
        }
        
        public bool GetBooleanStat(Stats booleanStat)
        {
            foreach (var curse in curses)
            {
                if (booleanStat == curse.stat)
                    return GetBoolFromFloat(curse.value);
            }
            return boolStats[booleanStat];
        }

        public float GetNumericStat(Stats numericStat)
        {
            var baseStatValue = numericStats[numericStat];
            float finalStat = 0f;
            
            foreach (var curse in curses)
            {
                if (curse.stat == numericStat)
                {
                    finalStat = FinalStatCalculation(curse, baseStatValue);
                    return MinMaxStatsCheck(numericStat, finalStat);
                }
            }
            return MinMaxStatsCheck(numericStat, baseStatValue);
        }

        float FinalStatCalculation(CurseModifier curseModifier, float baseStat)
        {
            switch (curseModifier.modifierType)
            {
                case CurseModifier.StatModifyingType.Additive : return baseStat + curseModifier.value;
                case CurseModifier.StatModifyingType.Flat : return curseModifier.value;
                case CurseModifier.StatModifyingType.Percentage : return baseStat * curseModifier.value;
            }
            return 0;
        }

        float MinMaxStatsCheck(Stats numericStat, float statValue)
        {
            if (numericStat == Stats.Lives)
                return numericStats[numericStat];
            
            var minStat = minBombermanStatsSo.BombermanStats.First(a => a.stat == numericStat);
            var maxStat = maxBombermanStatsSo.BombermanStats.First(a => a.stat == numericStat);
            
            if(statValue  >= maxStat.value)
                return maxStat.value;
            if (statValue <= minStat.value)
                return minStat.value;
            return statValue;
        }
    }
}