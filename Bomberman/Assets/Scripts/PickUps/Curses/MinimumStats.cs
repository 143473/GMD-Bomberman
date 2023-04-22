using System;
using System.Collections.Generic;
using PickUps.Curses;
using UnityEditor;
using UnityEngine;


public class MinimumStats : Curse
{
    private void Awake()
    {
        StartCoroutine(SelfDestroy());
    }

    private void Start()
    {
        Speed = 1;
    }
}