// using System;
// using System.Collections.Generic;
// using PickUps.Curses;
// using UnityEngine;
//
// public class BombermanStatsOLD: MonoBehaviour
// {
//     public delegate void OnBombIncrease(string name);
//     public static OnBombIncrease onBombIncrease;
//
//     public delegate void OnCursedBomberman();
//     public static OnCursedBomberman onCursedBomberman;
//     
//     private int _bombs = 1;
//     private int _speed = 6;
//     private int _flame = 1;
//     private float _bombDelay = 3;
//     private bool _cursed = false;
//     private bool _nasty = false;
//
//     public string Name { get; set; }
//     public int Lives { get; set; } = 1;
//
//     public int Bombs
//     {
//         get
//         {
//             if (Cursed) 
//                 return GetComponent<Curse>().Bombs.Count;
//             return _bombs;
//         }
//         set
//         {
//             _bombs = value;
//             onBombIncrease?.Invoke(name);
//         }
//     }
//
//     public int Speed
//     {
//         get
//         {
//             if (Cursed)
//                 return GetComponent<Curse>().Speed;
//             return _speed;
//         }
//         set => _speed = value;
//     }
//
//     public bool Kick { get; set; } = false;
//
//     public bool Cursed
//     {
//         get => _cursed;
//         set
//         {
//             if(value)
//                 onCursedBomberman?.Invoke();
//             _cursed = value;
//         }
//     }
//
//     public bool Nasty
//     {
//         get
//         {
//             if (Cursed)
//                 return GetComponent<Curse>().Nasty;
//             return false;
//         }
//         set => _nasty = value;
//     }
//
//     public int Flame {         
//         get
//         {
//             if (Cursed)
//                 return GetComponent<Curse>().Flame;
//             return _flame;
//         }
//         set => _flame = value;}
//     public bool RemoteExplosion { get; set; } = false;
//
//     public float BombDelay
//     {
//         get
//         {
//             if (Cursed)
//                 return GetComponent<Curse>().BombDelay;
//             return _bombDelay;
//         }
//         set => _bombDelay = value;
//     }
// }