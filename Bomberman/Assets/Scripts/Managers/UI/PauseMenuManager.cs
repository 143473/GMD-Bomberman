using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    private bool paused;
    private Canvas canvas;
    
    void Start()
    { 
        canvas = Instantiate(pauseCanvas, new Vector3(10,2,5), Quaternion.Euler(90,0,0));
        canvas.gameObject.SetActive(false);
        canvas.GetComponentsInChildren<Button>().First(x => x.name == "ResumeButton").onClick.AddListener(Resume);
    }
 
    void Update () {
        if (Input.GetKeyDown("escape")) {
            Resume();
        }
    }
    public void Resume(){
        if(paused){
            Time.timeScale = 1.0f;
            canvas.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            paused = false;
        } else {
            Time.timeScale = 0.0f;
            canvas.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            paused = true;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
