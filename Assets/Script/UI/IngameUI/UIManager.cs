using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PointsText;
    public static bool PointsChanged;

    [SerializeField] private TextMeshProUGUI BallsText;
    public static bool BallOut;

    [SerializeField] private TextMeshProUGUI QuoteText;

    void Start()
    {
        PointsText.text = "Points: " + PuntuactionManager.GetPunctuaction();
        PointsChanged = false;

        QuoteText.text = "Points required: " + QuoteManager.GetPointsRequired();

        BallsText.text = "Balls Remaining: " + BallManager.GetBalls();
        BallOut = false;
    }

    void Update()
    {
        if (PointsChanged)
        {
            PointsText.text = "Points: " + PuntuactionManager.GetPunctuaction();
            PointsChanged = false;
        }

        if (BallOut)
        {
            BallsText.text = "Balls Remaining: " + BallManager.GetBalls();
        }
    }
}