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
    public class CurseV2 : MonoBehaviour
    {
        public CurseModifier appliedCurse;

        private FinalBombermanStatsV2 finalBombermanStats;

        private StatsHandler statsHandler;

        private void Awake()
        {
            finalBombermanStats = gameObject.GetComponent<FinalBombermanStatsV2>();
            statsHandler = new StatsHandler(finalBombermanStats);

            statsHandler.AddCurseModifier(appliedCurse);
            
            StartCoroutine(SelfDestroy());
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(appliedCurse.timer);
            
            statsHandler.RemoveCurseModifier(appliedCurse);
            
            Destroy(this);
        }
    }
}