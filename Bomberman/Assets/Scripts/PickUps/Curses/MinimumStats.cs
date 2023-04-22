using System;
using System.Collections.Generic;
using PickUps.Curses;
using UnityEditor;
using UnityEngine;


public class MinimumStats : Curse
{
    private void Awake()
    {
        var stats = gameObject.GetComponent<BombermanStats>();
        Flame = 1;
        Speed = 2;
        AllowMultiple = false;
        BombDelay = stats.BombDelay;
        Nasty = stats.Nasty;

        var inventory = gameObject.GetComponent<BombsInventory>();
        Bombs = inventory.Bombs;
        StartCoroutine(SelfDestroy());
    }

    private void Start()
    {

    }
}