using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using TRYINGSTUFFOUT.CursesV2.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Utils
{
    public class CanvasScript : MonoBehaviour
    {
        [SerializeField] private BombermanStatsSO bombermanStatsSo; //take default stats
        [SerializeField] private GameSettings gameSettings; //take number of players
        [SerializeField] private TMP_Text playerText;
        [SerializeField] private Slider livesSlider;
        [SerializeField] private TMP_Text bombsText;
        [SerializeField] private TMP_Text flameText;
        [SerializeField] private TMP_Text speedText;
        [SerializeField] private TMP_Text flameMaxText;
        [SerializeField] private TMP_Text remoteText;
        [SerializeField] private Image curseImage;
        public IDictionary<Stats, float> numericStats;
        public IDictionary<Stats, bool> boolStats;
        
        private void Awake()
        {
            numericStats = new Dictionary<Stats, float>();
            boolStats = new Dictionary<Stats, bool>();

            numericStats.Add(Stats.Lives, bombermanStatsSo.lives);
            
            foreach (var baseStat in bombermanStatsSo.BombermanStats)
            {
                if(baseStat.isBoolean)
                    boolStats.Add(baseStat.stat, GetBoolFromFloat(baseStat.value));
                else
                    numericStats.Add(baseStat.stat, baseStat.value);
            }
        }
        
        private bool GetBoolFromFloat(float value)
        {
            if (value == 0)
                return false;
            return true;
        }
        private void Start()
        {
            livesSlider.maxValue = numericStats[Stats.Lives];
            flameText.text = $"{numericStats[Stats.Flame]}";
            bombsText.text = $"{numericStats[Stats.Bombs]}";
            curseImage.enabled = false;
            playerText.text = name[..8];
            
            StatsHandler.StatsChanged += UpdateStats;
        }
        
        void UpdateStats(object sender, StatsChangedArgs e)
        {  
            if (!name.Contains(e.PlayerName)) return;
            switch (e.Stat)
            {
                case Stats.Bombs:
                    bombsText.text =$"{e.NumericStatValue}";
                    break;
                case Stats.Lives:
                    livesSlider.value = e.NumericStatValue;
                    break;
                case Stats.Flame:
                    flameText.text = $"{e.NumericStatValue}";
                    break;
                case Stats.Cursed:
                    ShowCursedImage();
                    break;
                case Stats.Nasty:
                    ShowCursedImage();
                    break;
                case Stats.InverseControls:
                    ShowCursedImage();
                    break;
                case Stats.RemoteExplosion:
                    remoteText.text = "Remote!";
                    break;
                case Stats.Speed:
                    speedText.text = "Speed!";
                    break;
                default:
                    break;
            }
        }
        private void ShowCursedImage()
        {
            curseImage.enabled = true;
            Invoke(nameof(HideCursedImage), 10);
        }

        private void HideCursedImage()
        {
            curseImage.enabled = false;
        }
    }
}
