using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public void SetText(string text)
    {
        label.text = text;
    }
    
    public void SetColorOfText(Color color)
    {
        label.color = color;
    }
}
