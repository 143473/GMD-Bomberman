using System.Collections.Generic;
using TRYINGSTUFFOUT.CursesV2;

namespace Utils
{
    public class StatsHandler
    {
        private BombermanStatsV2SO bombermanStats;
        private FinalBombermanStatsV2 finalBombermanStatsV2;
        private IDictionary<Stats, float> numericStats;
        private IDictionary<Stats, bool> boolStats;
        private List<CurseModifier> curses;

        public StatsHandler(FinalBombermanStatsV2 finalBombermanStatsV2)
        {
            this.finalBombermanStatsV2 = finalBombermanStatsV2;
            
            bombermanStats = this.finalBombermanStatsV2.bombermanStats;
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

            if (curseModifier.statValueType == CurseModifier.ValueType.Numeric)
                numericStats[curseModifier.stat] *= curseModifier.value;
            else
                boolStats[curseModifier.stat] = GetBoolFromFloat(curseModifier.value);
        }

        public void RemoveCurseModifier(CurseModifier curseModifier)
        {
            if (curseModifier.statValueType == CurseModifier.ValueType.Numeric)
                numericStats[curseModifier.stat] /= curseModifier.value;
            else
                boolStats[curseModifier.stat] = !boolStats[curseModifier.stat];
            
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
            
            SaveStat(stat);
            //SaveStats();
        }

        private void SaveStats()
        {
            bombermanStats.bombs = numericStats[Stats.Bombs];
            bombermanStats.lives = numericStats[Stats.Lives];
            bombermanStats.flame = numericStats[Stats.Flame];
            bombermanStats.speed = numericStats[Stats.Speed];
            bombermanStats.bombDelay = numericStats[Stats.BombDelay];
            
            bombermanStats.kick = boolStats[Stats.Kick];
            bombermanStats.cursed = boolStats[Stats.Cursed];
            bombermanStats.nasty = boolStats[Stats.Nasty];
            bombermanStats.remoteExplosion = boolStats[Stats.RemoteExplosion];
            bombermanStats.inverseControls = boolStats[Stats.InverseControls];
        }

        private void SaveStat(Stats stat)
        {
            foreach (var param in typeof(BombermanStatsV2SO).GetProperties())
            {
                if(param.Name.Equals(stat.ToString()))
                    param.SetValue(bombermanStats, stat);
            }
        }
        
        public bool GetBoolFromFloat(float value)
        {
            if (value == 0)
                return false;
            return true;
        }
    }
}