using System;
using UnityEngine;

public class BombermanStats: MonoBehaviour
{
    public int Lives { get; set; } = 1;
    public int Bombs { get; set; } = 1;
    public int Explosion { get; set; } = 1;
    public int Speed { get; set; } = 6;
    public bool RemoteExplosion { get; set; } = false;
    public bool AllowMultiple { get; set; } = false;
    public bool Kick { get; set; } = false;
    public bool Curse { get; set; } = false;
    public float CurseTimer { get; set; } = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Curse)
            ApplyCurseTimer();
    }

    void ApplyCurseTimer()
    {
        Curse = false;
        CurseTimer = 10;
        CurseTimer -= Time.deltaTime;
    }
}