using System;
using System.Collections.Generic;
using TRYINGSTUFFOUT.CursesV2.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class CanvasScript : MonoBehaviour
    {
        [SerializeField] private BombermanStatsSO bombermanStatsSo; //take default stats
        [SerializeField] private GameSettings gameSettings; //take number of players

        private void Start()
        {
            StatsHandler.StatsChanged += UpdateStuff;

            var totalPlayers = gameSettings.numberOfAIPlayers + gameSettings.numberOfHumanPlayers;
        }

        void UpdateStuff(object sender, StatsChangedArgs e)
        {
            if (e.NumericStatValue == default)
                Debug.Log(e.PlayerName + " " + e.Stat + " = " + e.BooleanStatValue + "--IsCursed:" + e.IsCurse);
            else
            {
                Debug.Log(e.PlayerName + " " + e.Stat + " = " + e.NumericStatValue + "--IsCursed:" + e.IsCurse);
            }
        }

    }
}
