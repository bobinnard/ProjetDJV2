using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;

    private bool _isPaused;

    private void Start()
    {
        optionsMenu.SetActive(false);
        if (pauseMenu) pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenu.activeSelf)
            {
                ToggleOptionsMenu();
                return;
            }

            if (pauseMenu) TogglePause();
        }
    }

    public void StartGame() {
        SceneManager.LoadScene("Scenes/Paquito");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void BackToMainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void TogglePause(){
        Time.timeScale = (Time.timeScale - 1) * (Time.timeScale - 1); 
        _isPaused = !_isPaused;
        pauseMenu.SetActive(_isPaused);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ToggleOptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
