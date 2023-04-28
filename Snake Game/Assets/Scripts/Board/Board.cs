using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardSO m_boardSO;
    
    [SerializeField] private WallStruct m_wallStruct;
    
    private void Awake()
    {
        GameEvents.OnGameStart += BoardInit;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStart -= BoardInit;
    }

    private void BoardInit()
    {
        m_wallStruct.wallPrefabPool =
            new ObjectPool<GameObject>(() => InstantiateWallPrefab(Vector2.one), 
                obj => obj.gameObject.SetActive(true),
                obj => obj.gameObject.SetActive(false),
                obj => Destroy(obj.gameObject),
                m_wallStruct.defaultSize,
                m_wallStruct.maxSize);
        
        CreateBoard();
    }
    
    private GameObject InstantiateWallPrefab(Vector2 position)
    {
        GameObject wall = Instantiate(m_wallStruct.wallPrefab, transform);
        wall.transform.position = position;

        return wall;
    }
    
    [ContextMenu("CreateBoard")]
    private void CreateBoard()
    {
        //creating borders for board on top and bottom side
        for (int i = 0; i <= m_boardSO.XSize; i++)
        {
            float x = i - m_boardSO.XSize / 2;
            float y = m_boardSO.YSize / 2;

            GameObject bottomBorder = m_wallStruct.wallPrefabPool.Get();
            bottomBorder.transform.position = new Vector2(x, -y);

            GameObject topBorder = m_wallStruct.wallPrefabPool.Get();
            topBorder.transform.position = new Vector2(x, m_boardSO.YSize - y);
        }

        //creating borders for board on left and right side
        for (int i = 0; i <= m_boardSO.XSize; i++)
        {
            float x = m_boardSO.XSize / 2;
            float y = i - m_boardSO.YSize / 2;
            
            GameObject rightBorder = m_wallStruct.wallPrefabPool.Get();
            rightBorder.transform.position = new Vector2(-x, y);

            GameObject leftBorder = m_wallStruct.wallPrefabPool.Get();
            leftBorder.transform.position = new Vector2(m_boardSO.XSize - x, y);
        }
        
        GameEvents.OnBoardSetupEvent();
    }
}