using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject mainMenu;
    
    private bool _isPaused;

    private void Start()
    {
        optionsMenu.SetActive(false);
    }
    
    IEnumerator RestartLevel()
    {
        yield return new WaitForSecondsRealtime(.5f);
        SceneManager.LoadScene("Scenes/Paquito");
    }

    public void StartGame() {
        Time.timeScale=1;
        SceneManager.LoadScene("Scenes/Paquito");
        StartCoroutine(RestartLevel());
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void PauseGame(){
        Time.timeScale=0; 
        _isPaused=true;
    }

    public void ResumeGame(){
        Time.timeScale=1;
        _isPaused=false;
    }

    public void ToggleOptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
    }
}
