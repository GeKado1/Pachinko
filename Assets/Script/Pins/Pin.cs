using UnityEngine;

public class Pin : MonoBehaviour
{
    public static Pin SelectedPin;

    [Header("Pins")]
    [Tooltip("Referencias a los diferentes GameObjects de Pins para activarlos y desactivarlos cuando toca")]
    [SerializeField] private GameObject NormalPin;
    [SerializeField] private GameObject AttractorPin;
    [SerializeField] private GameObject RejectorPin;
    [SerializeField] private GameObject BuffMultPin;
    [SerializeField] private GameObject NerfMultPin;
    [SerializeField] private GameObject DivisorPin;

    [Header("Pin Canvas")]
    [SerializeField] private GameObject PinCanvas;

    void Awake()
    {
        DeactiveAll();
    }

    void Update()
    {
        if (PlayerSceneSetup.PinsToSetup)
        {
            PinCanvas.SetActive(true);
        }
        else
        {
            PinCanvas.SetActive(false);
        }
    }

    public void OnPinChange(string pinType)
    {
        NormalPin.SetActive(false);
        DeactiveAll();

        SetActiveComponent(pinType);

        PinSceneManager.AddToPinList(gameObject, pinType);
    }

    private void OnMouseDown()
    {
        SelectedPin = this;
    }

    public void DeactiveAll()
    {
        AttractorPin.SetActive(false);
        RejectorPin.SetActive(false);
        BuffMultPin.SetActive(false);
        NerfMultPin.SetActive(false);
        DivisorPin.SetActive(false);
    }

    public void SetActiveComponent(string pinType)
    {
        NormalPin.SetActive(false);
        switch (pinType)
        {
            case "AttractorPin":
                AttractorPin.SetActive(true);
                break;
            case "RejectorPin":
                RejectorPin.SetActive(true);
                break;
            case "BuffMultPin":
                BuffMultPin.SetActive(true);
                break;
            case "NerfMultPin":
                NerfMultPin.SetActive(true);
                break;
            case "DivisorPin":
                DivisorPin.SetActive(true);
                break;
            default:
                NormalPin.SetActive(true);
                break;
        }
    }
}