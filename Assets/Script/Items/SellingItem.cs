using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellingItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Item Item;
    private ItemManager ItemManager;

    [SerializeField] private GameObject ItemDescription;

    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private GameObject BallSprite;

    [SerializeField] private Button BuyButton;

    void Start()
    {
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        if (ItemManager != null)
        {
            Item = ItemManager.GetItem();

            if (PlayerPocket.Points < Item.ItemPrice)
            {
                PriceText.color = Color.red;
                BuyButton.interactable = false;
            }
            else
            {
                PriceText.color = Color.white;
            }

            PriceText.text = Item.ItemPrice.ToString() + " X ";
        }
        else
        {
            Debug.Log("No se ha encontrado ningun ItemManager");
        }

        if (ItemManager != null)
        {
            ItemDescription.GetComponent<TextMeshProUGUI>().text = Item.ItemDescription;
            ItemDescription.SetActive(false);
        }
    }

    public void OnSell()
    {
        if (PlayerPocket.Points >= Item.ItemPrice)
        {
            ItemManager.PurchasedItems.Add(Item);
            BuyButton.interactable = false;
            PriceText.text = "SOLD";
            PlayerPocket.Points -= (int)Item.ItemPrice;
            BallSprite.SetActive(false);
        }
        else
        {
            //CAMBIAR PARA ALGO MÁS VISUAL O NS
            Debug.Log("eres demasiado pobre para este comprar este objeto");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ItemDescription != null)
        {
            ItemDescription.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ItemDescription != null)
        {
            ItemDescription.SetActive(false);
        }
    }
}