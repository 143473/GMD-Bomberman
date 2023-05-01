// using System.Collections.Generic;
// using UnityEngine;
//
// namespace PickUps.Curses
// {
//     public class NoBombs : Curse
//     {
//         private void Awake()
//         {
//             var stats = gameObject.GetComponent<BombermanStats>();
//             Flame = stats.Flame;
//             Speed = 10;
//             BombDelay = stats.BombDelay;
//             Nasty = stats.Nasty;
//             Bombs = new List<GameObject>();
//             StartCoroutine(SelfDestroy());
//         }
//
//         private void Start()
//         {
//
//         }
//     }
// }