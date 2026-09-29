using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PointsText;
    public static bool PointsChanged;

    [SerializeField] private TextMeshProUGUI BallsText;
    public static bool BallOut;

    [SerializeField] private TextMeshProUGUI QuoteText;

    public static bool NextRoundUpdate = false;

    void Start()
    {
        PointsText.text = PuntuactionManager.GetPunctuaction() + " x";
        PointsChanged = false;

        QuoteText.text = "Next Payment " + QuoteManager.GetPointsRequired();

        BallsText.text = BallManager.GetBalls().ToString();
        BallOut = false;
    }

    void Update()
    {
        if (PointsChanged)
        {
            PointsText.text = PuntuactionManager.GetPunctuaction() + " x";
            PointsChanged = false;
        }

        if (BallOut || NextRoundUpdate)
        {
            BallsText.text = BallManager.GetBalls().ToString();
            NextRoundUpdate = false;
        }
    }
}