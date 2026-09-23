using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingItem : MonoBehaviour
{
    [SerializeField] private Item Item;
    private ItemManager ItemManager;

    void Start()
    {
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        if (ItemManager != null)
        {
            Item = ItemManager.GetItem();
        }
        else
        {
            Debug.Log("No se ha encontrado ningun ItemManager");
        }
    }
}