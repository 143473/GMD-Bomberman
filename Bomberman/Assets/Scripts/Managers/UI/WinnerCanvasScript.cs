using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TMPro;
using TRYINGSTUFFOUT.CursesV2.ScriptableObjects;
using UnityEngine;

namespace Managers.UI
{
    public class WinnerCanvasScript : MonoBehaviour
    {
       
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private Canvas winnerCanvas;
        private TMP_Text winnerText;
        private List<string> deadPile;
        private int totalPlayers;
        private void Awake()
        {
            PlayerManager.onDeadPlayer += MarkDeadBomberman;
            deadPile = new List<string>();
            totalPlayers = gameSettings.numberOfHumanPlayers + gameSettings.numberOfAIPlayers;
            winnerCanvas = Instantiate(winnerCanvas, new Vector3(10, 3, 5), Quaternion.Euler(90,0,0));
            winnerCanvas.enabled = false;
            winnerText = GameObject.Find("WinnerText").GetComponent<TMP_Text>();
            winnerCanvas.AddListenerToButton("ExitButton", ExitGame);

        }

        private void MarkDeadBomberman(GameObject deadBomberman)
        {
            if (deadPile.Count < totalPlayers-1)
            {
                deadPile.Add(deadBomberman.name);
            }
            else
            {   
                var winner = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault(a => isActiveAndEnabled);
                winnerText.text = $"{winner.name} Won!";
                winnerCanvas.enabled = true;
            }
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}