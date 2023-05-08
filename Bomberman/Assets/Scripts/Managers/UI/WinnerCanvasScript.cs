using System;
using System.Collections.Generic;
using TRYINGSTUFFOUT.CursesV2.ScriptableObjects;
using UnityEngine;

namespace Managers.UI
{
    public class WinnerCanvasScript : MonoBehaviour
    {
        private List<string> deadPile;
        [SerializeField] private GameSettings gameSettings;
        private int totalPlayers;
        [SerializeField] private Canvas winnerCanvas;
        private void Awake()
        {
            PlayerManager.onDeadPlayer += MarkDeadBomberman;
            deadPile = new List<string>();
            totalPlayers = gameSettings.numberOfHumanPlayers + gameSettings.numberOfAIPlayers;
        }

        private void MarkDeadBomberman(GameObject deadBomberman)
        {
            if (deadPile.Count < totalPlayers-1)
            {
                deadPile.Add(deadBomberman.name);
            }
            else
            {
                // Set winner canvas
            }
        }
    }
}