using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject gameUI;
    private GameObject gameOver;
    private GameObject pauseMenu;
    private GameObject exchangeUI;
    private PlayerInventory inventory;


    [SerializeField] private GameObject quitExchangeBtn;
    [SerializeField ]private GameObject exchangeBtn;

    private bool canPause = false;

    void Start() 
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
        gameOver = GameObject.FindGameObjectWithTag("GameOverUI");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        exchangeUI = GameObject.FindGameObjectWithTag("ExchangeUI");
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        mainMenu.SetActive(true);
        exchangeUI.SetActive(false);
        ToogleGameUI(false);
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
        ToogleGameUI(true);
        gameOver.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        TooglePause(true);
        exchangeUI.SetActive(false);
        ToogleGameUI(false);
    }

    public void ToogleGameUI(bool mode)
    {
        canPause = mode;
        gameUI.SetActive(mode);
        // enlever rendering des icones
        // looper sur _indicatorsByInteractables pour disable tous les indicators
    }

    public void ContinueGame() 
    {
        ToogleGameUI(true);
        TooglePause(false);
    }

    private void TooglePause(bool pause)
    {
        if(canPause)
        {
            Time.timeScale = (pause ? 0 : 1);
            pauseMenu.SetActive(pause);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = !pause;
        }
    }

    public void ActivateMainMenu()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void QuitGame()
    {
        Debug.Log("Quit...");
        Application.Quit();
    }

    public void OpenExchangeUI()
    {
        exchangeUI.SetActive(true);
        bool canExchange = inventory.PrepareExchange();
        exchangeBtn.SetActive(canExchange);
        quitExchangeBtn.SetActive(!canExchange);
        // if(inventory.PrepareExchange())
        //     exchangeBtn.enabled = true;
        // else
        //     quitExchangeBtn.enabled = true;

    }

    public void CloseExchangeUI()
    {
        exchangeUI.SetActive(false);
    }



    public void ExchangeCrystals()
    {
        inventory.ConfirmExchange();
        exchangeUI.SetActive(false);
    }
}
