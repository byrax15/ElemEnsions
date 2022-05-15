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
    private GameObject creditsPage;
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
        creditsPage = GameObject.FindGameObjectWithTag("CreditsPage");
        interactableManager = GameObject.FindGameObjectWithTag("InteractableManager").GetComponent<InteractableManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        mainMenu.SetActive(true);
        exchangeUI.SetActive(false);
        ToogleGameUI(false);
        gameOver.SetActive(false);
        pauseMenu.SetActive(false);
        creditsPage.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update() 
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            PauseGame();  

        Cursor.visible = UIOn;

        if(UIOn)
            Cursor.lockState = CursorLockMode.Confined;
        else
            Cursor.lockState = CursorLockMode.Locked;
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

    private void StopGame(bool disable)
    {
        Time.timeScale = (disable ? 0 : 1);
        player.GetComponent<PlayerController>().enabled = !disable;
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

    public void CloseExchangeUI() => ToogleExchangeUI(false);

    private void ToogleExchangeUI(bool activate)
    {
        exchangeUI.SetActive(activate);
        UIOn = activate;
    }

    public void ExchangeCrystals()
    {
        player.GetComponent<PlayerInventory>().ConfirmExchange();
        ToogleExchangeUI(false);
    }

    public void OpenCreditsPage()
    {
        creditsPage.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseCreditsPage()
    {
        creditsPage.SetActive(false);
        mainMenu.SetActive(true);
    }
    
    public void GameOver(float showForSeconds)
    {
        StartCoroutine(ShowFor(showForSeconds));
        IEnumerator ShowFor(float showForSeconds)
        {
            gameOver.SetActive(true);
            yield return new WaitForSeconds(showForSeconds);
            PauseGame();
        }
    }
}
