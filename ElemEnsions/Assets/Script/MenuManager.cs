using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private PlayerController playerController;

    [SerializeField] private GameObject quitExchangeBtn;
    [SerializeField ]private GameObject exchangeBtn;

    private bool UIOn = false;
    private bool canPause = false;

    void Start() 
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
        gameOver = GameObject.FindGameObjectWithTag("GameOverUI");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        exchangeUI = GameObject.FindGameObjectWithTag("ExchangeUI");
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

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
            PauseGame();  

        if(UIOn)
        {
            Debug.Log("in menu");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = UIOn;
        }
        else
        {
            Debug.Log("in game");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = UIOn;
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

    private void ToogleGameUI(bool mode)
    {
        canPause = mode;
        gameUI.SetActive(mode);
        StopGame(!mode);
        UIOn = !mode;
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
            pauseMenu.SetActive(pause);
            StopGame(pause);
        }
    }

    private void StopGame(bool deactivate)
    {
        Time.timeScale = (deactivate ? 0 : 1);
        playerController.enabled = !deactivate;
    }

    public void ActivateMainMenu()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        mainMenu.SetActive(true);
        StopGame(true);
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
        UIOn = true;
    }

    public void CloseExchangeUI()
    {
        exchangeUI.SetActive(false);
        UIOn = false;
    }



    public void ExchangeCrystals()
    {
        inventory.ConfirmExchange();
        exchangeUI.SetActive(false);
        UIOn = false;
    }
}
