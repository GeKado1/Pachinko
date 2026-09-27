using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{
    public string FileName;

    private CSVReader CSVReader;

    private List<Dictionary<string, string>> Data;

    [SerializeField] private ItemManager ItemManager;

    void Awake()
    {
        CSVReader = new CSVReader();

        FindingData(FileName);
    }

    private void FindingData(string fileName)
    {
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        string resourcePath = $"{fileNameWithoutExtension}";

        TextAsset csvAsset = Resources.Load<TextAsset>(resourcePath);

        if (csvAsset == null)
        {
            Debug.LogError($"[ItemLoader] No se encontró el archivo CSV en: Resources/{resourcePath}");
            return;
        }

        Data = CSVReader.ReadCSV(csvAsset.text);

        LoadData();
    }

    private void LoadData() 
    {
        if (Data.Count > 0)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                var row = Data[i];

                if (row != null)
                {
                    string itemName = "";
                    string itemTag = "";
                    string itemDescription = "";
                    float itemPrice = 0f;
                    int statsInvolved = 0;
                    string itemStatsValue = "";

                    if (row.ContainsKey("ITEM_NAME"))
                    {
                        itemName = row["ITEM_NAME"];
                    }
                    if (row.ContainsKey("ITEM_TAG"))
                    {
                        itemTag = row["ITEM_TAG"];
                    }
                    if (row.ContainsKey("ITEM_DESCRIPTION"))
                    {
                        itemDescription = row["ITEM_DESCRIPTION"];
                    }
                    if (row.ContainsKey("ITEM_PRICE"))
                    {
                        float.TryParse(row["ITEM_PRICE"], out itemPrice);
                    }
                    if (row.ContainsKey("NUMBER_STATS_INVOLVED"))
                    {
                        int.TryParse(row["NUMBER_STATS_INVOLVED"], out statsInvolved);
                    }
                    if (row.ContainsKey("ITEM_STATS_VALUE"))
                    {
                        itemStatsValue = row["ITEM_STATS_VALUE"];
                    }

                    Item item = new Item(itemName, itemTag, itemDescription, itemPrice, statsInvolved, itemStatsValue);
                    ItemManager.AllItems.Add(item);
                }
            }
        }
    }
}