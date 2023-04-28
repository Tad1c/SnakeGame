using System.Collections;
using UnityEngine;

public class Tail : MonoBehaviour
{
    
    [SerializeField] private BoxCollider2D m_boxCollider2D;

    [SerializeField] private SpriteRenderer m_spriteRenderer;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        m_boxCollider2D.enabled = true;
    }

    public void SetTailColor(Color color)
    {
        m_spriteRenderer.color = color;
    }
}
