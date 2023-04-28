using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    
    [SerializeField] private  float m_cameraSize = 10f;
    [SerializeField] private Vector3 m_offset;
    
    private Camera m_camera;

    private void OnEnable()
    {
        m_camera = Camera.main;
        GameEvents.OnBoardSetup += SetCameraSizeBasedOnBoard;
    }

    private void OnDestroy()
    {
        GameEvents.OnBoardSetup -= SetCameraSizeBasedOnBoard;
    }

    private void SetCameraSizeBasedOnBoard()
    {
        Vector2 boardSize = new Vector2(Utily.GetBoardSo.XSize, Utily.GetBoardSo.YSize);

        float screenAspect = (float)Screen.width / Screen.height;
        float boardAspect = boardSize.x / boardSize.y;
        float cameraSize = (boardSize.y / 2) + m_cameraSize;
        if (boardAspect > screenAspect)
        {
            cameraSize = (boardSize.x / 2 / screenAspect);
        }

        m_camera.orthographicSize = cameraSize;
        m_camera.transform.position = m_offset;
    }
}