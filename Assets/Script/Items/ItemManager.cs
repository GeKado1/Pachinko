using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> AllItems = new List<Item>();
    public List<Item> ActiveItems = new List<Item>();

    public static ItemManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public Item GetItem()
    {
        int randomItem = Random.Range(0, AllItems.Count);

        return AllItems[randomItem];
    }
}