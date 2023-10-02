using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONTest : MonoBehaviour
{
    public string jsonFileName;
    string jsonPath;

    [System.Serializable]
    struct PlayerInfo
    {
        public Vector3 position;
    }
    // Start is called before the first frame update
    void Start()
    {
        jsonFileName = jsonFileName + ".json";
        jsonPath = Application.persistentDataPath + "\\" + jsonFileName;

        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveGame();
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    void SaveGame()
    {
        PlayerInfo info = new PlayerInfo();
        info.position = transform.position;

        StreamWriter writer = new StreamWriter(jsonPath);
        string jsonInfo = JsonUtility.ToJson(info);
        writer.WriteLine(jsonInfo);

        writer.Close();

        print("<color=green>saved game !</color>");
    }

    void LoadGame()
    {
        if (File.Exists(jsonPath))
        {
            StreamReader sr = new StreamReader(jsonPath);
            string JsonFile = sr.ReadToEnd();

            PlayerInfo info = JsonUtility.FromJson<PlayerInfo>(JsonFile);
            transform.position = info.position;

            sr.Close();

            print("<color=cyan>loaded game !</color> ");
        }
    }
}
