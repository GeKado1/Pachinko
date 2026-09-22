using System;
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

        try
        {
            using (var reader = new StringReader(csvText))
            {
                string headerLine = reader.ReadLine();
            
                if (headerLine == null)
                {
                    return result;
                }

                var headers = headerLine.Split(',');
                for (int i = 0; i < headers.Length; i++)
                {
                    headers[i] = headers[i].Trim();
                }

                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var values = line.Split(",");
                    var row = new Dictionary<string, string>();

                    for (int i = 0; i < headers.Length; i++)
                    {
                        string value = (i < values.Length) ? values[i].Trim() : "";
                        row[headers[i]] = value;
                    }

                    result.Add(row);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error reading CSV file: {e.Message}");
        }

        return result;
    }
}