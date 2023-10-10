using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFileManager
{
    //protected static AbstractFileManager instance = null;
    protected string filePath = $"{Application.persistentDataPath}\\";
    public abstract void Save(string filename, List<string> content);
    public abstract List<string> Load(string filename);

}
