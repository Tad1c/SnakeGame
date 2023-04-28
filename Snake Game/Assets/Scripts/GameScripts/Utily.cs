using UnityEngine;

public static class Utily
{
    private static BoardSO m_boardSO;
    
    public static BoardSO GetBoardSo => m_boardSO;

    static Utily()
    {
        m_boardSO = Resources.Load<BoardSO>("ScriptableObjects/BoardSO");
    }
    
    public static Vector2 GetRandomPositionFromBoard()
    {
        return new Vector2(Random.Range(-m_boardSO.XSize / 2 + 1, m_boardSO.XSize / 2 ), Random.Range(-m_boardSO.YSize / 2 + 1, m_boardSO.YSize / 2));
    }
    
    public static Vector2 GetRandomPositionForPlayerOnBoard()
    {
        return new Vector2(Random.Range(-m_boardSO.XSize / 2 + 4, m_boardSO.XSize / 4), Random.Range(-m_boardSO.YSize / 2 + 4, m_boardSO.YSize / 4));
    }

}
