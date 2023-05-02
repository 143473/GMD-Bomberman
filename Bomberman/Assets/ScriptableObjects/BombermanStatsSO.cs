using System;
using System.Collections.Generic;
using PickUps.Curses;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu]
    public class BombermanStatsSO: ScriptableObject
    {
        public float lives = 1;

        [Serializable]
        public struct BaseStat
        {
            public Stats stat;
            public float value;
            public bool isBoolean;
        }
        
        public BaseStat[] BombermanStats = new []
        {
            new BaseStat(){stat = Stats.Bombs, value = 1, isBoolean = false},
            new BaseStat(){stat = Stats.Speed, value = 5, isBoolean = false},
            new BaseStat(){stat = Stats.Flame, value = 1, isBoolean = false},
            new BaseStat(){stat = Stats.BombDelay, value = 3, isBoolean = false},
            
            new BaseStat(){stat = Stats.Kick, value = 0, isBoolean = true},
            new BaseStat(){stat = Stats.Cursed, value = 0, isBoolean = true},
            new BaseStat(){stat = Stats.RemoteExplosion, value = 0, isBoolean = true},
            new BaseStat(){stat = Stats.InverseControls, value = 0, isBoolean = true},
            new BaseStat(){stat = Stats.Nasty, value = 0, isBoolean = true}
        };
    }
}