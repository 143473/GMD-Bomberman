using System.Collections.Generic;
using PickUps.Curses;
using TRYINGSTUFFOUT.CursesV2;
using UnityEngine;

namespace Utils
{
    public class FinalBombermanStatsV2 : MonoBehaviour
    {
        public BombermanStatsV2SO bombermanStats;
        public List<CurseModifier> curses;

        public IDictionary<Stats, float> numericStats;
        public IDictionary<Stats, bool> boolStats;
        
        private void Awake()
        {
            bombermanStats = ScriptableObject.CreateInstance<BombermanStatsV2SO>();

            numericStats = new Dictionary<Stats, float>();
            boolStats = new Dictionary<Stats, bool>();

            numericStats.Add(Stats.Lives, bombermanStats.lives);
            numericStats.Add(Stats.Bombs, bombermanStats.bombs);
            numericStats.Add(Stats.Flame, bombermanStats.speed);
            numericStats.Add(Stats.Speed, bombermanStats.flame);
            numericStats.Add(Stats.BombDelay, bombermanStats.bombDelay);
            
            boolStats.Add(Stats.Kick, bombermanStats.kick);
            boolStats.Add(Stats.Cursed, bombermanStats.cursed);
            boolStats.Add(Stats.Nasty, bombermanStats.nasty);
            boolStats.Add(Stats.RemoteExplosion, bombermanStats.remoteExplosion);
            boolStats.Add(Stats.InverseControls, bombermanStats.inverseControls);
        }

        public bool GetBooleanStat(Stats booleanStat) => boolStats[booleanStat];

        public float GetNumericStat(Stats numericStat) => numericStats[numericStat];
        
    }
}