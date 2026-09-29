using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinSceneManager : MonoBehaviour
{
    public static PinSceneManager instance { get; private set; }

    public struct PinData
    {
        public Vector3 Position;
        public string PinType;

        public PinData(Vector3 position, string pinType)
        {
            Position = position;
            PinType = pinType;
        }
    }

    private static List<PinData> SavedPins = new List<PinData>();

    [Tooltip("Tolerancia para encontrar los gameobjects pins")]
    [SerializeField] private float SearchRadius = 0.2f;

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
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetPins();
    }

    public static void AddToPinList(GameObject pinToAdd, string pinType)
    {
        Vector3 pos = pinToAdd.transform.position;

        int existingIndex = SavedPins.FindIndex(p => Vector3.Distance(p.Position, pos) < 0.1f);

        if (existingIndex != -1)
        {
            SavedPins[existingIndex] = new PinData(pos, pinType);
        }
        else
        {
             SavedPins.Add(new PinData(pos, pinType));
        }
    }

    private void SetPins()
    {
        foreach (var pinData in SavedPins)
        {
            Collider[] hits = Physics.OverlapSphere(pinData.Position, SearchRadius);

            GameObject foundObject = null;
            if (hits.Length > 0)
            {
                foundObject = hits[0].gameObject;
            }

            if (foundObject != null)
            {
                Pin pin = foundObject.GetComponent<Pin>();

                if (pin != null)
                {
                    pin.DeactiveAll();

                    switch (pinData.PinType)
                    {
                        case "AttractorPin":
                            pin.SetActiveComponent(pinData.PinType);
                            break;
                        case "RejectorPin":
                            pin.SetActiveComponent(pinData.PinType);
                            break;
                        case "BuffMultPin":
                            pin.SetActiveComponent(pinData.PinType);
                            break;
                        case "NerfMultPin":
                            pin.SetActiveComponent(pinData.PinType);
                            break;
                        case "DivisorPin":
                            pin.SetActiveComponent(pinData.PinType);
                            break;
                        default:
                            pin.SetActiveComponent("NormalPin");
                            break;
                    }
                }
            }
        }
    }

    public static void ClearSavedPins()
    {
        SavedPins.Clear();
    }
}