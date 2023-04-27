using System.Collections.Generic;
using System.Linq;
using TRYINGSTUFFOUT.CursesV2;
using UnityEngine;

namespace Utils
{
    public class StatsHandler
    {
        private FinalBombermanStatsV2 finalBombermanStatsV2;
        private IDictionary<Stats, float> numericStats;
        private IDictionary<Stats, bool> boolStats;
        private List<CurseModifier> curses;

        public StatsHandler(FinalBombermanStatsV2 finalBombermanStatsV2)
        {
            this.finalBombermanStatsV2 = finalBombermanStatsV2;
            
            numericStats = this.finalBombermanStatsV2.numericStats;
            boolStats = this.finalBombermanStatsV2.boolStats;
            curses = this.finalBombermanStatsV2.curses;
        }
        
        public void AddCurseModifier(CurseModifier curseModifier)
        {
            if(!boolStats[Stats.Cursed]) 
                boolStats[Stats.Cursed] = true;
            
            if(!curses.Contains(curseModifier))
                curses.Add(curseModifier);
            Debug.Log(curses.First(a => a.stat == curseModifier.stat).stat);
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
                boolStats[stat] = boolValue;
            else if (numericStats.ContainsKey(stat))
                numericStats[stat] += numericValue;
        }
    }
}