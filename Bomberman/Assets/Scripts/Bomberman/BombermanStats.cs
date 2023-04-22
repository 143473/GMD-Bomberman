using System;
using System.Collections.Generic;
using PickUps.Curses;
using UnityEngine;

public class BombermanStats: MonoBehaviour
{
    public delegate void OnBombIncrease();
    public static OnBombIncrease onBombIncrease;
    
    private int _bombs = 1;
    private int _speed = 6;
    
    private BombStats _bombStats;
    public int Lives { get; set; } = 1;

    public int Bombs
    {
        get
        {
            // if (Curse)
            //     return GetComponent<Curse>().CursedBombsInventory.Bombs.Count;
            return _bombs;
        }
        set
        {
            _bombs = value;
            onBombIncrease?.Invoke();
        }
    }

    public int Speed
    {
        get
        {
            if (Curse)
                return GetComponent<Curse>().Speed;
            return _speed;
        }
        set => _speed = value;
    }

    public bool Kick { get; set; } = false;
    public bool Curse { get; set; } = false;

    public BombStats BombStats
    {
        get
        {
            // if (Curse)
            //     return GetComponent<Curse>().CursedBombStats;
            return _bombStats;
        }
        set => _bombStats = value;
    }

    private void Start()
    {
        BombStats = gameObject.AddComponent<BombStats>();
    }
}