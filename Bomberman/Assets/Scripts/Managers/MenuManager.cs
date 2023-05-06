using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private int startGame;
    [SerializeField] private int optionsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(startGame);
    }
    
    public void OpenOptions()
    {
        SceneManager.LoadScene(optionsMenu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
