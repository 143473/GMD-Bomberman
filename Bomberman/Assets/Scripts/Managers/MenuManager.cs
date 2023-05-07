using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void LoadScene(int targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
