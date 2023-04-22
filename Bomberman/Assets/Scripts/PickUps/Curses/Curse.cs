using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PickUps.Curses
{
    public class Curse : MonoBehaviour
    {
        public int Speed { get; set; }
        public int Flame { get; set; }
        public float BombDelay { get; set; }
        public bool AllowMultiple { get; set; }
        public List<GameObject> Bombs { get; set; }
        public bool Nasty { get; set; }

        protected IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(10);
            gameObject.GetComponent<BombermanStats>().Cursed = false;
            Destroy(this);
        }
    }
}