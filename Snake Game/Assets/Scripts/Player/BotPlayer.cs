using System.Collections.Generic;
using UnityEngine;

public class BotPlayer : BasePlayer
{
    
    private GameObject m_apple;
    private List<Vector2> m_occupiedPositions = new();

    protected override void Start()
    {
        base.Start();
        UpdateOccupiedPositions();
    }
    
    protected override void Update()
    {
        if (m_apple == null)
        {
            m_apple = GameObject.FindWithTag("Food");
        }
        
        if (m_apple != null)
        {
            Vector2 toApple = m_apple.transform.position - transform.position;
            List<Vector2> possibleMoves = new List<Vector2>();
            if (Mathf.Abs(toApple.x) > Mathf.Abs(toApple.y))
            {
                possibleMoves.Add(new Vector2(Mathf.Sign(toApple.x), 0f));
                possibleMoves.Add(new Vector2(0f, Mathf.Sign(toApple.y)));
            }
            else
            {
                possibleMoves.Add(new Vector2(0f, Mathf.Sign(toApple.y)));
                possibleMoves.Add(new Vector2(Mathf.Sign(toApple.x), 0f));
            }
    
            foreach (Vector2 move in possibleMoves)
            {
                Vector2 targetPos = (Vector2)transform.position + move;
                if (!m_occupiedPositions.Contains(targetPos))
                {
                    m_input = move;
                    break;
                }
            }
        }
    
        base.Update();
    }
    
    
    private void UpdateOccupiedPositions()
    {
        m_occupiedPositions.Clear();
        foreach (GameObject tail in m_tails)
        {
            m_occupiedPositions.Add(tail.transform.position);
        }
        m_occupiedPositions.Add(transform.position);
        if (m_apple != null)
        {
            m_occupiedPositions.Add(m_apple.transform.position);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            GrowSnake();
            UpdateOccupiedPositions();
            GameEvents.OnFoodEatenEvent();
        }
    }

}
