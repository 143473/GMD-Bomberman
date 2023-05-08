using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas optionsCanvas;
    private Canvas mCanvas;
    private Canvas oCanvas;
    
    void Start()
    { 
        mCanvas = Instantiate(menuCanvas, new Vector3(10,2,5), Quaternion.Euler(90,0,0));
        oCanvas = Instantiate(optionsCanvas, new Vector3(10,2,5), Quaternion.Euler(90,0,0));
        oCanvas.gameObject.SetActive(false);
        mCanvas.gameObject.SetActive(true);
        
        mCanvas.AddListenerToButton("PlayButton", LoadScene);
        mCanvas.AddListenerToButton("OptionsButton", OnOptionsMenu);
        //oCanvas.AddListenerToButton("SaveButton", LoadScene);
        oCanvas.AddListenerToButton("BackButton", OnMainMenu);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnOptionsMenu()
    {
        mCanvas.gameObject.SetActive(false);
        oCanvas.gameObject.SetActive(true);
    }

    public void OnMainMenu()
    {
        oCanvas.gameObject.SetActive(false);
        mCanvas.gameObject.SetActive(true);
    }
}
