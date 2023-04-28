using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BasePlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] protected Tail m_tailPrefab;

    [SerializeField] private int m_initialSize = 3;
    [SerializeField] private float m_speed = 0.1f;
    
    private float m_passedTime;
    protected int m_score;
    private int m_id;
    
    private Vector2 m_dir = Vector2.right;
    protected Vector2 m_input;
    private Vector2 m_moveDirection;

    protected List<GameObject> m_tails = new();
    private ObjectPool<GameObject> m_tailObjectPool;

    private bool m_playerDead;
    private bool m_inputPressed;

    private Color m_snakeColor;
    
    public List<GameObject> Tails => m_tails;
    public int Score => m_score;

    public int ID
    {
        get => m_id;
        set => m_id = value;
    }


    protected virtual void Start()
    {
        m_tails.Add(gameObject);
        
        SetColorToSnake();

        m_tailObjectPool = new ObjectPool<GameObject>(InstantiateTailObject,
            obj => obj.SetActive(true),
            obj => obj.SetActive(false),
            Destroy,
            7,
            100);

        for (int i = 0; i < m_initialSize - 1; i++)
        {
            GrowSnake();
        }
    }

    protected virtual void Update()
    {
        if (m_playerDead)
            return;

        InputDirection();
        MovementOverTime();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_input = context.ReadValue<Vector2>();
    }
    
    private void MovementOverTime()
    {
        m_passedTime += Time.deltaTime;
        if (m_passedTime >= m_speed)
        {
            for (int i = m_tails.Count - 1; i > 0; i--)
            {
                m_tails[i].transform.position = m_tails[i - 1].transform.position;
            }

            Vector2 position = transform.position;
            float x = Mathf.Round(position.x) + m_dir.x;
            float y = Mathf.Round(position.y) + m_dir.y;

            transform.position = new Vector2(x, y);
            
            m_inputPressed = false;
            m_passedTime -= m_speed;
        }
    }

    private void InputDirection()
    {
        if (m_dir.y == 0f)
        {
            if (m_input.y == 1)
            {
                m_moveDirection = Vector2.up;
            }
            else if (m_input.y == -1)
            {
                m_moveDirection = Vector2.down;
            }
        }
        else if (m_dir.x == 0)
        {
            if (m_input.x == 1)
            {
                m_moveDirection = Vector2.right;
            }
            else if (m_input.x == -1)
            {
                m_moveDirection = Vector2.left;
            }
        }
        
        if (m_moveDirection != Vector2.zero)
             m_dir = m_moveDirection;
    }

    private GameObject InstantiateTailObject()
    {
        Tail tail = Instantiate(m_tailPrefab);
        tail.SetTailColor(m_snakeColor);
        return tail.gameObject;
    }
    
    private void SetColorToSnake()
    {
        m_snakeColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        GameEvents.OnColorChangeEvent(m_snakeColor, m_id);
        m_spriteRenderer.color = m_snakeColor;
    }

    protected void GrowSnake()
    {
        GameObject tail = m_tailObjectPool.Get();
        tail.transform.position = m_tails[m_tails.Count - 1].transform.position;
        m_tails.Add(tail);
        GameEvents.OnFoodEatenEvent();
    }

    protected void DestroySnake()
    {
        GameManager.Instance.SnakeDied(m_id);
        
        m_playerDead = true;
        foreach (GameObject tail in m_tails)
        {
            m_tailObjectPool.Store(tail);
        }
    }
}