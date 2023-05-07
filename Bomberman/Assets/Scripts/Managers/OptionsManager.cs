using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfLives;
    [SerializeField] private TextMeshProUGUI numberOfPlayers;
    [SerializeField] private TextMeshProUGUI numberOfAI;

    public void SetNumberOfLives(float lives)
    {
        numberOfLives.text = $"{lives}";
    }
    public void SetNumberOfPlayers(float players)
    {
        numberOfPlayers.text = $"{players}";
    }
    
    public void SetNumberOfAI(float aiplayers)
    {
        numberOfAI.text = $"{aiplayers}";
    }
}
