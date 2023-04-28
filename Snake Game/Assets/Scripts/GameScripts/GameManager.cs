using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private BasePlayer m_playerPrefab;
    private List<BasePlayer> m_players = new();
    private List<BasePlayer> m_deadPlayers = new();
    public List<BasePlayer> Players => m_players;

    private int m_playerId = 0;
    
    public void SinglePlayer()
    {
        CreateSnakePlayer("Keyboard1");
    }
    
    public void MultiPlayer()
    {
        CreateSnakePlayer("Keyboard1");
        CreateSnakePlayer("Keyboard2");
    }

    private void CreateSnakePlayer(string controlScheme)
    {
        var player = PlayerInput.Instantiate(m_playerPrefab.gameObject,
            controlScheme: controlScheme, pairWithDevice: Keyboard.current);
   
        player.transform.position = Utily.GetRandomPositionForPlayerOnBoard();
        BasePlayer basePlayer = player.GetComponent<BasePlayer>();
        
        basePlayer.ID = m_playerId;
        m_players.Add(basePlayer);
        
        m_playerId++;
    }

    public void SnakeDied(int id)
    {
        m_deadPlayers.Add(m_players[id]);

        if (m_players.Count == m_deadPlayers.Count)
        {
            GameEvents.OnGameOverEvent();
            m_deadPlayers.Clear();
            m_players.Clear();
            m_playerId = 0;
        }
    }

    public bool CheckIfPlayersAreInThisPosition(Vector2 spawnPos)
    {
        bool isPlayerInThisPosition = false;
        bool isPlayerTailInThisPosition = false;

        foreach (BasePlayer player in m_players)
        {
            isPlayerInThisPosition =
                spawnPos.x == player.transform.position.x && spawnPos.y == player.transform.position.y;

            if (isPlayerInThisPosition)
                break;
        }

        foreach (BasePlayer player in m_players)
        {
            foreach (GameObject tail in player.Tails)
            {
                isPlayerTailInThisPosition =
                    spawnPos.x == tail.transform.position.x && spawnPos.y == tail.transform.position.y;

                if (isPlayerTailInThisPosition)
                    break;
            }
        }

        return isPlayerInThisPosition || isPlayerTailInThisPosition;
    }
}