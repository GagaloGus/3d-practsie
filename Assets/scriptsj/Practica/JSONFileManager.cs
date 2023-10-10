using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONFileManager : AbstractFileManager
{
    public static JSONFileManager instance = new JSONFileManager();
    struct PlayerInfo
    {
        public Vector3 position;
        public Vector3 rotation;
        public int score;
        public List<string> dateList;
    }
    public override List<string> Load(string filename)
    {
        if(File.Exists(filePath + filename + ".json")) 
        {
            StreamReader sr = new StreamReader(filePath + filename + ".txt");
            string jsonFile = sr.ReadToEnd();

            PlayerInfo info = JsonUtility.FromJson<PlayerInfo>(jsonFile);

            List<string> result = new List<string>();

            result.Add(info.position.ToString());
            result.Add(info.rotation.ToString());
            result.Add(info.score.ToString());
            
            for(int i = 0; i < info.dateList.Count; i++)
            {
                result.Add(info.dateList[i]);
            }
            return result;
        }
        else { throw new Exception("No json savefile"); }
    }

    public override void Save(string filename, List<string> content)
    {
        StreamWriter streamWriter = new StreamWriter(filePath + filename + ".json");

        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.position = GameManager.StringToVector3(content[0]);
        playerInfo.rotation = GameManager.StringToVector3(content[1]);
        playerInfo.score = int.Parse(content[2]);
        playerInfo.dateList = new List<string>();
        
        for(int i = 3; i < content.Count; i++)
        {
            playerInfo.dateList.Add(content[i]);
        }

        string jsonInfo = JsonUtility.ToJson(playerInfo);
        streamWriter.WriteLine(jsonInfo);
        streamWriter.Close();
    }
}
