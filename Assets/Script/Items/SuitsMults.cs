using TMPro;
using UnityEngine;

public class SuitsMults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HeartText;
    [SerializeField] private TextMeshProUGUI DiamondText;
    [SerializeField] private TextMeshProUGUI ClubsText;
    [SerializeField] private TextMeshProUGUI SpadesText;

    public void ModifyText(string text, float mult)
    {
        switch (text)
        {
            case "Heart":
                HeartText.text = "x " + mult;
                break;
            case "Diamond":
                DiamondText.text = "x " + mult;
                break;
            case "Clubs":
                ClubsText.text = "x " + mult;
                break;
            case "Spades":
                SpadesText.text = "x " + mult;
                break;
        }
    }
}