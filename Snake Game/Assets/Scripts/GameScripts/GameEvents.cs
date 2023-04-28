using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnFoodEaten;
    public static void OnFoodEatenEvent()
    {
        OnFoodEaten?.Invoke();
    }

    public static event Action OnBoardSetup;
    
    public static void OnBoardSetupEvent()
    {
        OnBoardSetup?.Invoke();
    }

    public static event Action<int> OnFoodCollected;
    public static void OnFoodCollectedEvent(int playerId)
    {
        OnFoodCollected?.Invoke(playerId);
    }
    
    public static event Action OnGameStart;
    public static void OnGameStartEvent()
    {
        OnGameStart?.Invoke();
    }
    
    public static event Action OnGameOver;
    public static void OnGameOverEvent()
    {
        OnGameOver?.Invoke();
    }
    
    public static event Action OnWinGame;
    public static void OnWinGameEvent()
    {
        OnWinGame?.Invoke();
    }
    
    public static event Action<Color, int> OnColorChange;
    public static void OnColorChangeEvent(Color color, int id)
    {
        OnColorChange?.Invoke(color, id);
    }

}
