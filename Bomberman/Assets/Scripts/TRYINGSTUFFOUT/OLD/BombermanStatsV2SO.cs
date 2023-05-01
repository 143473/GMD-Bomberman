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
        
        public float lives = 1; 
        public float bombs = 1;
        public float speed = 6;
        public float flame = 1;
        public float bombDelay = 3;
        
        public bool kick = false;
        public bool cursed = false;
        public bool nasty = false;
        public bool remoteExplosion = false;
        public bool inverseControls = false;
    }
}