using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneSetup : MonoBehaviour
{
    public static PlayerSceneSetup instance { get; private set; }
    public static int PinsToSetup;

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
        SetupCanvas.SetActive(false);

        Items = ItemManager.GetPinItems();

        ItemsType = Items
                    .Select(item => new Dictionary<string, string> { { item.ItemTag, item.ItemName } })
                    .ToList();

        PinsToSetup = Items.Count;
    }
}