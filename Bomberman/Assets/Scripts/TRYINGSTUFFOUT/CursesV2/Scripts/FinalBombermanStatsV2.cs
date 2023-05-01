using System;
using System.Collections.Generic;
using System.Linq;
using PickUps.Curses;
using TRYINGSTUFFOUT.CursesV2;
using Unity.VisualScripting;
using UnityEngine;

namespace Utils
{
    public class FinalBombermanStatsV2 : MonoBehaviour
    {
        [SerializeField] private BombermanStatsV3SO bombermanStatsV3So;
        [SerializeField] private BombermanStatsV3SO maxBombermanStatsV3So;
        [SerializeField] private BombermanStatsV3SO minBombermanStatsV3So;
        
        public List<CurseModifier> curses;

        public IDictionary<Stats, float> numericStats;
        public IDictionary<Stats, bool> boolStats;
        
        private void Awake()
        {
            numericStats = new Dictionary<Stats, float>();
            boolStats = new Dictionary<Stats, bool>();

            numericStats.Add(Stats.Lives, bombermanStatsV3So.lives);
            
            foreach (var baseStat in bombermanStatsV3So.BombermanStats)
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
            var minStat = minBombermanStatsV3So.BombermanStats.First(a => a.stat == numericStat);
            var maxStat = maxBombermanStatsV3So.BombermanStats.First(a => a.stat == numericStat);
            
            foreach (var curse in curses)
            {
                if (curse.stat == numericStat)
                {
                    var finalStat = FinalStatCalculation(curse, baseStatValue);
                    if(finalStat  >= maxStat.value)
                        return maxStat.value;
                    if (finalStat <= minStat.value)
                        return minStat.value;
                    return finalStat;
                }
            }
            return baseStatValue;
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
    }
}