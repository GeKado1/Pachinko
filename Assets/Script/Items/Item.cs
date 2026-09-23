using System;

[Serializable]
public class Item
{
    public string ItemName;
    public string ItemTag;
    public string ItemDescription;
    public float ItemPrice;
    public float ItemStatValue;

    public Item(string itemName, string itemTag, string itemDescription, float itemPrice, float itemStatValue)
    {
        ItemName = itemName;
        ItemTag = itemTag;
        ItemDescription = itemDescription;
        ItemPrice = itemPrice;
        ItemStatValue = itemStatValue;
    }
}