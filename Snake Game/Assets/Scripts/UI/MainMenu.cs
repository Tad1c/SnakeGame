using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_scoreObject;
    [SerializeField] private GameObject m_mainMenuScreen;
    [SerializeField] private GameObject m_gameOverScreen;
    [SerializeField] private GameObject m_winGameScreen;
    
    private void Awake()
    {
        GameEvents.OnGameOver += GameOverScreen;
        GameEvents.OnWinGame += WinGameScreen;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameOver -= GameOverScreen;
        GameEvents.OnWinGame -= WinGameScreen;
    }

    public void SinglePlayerButtonEvent()
    {
        CloseManuObjectOnButtonPress();
        GameManager.Instance.SinglePlayer();
    }

    public void DoublePlayerButtonEvent()
    {
        CloseManuObjectOnButtonPress();
        GameManager.Instance.MultiPlayer();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    private void CloseManuObjectOnButtonPress()
    {
        m_scoreObject.SetActive(true);
        m_mainMenuScreen.SetActive(false);
        GameEvents.OnGameStartEvent();
    }
    
    private void WinGameScreen()
    {
        m_scoreObject.SetActive(false);
        m_winGameScreen.SetActive(true);
    }
    
    private void GameOverScreen()
    {
        m_scoreObject.SetActive(false);
        m_gameOverScreen.SetActive(true);
    }
}