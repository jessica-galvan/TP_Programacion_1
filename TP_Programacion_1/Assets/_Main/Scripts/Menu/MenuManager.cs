using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("AllMenus Settings")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject helpMenu;
    [SerializeField] private GameObject creditsMenu;
    private bool mainMenuCheck;

    [Header("MainMenu Settings")]
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonHelp;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private string gameSceneName = "Level1";

    [Header("Help Settings")]
    [SerializeField] private Button buttonHelpGoBack;

    [Header("Credits Settings")]
    [SerializeField] private Button buttonCreditsGoBack;

    void Awake()
    {
        buttonPlay.onClick.AddListener(OnClickPlayHandler);
        buttonHelp.onClick.AddListener(OnClickHelpHandler);
        buttonCredits.onClick.AddListener(OnClickCreditsHandler);
        buttonQuit.onClick.AddListener(OnClickQuitHandler);
        buttonHelpGoBack.onClick.AddListener(OnClickGoBackHandler);
        buttonCreditsGoBack.onClick.AddListener(OnClickGoBackHandler);
        
        //Siempre inicia con el menu principal
        OnClickGoBackHandler();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!mainMenuCheck)
            {
                OnClickGoBackHandler();
            }
        }
    }

    private void OnClickPlayHandler()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    private void OnClickHelpHandler()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
        creditsMenu.SetActive(false);
        mainMenuCheck = false;
    }

    private void OnClickCreditsHandler()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(false);
        creditsMenu.SetActive(true);
        mainMenuCheck = false;
    }

    private void OnClickGoBackHandler()
    {
        mainMenu.SetActive(true);
        helpMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenuCheck = true;
    }

    private void OnClickQuitHandler()
    {
        Application.Quit();
    }

}
