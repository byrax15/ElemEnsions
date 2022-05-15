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
    private GameObject player;
    private InteractableManager interactableManager;
    private bool isOnUI = false;
    private bool canPause = false;


    [SerializeField] private GameObject quitExchangeBtn;
    [SerializeField ]private GameObject exchangeBtn;



    public bool UIOn 
    {
        get => isOnUI;

        private set 
        {
            isOnUI = value;
            interactableManager.MenuOn = value;
            
            if(isOnUI)
                interactableManager.DisableAllIndicators();
                
        }
    }

    void Start() 
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
        gameOver = GameObject.FindGameObjectWithTag("GameOverUI");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        exchangeUI = GameObject.FindGameObjectWithTag("ExchangeUI");
        interactableManager = GameObject.FindGameObjectWithTag("InteractableManager").GetComponent<InteractableManager>();
        player = GameObject.FindGameObjectWithTag("Player");

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
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = UIOn;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = UIOn;
        }
    }

    public void StartGame()
    {
        ToogleGameUI(true);
        mainMenu.SetActive(false);
        gameOver.SetActive(false);
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

        if(!mode) 
            InputSystem.DisableDevice(Keyboard.current);
        else 
            InputSystem.EnableDevice(Keyboard.current);
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
        player.GetComponent<PlayerController>().enabled = !deactivate;
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
        ToogleExchangeUI(true);
        exchangeBtn.SetActive(player.GetComponent<PlayerInventory>().PrepareExchange());
    }

    public void ToogleExchangeUI(bool activate)
    {
        exchangeUI.SetActive(activate);
        UIOn = activate;
    }

    public void ExchangeCrystals()
    {
        player.GetComponent<PlayerInventory>().ConfirmExchange();
        ToogleExchangeUI(false);
    }
}
