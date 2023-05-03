using System;
using System.Collections.Generic;
using System.Linq;
using TRYINGSTUFFOUT.CursesV2;
using UnityEngine;

namespace Utils
{
    public class StatsHandler
    {
        private FinalBombermanStats finalBombermanStats;
        private IDictionary<Stats, float> numericStats;
        private IDictionary<Stats, bool> boolStats;
        private List<CurseModifier> curses;
        private string playerName;
        
        public static event EventHandler<StatsChangedArgs> StatsChanged = delegate(object sender, StatsChangedArgs args) {  };  

        public StatsHandler(FinalBombermanStats finalBombermanStats)
        {
            this.finalBombermanStats = finalBombermanStats;
            
            numericStats = this.finalBombermanStats.numericStats;
            boolStats = this.finalBombermanStats.boolStats;
            curses = this.finalBombermanStats.curses;
            playerName = finalBombermanStats.gameObject.name;
        }

        public bool CheckForModifierAppliedToTheSameStat(CurseModifier curseModifier)
        {
            var existingStatCurses = curses.FirstOrDefault(a => a.stat == curseModifier.stat);

            return existingStatCurses != null || curses.Contains(curseModifier);
        }
        
        public void AddCurseModifier(CurseModifier curseModifier)
        {
            if(!boolStats[Stats.Cursed]) 
                boolStats[Stats.Cursed] = true;

            curses.Add(curseModifier);
            
            var isBool = boolStats.ContainsKey(curseModifier.stat);
        }

        public void RemoveCurseModifier(CurseModifier curseModifier)
        {
            curses.Remove(curseModifier);
            
            if (curses.Count <= 0)
                boolStats[Stats.Cursed] = false;
        }

        public void AddPermanentStat(Stats stat, bool boolValue = default, float numericValue = default)
        {
            if (boolStats.ContainsKey(stat))
            {
                boolStats[stat] = boolValue;
                StatsChanged(this, new StatsChangedArgs(playerName, stat, booleanStatValue: finalBombermanStats.GetBooleanStat(stat)));
            }
                
            else if (numericStats.ContainsKey(stat))
            {
                numericStats[stat] += numericValue;
                StatsChanged(this, new StatsChangedArgs(playerName, stat, numericStatValue: finalBombermanStats.GetNumericStat(stat)));
            }
        }
    }
}