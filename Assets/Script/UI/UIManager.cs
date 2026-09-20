using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PointsText;
    public static bool PointsChanged;

    void Start()
    {
        PointsText.text = "Points: " + PuntuactionManager.GetPunctuaction();
        PointsChanged = false;
    }

    void Update()
    {
        if (PointsChanged)
        {
            PointsText.text = "Points: " + PuntuactionManager.GetPunctuaction();
            PointsChanged = false;
        }
    }
}