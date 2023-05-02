using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TRYINGSTUFFOUT.CursesV2;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace PickUps.Curses
{
    public class Curse : MonoBehaviour
    {
        public CurseModifier appliedCurse;

        private FinalBombermanStats finalBombermanStats;

        private StatsHandler statsHandler;

        private void Awake()
        {
            finalBombermanStats = gameObject.GetComponent<FinalBombermanStats>();
            statsHandler = new StatsHandler(finalBombermanStats);
        }

        private void Start()
        {
            var exists = statsHandler.CheckForModifierAppliedToTheSameStat(appliedCurse);
            
            if (exists) 
                Destroy(this);
            else
            {
                statsHandler.AddCurseModifier(appliedCurse); 
                StartCoroutine(SelfDestroy());
            }
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(appliedCurse.timer);
            
            statsHandler.RemoveCurseModifier(appliedCurse);
            
            Destroy(this);
        }
    }
}