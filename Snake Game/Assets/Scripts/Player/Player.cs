using UnityEngine;

public class Player : BasePlayer
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            m_score++;
            GameEvents.OnFoodCollectedEvent(ID);
            GrowSnake();
        }

        if (other.CompareTag("Wall") || other.CompareTag("Tail"))
        {
            DestroySnake();
        }
    }
}