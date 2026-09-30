using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneSetup : MonoBehaviour
{
    public static PlayerSceneSetup instance { get; private set; }
    public static int PinsToSetup = 0;

    [SerializeField] private GameObject SetupCanvas;

    [SerializeField] private ItemManager ItemManager;
    public ItemManager ItemManagerRef => ItemManager;

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
        SetupCanvas.SetActive(false);
    }

    void Update()
    {
        if (PinsToSetup <= 0)
        {
            SetupCanvas.SetActive(false);
        }
    }

    public void OnTransition()
    {
        SetupCanvas.SetActive(true);
        Items = ItemManager.GetPinItems();

        ItemsType = Items
                    .Select(item => new Dictionary<string, string> { { item.ItemTag, item.ItemName } })
                    .ToList();

        PinsToSetup = Items.Count;
    }
}