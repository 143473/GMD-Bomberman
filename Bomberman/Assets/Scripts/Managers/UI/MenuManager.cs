using System.Linq;
using Helpers;
using TRYINGSTUFFOUT.CursesV2.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas optionsCanvas;    
    [SerializeField] private GameObject frame;
    [SerializeField] private GameSettings gameSettings;
    private Canvas mCanvas;
    private Canvas oCanvas;
    
    void Start()
    { 
        mCanvas = Instantiate(menuCanvas, new Vector3(10,2,5), Quaternion.Euler(90,0,0));
        oCanvas = Instantiate(optionsCanvas, new Vector3(10,2,5), Quaternion.Euler(90,0,0));
        frame = Instantiate(frame, new Vector3(10,0,5), Quaternion.Euler(0,0,0));
        oCanvas.gameObject.SetActive(false);
        mCanvas.gameObject.SetActive(true);
        
        mCanvas.AddListenerToButton("PlayButton", LoadScene);
        mCanvas.AddListenerToButton("OptionsButton", OnOptionsMenu);
        oCanvas.AddListenerToButton("BackButton", OnMainMenu);
        
        AudioListener.volume = gameSettings.volume;
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
