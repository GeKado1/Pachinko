using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSceneSetup : MonoBehaviour
{
    public static PlayerSceneSetup instance { get; private set; }
    public static bool PinsToSetup;

    [SerializeField] private GameObject SetupCanvas;

    [SerializeField] private ItemManager ItemManager;
    private List<Item> Items;
    public List<Dictionary<string, string>> ItemsType { get; private set; }

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
    }

    void Start()
    {
        PinsToSetup = true;
        SetupCanvas.SetActive(false);

        Items = ItemManager.GetActiveItems();

        ItemsType = Items
                    .Select(item => new Dictionary<string, string> { { item.ItemTag, item.ItemName } })
                    .ToList();
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