using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PickUps.Curses
{
    public class Curse : MonoBehaviour
    {
        [SerializeField] public float curseTimer = 10;
        public int Speed { get; set; }
        public int Flame { get; set; }
        public float BombDelay { get; set; }
        public List<GameObject> Bombs { get; set; }
        public bool Nasty { get; set; }

        protected IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(curseTimer);
            gameObject.GetComponent<BombermanStats>().Cursed = false;
            Destroy(this);
        }

        // protected void SetCurseDefaultParameters()
        // {
        //     var stats = gameObject.GetComponent<BombermanStats>();
        //     Flame = stats.Flame;
        //     Speed = stats.Speed;
        //     AllowMultiple = stats.AllowMultiple;
        //     BombDelay = stats.Bombs;
        //     Nasty = stats.Nasty;
        //
        //     var inventory = gameObject.GetComponent<BombsInventory>();
        //     Bombs = inventory.Bombs;
        // }

        public float CurseResetTimer()
        {
            return 10;
        }
    }
}