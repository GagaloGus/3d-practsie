using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextFileManager : AbstractFileManager
{
    public static TextFileManager instance = new TextFileManager();
    string filePath = $"{Application.persistentDataPath}\\" ;
    public override List<string> Load(string filename)
    {
        if(File.Exists(filePath + filename + ".txt")) 
        {
            StreamReader sr = new StreamReader(filePath + filename + ".txt");
            List<string> result = new List<string>();

            string line;
            // lee y añade las lineas hasta el final del documento
            while ((line = sr.ReadLine()) != null)
            {
                result.Add(line);
            }
            return result;
        }
        else { return null; }
    }

    public override void Save(string filename, List<string> content)
    {
        FileStream fs = File.Create(filePath + filename + ".txt");

        StreamWriter streamWriter = new StreamWriter(fs);

        foreach (string line in content )
        {
            streamWriter.WriteLine(line);
        }

        streamWriter.Close();
        fs.Close();
    }

}
