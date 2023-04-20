using System.Collections.Generic;
using UnityEngine;

public class BombermanStats: MonoBehaviour
{
    public delegate void OnBombIncrease();
    public static OnBombIncrease onBombIncrease;
    
    private int _bombs = 1;
    public int Lives { get; set; } = 1;

    public int Bombs
    {
        get => _bombs;
        set
        {
            _bombs = value; 
            onBombIncrease?.Invoke();
        }
    }

    public int Speed { get; set; } = 6;
    public bool Kick { get; set; } = false;
    public bool Curse { get; set; } = false;
    public float CurseTimer { get; set; } = 0;
    public BombStats BombStats { get; set; }
    
    private void Start()
    {
        BombStats = new BombStats();
    }
}