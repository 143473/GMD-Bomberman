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
    [SerializeField] private TextMeshProUGUI volume;
    [SerializeField] private GameSettings gameSettings;

    private Slider livesSlider;
    private Slider aiPlayersSlider;
    private Slider humanPlayersSlider;
    private Slider volumeSlider;
    

    private void Awake()
    {
        livesSlider = GameObject.Find("LivesSlider").GetComponent<Slider>();
        aiPlayersSlider = GameObject.Find("AIPlayersSlider").GetComponent<Slider>();
        humanPlayersSlider = GameObject.Find("PlayersSlider").GetComponent<Slider>();
        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();

        livesSlider.value = gameSettings.playerLivesToStartWith;
        aiPlayersSlider.value = gameSettings.numberOfAIPlayers;
        humanPlayersSlider.value = gameSettings.numberOfHumanPlayers;
        volumeSlider.value = gameSettings.volume;
        
        numberOfLives.text = $"{(int)livesSlider.value}";
        numberOfPlayers.text = $"{(int)humanPlayersSlider.value}";
        numberOfAI.text = $"{(int)aiPlayersSlider.value}";
        volume.text = $"{(int)(volumeSlider.value*100)}%";
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
    
    public void SetVolume()
    {
        volume.text = $"{(int)(volumeSlider.value*100)}%";
        gameSettings.volume = volumeSlider.value;
        AudioListener.volume = gameSettings.volume;
    }
}
