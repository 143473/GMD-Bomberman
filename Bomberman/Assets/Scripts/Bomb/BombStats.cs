using System;
using UnityEngine;

public class BombStats : MonoBehaviour
{
    public int Flame { get; set; } = 1;
    public bool RemoteExplosion { get; set; } = false;
    public bool AllowMultiple { get; set; } = false;
    public float BombDelay { get; set; } = 3;

    public void SetStats(int flame, bool remote, bool allowMultiple, float bombDelay)
    {
        Flame = flame;
        RemoteExplosion = remote;
        AllowMultiple = allowMultiple;
        BombDelay = bombDelay;
    }
}