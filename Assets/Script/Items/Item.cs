using System;
using System.Collections.Generic;
using Unity.VisualScripting;

[Serializable]
public class Item
{
    public string ItemName;
    public string ItemTag;
    public string ItemDescription;
    public float ItemPrice;
    public int ItemStatsInvolved;
    public List<int> ValueOfStats;

    public Item(string itemName, string itemTag, string itemDescription, float itemPrice, int itemStatsInvolved, string statsValue)
    {
        ItemName = itemName;
        ItemTag = itemTag;
        ItemDescription = itemDescription;
        ItemPrice = itemPrice;
        ItemStatsInvolved = itemStatsInvolved;

        ValueOfStats = new List<int>();

        string[] statValues = statsValue.Split('-');
        foreach (string str in statValues)
        {
            if (int.TryParse(str, out int value))
            { 
                ValueOfStats.Add(value);
            }
        }
    }
}