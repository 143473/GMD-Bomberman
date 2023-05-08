using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using TRYINGSTUFFOUT.CursesV2.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfLives;
    [SerializeField] private TextMeshProUGUI numberOfPlayers;
    [SerializeField] private TextMeshProUGUI numberOfAI;

    [SerializeField] private GameSettings gameSettings;

    private Slider livesSlider;
    private Slider aiPlayersSlider;
    private Slider humanPlayersSlider;
    

    private void Awake()
    {
        livesSlider = GameObject.Find("LivesSlider").GetComponent<Slider>();
        aiPlayersSlider = GameObject.Find("AIPlayersSlider").GetComponent<Slider>();
        humanPlayersSlider = GameObject.Find("PlayersSlider").GetComponent<Slider>();

        livesSlider.value = gameSettings.playerLivesToStartWith;
        aiPlayersSlider.value = gameSettings.numberOfAIPlayers;
        humanPlayersSlider.value = gameSettings.numberOfHumanPlayers;
        
        numberOfLives.text = $"{(int)livesSlider.value}";
        numberOfPlayers.text = $"{(int)humanPlayersSlider.value}";
        numberOfAI.text = $"{(int)aiPlayersSlider.value}";
    }

    public void SetNumberOfLives()
    {
        numberOfLives.text = $"{livesSlider.value}";
        gameSettings.playerLivesToStartWith = (int)livesSlider.value;

    }
    public void SetNumberOfPlayers()
    {
        numberOfPlayers.text = $"{humanPlayersSlider.value}";
        gameSettings.numberOfHumanPlayers = (int)humanPlayersSlider.value;
    }
    
    public void SetNumberOfAI()
    {
        numberOfAI.text = $"{aiPlayersSlider.value}";
        gameSettings.numberOfAIPlayers = (int)aiPlayersSlider.value;
    }
    
    
}
