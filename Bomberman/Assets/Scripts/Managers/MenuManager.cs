using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas optionsCanvas;
    public void LoadScene(int targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnOptionsMenu()
    {
        menuCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(true);
    }

    public void OnMainMenu()
    {
        optionsCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }
}
