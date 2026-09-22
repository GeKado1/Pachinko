using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader
{
    public List<Dictionary<string, string>> data;

    public List<Dictionary<string, string>> ReadCSV(string csvText)
    {
        var result = new List<Dictionary<string, string>>();

        if (string.IsNullOrEmpty(csvText))
        {
            Debug.LogWarning("[CSVReader] El contenido del texto CSV está vacío.");
            data = result;
            return result;
        }

        //try
        //{
        //    using (var reader = new StringReader(csvText))
        //    {
        //        string headerLine = reader.ReadLine();
                
        //        if (headerLine == null)
        //        {
        //            return result;
        //        }

        //        var headers = headerLine.Split(',');
        //    }
        //}

        return result;
    }
}