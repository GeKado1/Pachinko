using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneSetup : MonoBehaviour
{
    public static PlayerSceneSetup instance { get; private set; }
    public static bool PinsToSetup;

    [SerializeField] private GameObject SetupCanvas;

    [SerializeField] private ItemManager ItemManager;
    private List<Item> Items;
    private List<string> ItemsType;

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
        }

        PinsToSetup = false;
        SetupCanvas.SetActive(false);

        Items = ItemManager.GetActiveItems();
    }

    void Update()
    {
        if (PinsToSetup)
        {
            SetupCanvas.SetActive(true);
        }
        else
        {
            SetupCanvas.SetActive(false);
        }
    }
}