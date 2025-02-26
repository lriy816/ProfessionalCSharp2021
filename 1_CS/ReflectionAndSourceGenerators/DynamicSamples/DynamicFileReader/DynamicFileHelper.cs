﻿namespace DynamicFileReader;

public static class DynamicFileHelper
{
    public static IEnumerable<dynamic> ParseFile(string fileName)
    {
        List<dynamic> retList = new();
        using StreamReader? reader = OpenFile(fileName);
        if (reader != null)
        {
            string[] headerLine = reader.ReadLine()?.Split(',').Select(s => s.Trim()).ToArray() ?? throw new InvalidOperationException("reader.ReadLine returned null");
            while (reader.Peek() > 0)
            {
                string[] dataLine = reader.ReadLine()?.Split(',') ?? throw new InvalidOperationException("reader.Readline returned null");
                dynamic dynamicEntity = new ExpandoObject();
                for (int i = 0; i < headerLine.Length; i++)
                {
                    ((IDictionary<string, object>)dynamicEntity).Add(headerLine[i], dataLine[i]);
                }
                retList.Add(dynamicEntity);
            }
        }

        return retList;
    }

    private static StreamReader? OpenFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            return new StreamReader(File.OpenRead(fileName));
        }
        return null;
    }
}
