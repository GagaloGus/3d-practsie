using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileTest : MonoBehaviour
{
    public string fileName = "playerData";
    string filePath;

    GameObject playerCamera;

    void Awake()
    {
        filePath = Application.persistentDataPath + "\\" + fileName;
        playerCamera = transform.Find("bicho").gameObject.transform.Find("Player camera").gameObject;
    }

    // guardar partida
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)){
            SaveGame();
        }
        
        if (Input.GetKeyDown(KeyCode.L)){
            LoadGame();
        }

    }

    void SaveGame()
    {
        
        FileStream fs = File.Create(filePath);

        StreamWriter streamWriter = new StreamWriter(fs);
        streamWriter.WriteLine(transform.position.ToString());

        streamWriter.WriteLine(transform.rotation.eulerAngles.ToString());
        streamWriter.WriteLine(playerCamera.transform.rotation.eulerAngles.ToString());


        streamWriter.WriteLine(transform.localScale.ToString());


        streamWriter.Close();
        fs.Close();

        print("<color=green>saved game !</color>");
    }

    void LoadGame()
    {
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);

            transform.position = StringToVector3(sr.ReadLine());

            transform.rotation = Quaternion.Euler(StringToVector3(sr.ReadLine()));
            playerCamera.transform.rotation = Quaternion.Euler(StringToVector3(sr.ReadLine()));

            transform.localScale = StringToVector3(sr.ReadLine());
            sr.Close();

            print("<color=cyan>loaded game !</color> ");
        }
    }

    Vector3 StringToVector3(string s)
    {
        //por si acaso el string no empieza por ()
        if (s.StartsWith("(") && s.EndsWith(")")) s = s.Substring(1, s.Length - 2);

        string[] division = s.Split(',');

        for (int i = 0; i < division.Length; i++) 
        {
            division[i] = division[i].Replace('.', ',');
        }

        Vector3 result = new Vector3(
            float.Parse(division[0]),
            float.Parse(division[1]),
            float.Parse(division[2])
            );
        
        return result;
    }

}
