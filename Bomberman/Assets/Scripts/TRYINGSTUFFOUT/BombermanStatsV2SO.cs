using System;
using System.Collections.Generic;
using PickUps.Curses;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu]
    public class BombermanStatsV2SO: ScriptableObject
    {
        public string bombermanName;
        
        public float lives; 
        public float bombs;
        public float speed;
        public float flame;
        public float bombDelay;
        
        public bool kick;
        public bool cursed;
        public bool nasty;
        public bool remoteExplosion;
        public bool inverseControls;
    }
}