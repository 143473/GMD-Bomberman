using System;
using PickUps.Curses;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bomberman
{
    public class CurseInteraction : MonoBehaviour
    {
        protected GameObject bomberman;

        private void Awake()
        {
            bomberman = gameObject;
        }
        
        // private void OnTriggerEnter(Collider bomberman)
        // {
        //     if (gameObject.GetComponent<BombermanStats>().Cursed)
        //     {
        //         var curse = gameObject.GetComponent<Curse>();
        //         if (bomberman.gameObject.tag.Equals("Player") && !bomberman.gameObject.GetComponent<BombermanStats>().Cursed)
        //         {
        //             bomberman.gameObject.AddComponent(curse.GetType());
        //             bomberman.GetComponent<BombermanStats>().Cursed = true;
        //         }
        //     }
        // }
    }
}