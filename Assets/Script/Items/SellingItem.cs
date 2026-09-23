using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellingItem : MonoBehaviour
{
    [SerializeField] private Item Item;
    private ItemManager ItemManager;

    [SerializeField] TextMeshProUGUI PriceText;

    void Start()
    {
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        if (ItemManager != null)
        {
            Item = ItemManager.GetItem();

            PriceText.text = Item.ItemPrice.ToString() + " €";
        }
        else
        {
            Debug.Log("No se ha encontrado ningun ItemManager");
        }
    }
}