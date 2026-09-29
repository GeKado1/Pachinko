using TMPro;
using UnityEngine;

public class SuitsMults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HearthText;
    [SerializeField] private TextMeshProUGUI DiamondText;
    [SerializeField] private TextMeshProUGUI ClubsText;
    [SerializeField] private TextMeshProUGUI SpadesText;

    public void ModifyText(string text, float mult)
    {
        switch (text)
        {
            case "Hearth":
                HearthText.text = "Hearth Suit Multiplier: x" + mult;
                break;
            case "Diamond":
                DiamondText.text = "Diamond Suit Multiplier: x" + mult;
                break;
            case "Clubs":
                ClubsText.text = "Clubs Suit Multiplier: x" + mult;
                break;
            case "Spades":
                SpadesText.text = "Spades Suit Multiplier: x" + mult;
                break;
        }
    }
}