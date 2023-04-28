using System;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private  GameObject m_foodPrefab;
    
    private GameObject m_foodObject;
    
    private int m_maxFoodSpawnAttempts = 100;
    private int m_foodSpawnAttempts = 0;
    
    private void OnDestroy()
    {
        GameEvents.OnFoodEaten -= SpawnFood;
    }
    
    private void Start()
    {
        GameEvents.OnFoodEaten += SpawnFood;
    }

    private void SpawnFood()
    {
        Vector2 randomSpawnPos = Utily.GetRandomPositionFromBoard();
        try
        {
            while (GameManager.Instance.CheckIfPlayersAreInThisPosition(randomSpawnPos))
            {
                m_foodSpawnAttempts++;
                if (m_foodSpawnAttempts > m_maxFoodSpawnAttempts)
                {
                    GameEvents.OnWinGameEvent();
                    return;
                }

                randomSpawnPos = Utily.GetRandomPositionFromBoard();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        m_foodSpawnAttempts = 0;

        if (m_foodObject == null)
        {
            m_foodObject = Instantiate(m_foodPrefab);
        }

        m_foodObject.transform.position = randomSpawnPos;
    }
    
}
