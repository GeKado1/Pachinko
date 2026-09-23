using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellingItem : MonoBehaviour
{
    [SerializeField] private Item Item;
    private ItemManager ItemManager;

    [SerializeField] private TextMeshProUGUI PriceText;

    [SerializeField] private Button BuyButton;

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

    public void OnSell()
    {
        if (PlayerPocket.Points >= Item.ItemPrice)
        {
            ItemManager.ActiveItems.Add(Item);
            BuyButton.interactable = false;
            BuyButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SOLD";
        }
        else
        {
            //CAMBIAR PARA ALGO MÁS VISUAL O NS
            Debug.Log("eres demasiado pobre para este comprar este objeto");
        }
    }
}