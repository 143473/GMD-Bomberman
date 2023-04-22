using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PickUps.Curses
{
    public class Curse : MonoBehaviour
    {
        public int Speed { get; set; }


        protected IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(10);
            gameObject.GetComponent<BombermanStats>().Curse = false;
            Destroy(this);
        }
    }
}