using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;

    void Start() 
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
        gameOver = GameObject.FindGameObjectWithTag("GameOverUI");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");

        mainMenu.SetActive(true);
        gameUI.SetActive(false);
        gameOver.SetActive(false);
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update() 
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        { 
            PauseGame();    
        }    
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        gameOver.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        TooglePause(true);
        gameUI.SetActive(false);
    }

    public void ContinueGame() 
    {
        TooglePause(false);
        gameUI.SetActive(true);
    }

    private void TooglePause(bool pause)
    {
        Time.timeScale = (pause ? 0 : 1);
        pauseMenu.SetActive(pause);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = !pause;
    }

    public void ActivateMainMenu()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit...");
        Application.Quit();
    }
}
