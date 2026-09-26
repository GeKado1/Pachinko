using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PinSceneManager : MonoBehaviour
{
    public static PinSceneManager instance { get; private set; }
    private static List<Dictionary<Transform, string>> Pins;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

            SetPins();
        }
    }

    public static void AddToPinList(GameObject pinToAdd, string pinType)
    {
        Pins.Add(new Dictionary<Transform, string> { { pinToAdd.transform, pinType } });
    }

    private void SetPins()
    {
        foreach (var pin in Pins)
        {
            string pinType = pin.Values.First();

            switch (pinType)
            {
                case "AttractorPin":
                    
                    break;
                case "RejectorPin":
                    
                    break;
                case "BuffMultPin":
                    
                    break;
                case "NerfMultPin":
                    
                    break;
                case "DivisorPin":
                    
                    break;
                default:
                    
                    break;
            }
        }
    }
}