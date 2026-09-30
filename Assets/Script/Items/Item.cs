using System;
using System.Collections.Generic;
using System.Globalization;

[Serializable]
public class Item
{
    public string ItemName;
    public string ItemTag;
    public string ItemDescription;
    public float ItemPrice;
    public int ItemStatsInvolved;
    public List<float> ValueOfStats;
    public string ItemSpriteSource;

    public Item(string itemName, string itemTag, string itemDescription, float itemPrice, int itemStatsInvolved, string statsValue, string imageSrc)
    {
        ItemName = itemName;
        ItemTag = itemTag;
        ItemDescription = itemDescription;
        ItemPrice = itemPrice;
        ItemStatsInvolved = itemStatsInvolved;

        ValueOfStats = new List<float>();

        string[] statValues = statsValue.Split('-');
        foreach (string str in statValues)
        {
            if (float.TryParse(str, out float value))
            { 
                ValueOfStats.Add(value);
            }
        }

        ItemSpriteSource = imageSrc;
    }
}