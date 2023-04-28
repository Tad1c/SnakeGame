using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreLabel m_scorePrefab;

    private List<ScoreLabel> m_scoreLabels = new();

    public void Awake()
    {
        GameEvents.OnFoodCollected += DisplayScore;
        GameEvents.OnGameOver += RemoveScores;
        GameEvents.OnColorChange += SetColorToText;
    }

    public void OnDestroy()
    {
        GameEvents.OnFoodCollected -= DisplayScore;
        GameEvents.OnGameOver -= RemoveScores;
        GameEvents.OnColorChange -= SetColorToText;
    }

    private void DisplayScore(int playerId)
    {
        int score = GameManager.Instance.Players[playerId].Score;
        m_scoreLabels[playerId].SetText($"Player {playerId + 1}: {score}");
    }
    
    private void SetColorToText(Color color, int id)
    {
       m_scoreLabels[id].SetColorOfText(color);
    }

    private void RemoveScores()
    {
        foreach (ScoreLabel scoreLabel in m_scoreLabels)
        {
            Destroy(scoreLabel.gameObject);
        }
        
        m_scoreLabels.Clear();
    }

    private void Start()
    {
        for (int i = 0; i < GameManager.Instance.Players.Count; i++)
        {
            ScoreLabel scoreObj = Instantiate(m_scorePrefab, transform);
            m_scoreLabels.Add(scoreObj);
            DisplayScore(i);
        }
    }
}