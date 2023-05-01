using System;
using System.Collections.Generic;
using System.Linq;
using PickUps.Curses;
using TRYINGSTUFFOUT.CursesV2;
using Unity.VisualScripting;
using UnityEngine;

namespace Utils
{
    public class FinalBombermanStatsV2OLISTS : MonoBehaviour
    {
        [SerializeField] private BombermanStatsV3SO bombermanStatsV3So;
        [SerializeField] private BombermanStatsV3SO maxBombermanStatsV3So;
        [SerializeField] private BombermanStatsV3SO minBombermanStatsV3So;
        
        public List<CurseModifier> curses;
        public List<ModifiableBaseStat> baseStats;

        private void Awake()
        {
            foreach (var baseStat in bombermanStatsV3So.BombermanStats)
            {
                baseStats.Add(new ModifiableBaseStat()
                {
                    isBoolean = baseStat.isBoolean,
                    stat = baseStat.stat,
                    value = baseStat.value
                });
            }
        }

        public float GetNumericStat(Stats numericStat)
        {
            var baseStat = baseStats.First(a => a.stat == numericStat);
            var minStat = minBombermanStatsV3So.BombermanStats.First(a => a.stat == numericStat);
            var maxStat = maxBombermanStatsV3So.BombermanStats.First(a => a.stat == numericStat);
            
            foreach (var curse in curses)
            {
                if (curse.stat == numericStat)
                {
                    var finalStat = FinalStatCalculation(curse, baseStat.value);
                    if(finalStat  >= maxStat.value)
                        return maxStat.value;
                    if (finalStat <= minStat.value)
                        return minStat.value;
                    return finalStat;
                }

            }
            return baseStat.value;
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
        
        public bool GetBooleanStat(Stats booleanStat)
        {
            var baseStat = baseStats.Find(a => a.stat == booleanStat);
            foreach (var curse in curses)
            {
                if (curse.stat == booleanStat)
                    if(baseStat.isBoolean)
                        return GetBoolFromFloat(curse.value);
            }
            return GetBoolFromFloat(baseStat.value);
        }
        
        private bool GetBoolFromFloat(float value)
        {
            if (value == 0)
                return false;
            return true;
        }

        public class ModifiableBaseStat
        {
            public Stats stat;
            public float value;
            public bool isBoolean;
        }
        
    }
}