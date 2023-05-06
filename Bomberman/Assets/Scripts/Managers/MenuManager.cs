using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] 
    private int startScene;

    public void StartScene()
    {
        SceneManager.LoadScene(startScene);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
