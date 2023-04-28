using UnityEngine;

[CreateAssetMenu(fileName = "BoardSO", menuName = "ScriptableObjects/BoardSO", order = 1)]
public class BoardSO : ScriptableObject
{

    [SerializeField] private int m_xSize;
    [SerializeField] private  int m_ySize;
    public int XSize => m_xSize;
    public int YSize => m_ySize;


}
