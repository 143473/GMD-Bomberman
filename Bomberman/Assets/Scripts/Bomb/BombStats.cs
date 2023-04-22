using System;
using UnityEngine;

public class BombStats : MonoBehaviour
{
    public int Flame { get; set; } = 1;
    public bool RemoteExplosion { get; set; } = false;
    public bool AllowMultiple { get; set; } = false;
    public bool Kickable { get; set; } = false;
    public float Delay { get; set; } = 3;

    public void SetStats(BombStats bombStats)
    {
        Flame = bombStats.Flame;
        RemoteExplosion = bombStats.RemoteExplosion;
        AllowMultiple = bombStats.AllowMultiple;
        Kickable = bombStats.Kickable;
        Delay = bombStats.Delay;
    }
}