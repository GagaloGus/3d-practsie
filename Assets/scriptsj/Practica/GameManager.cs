using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    GameObject player;
    float time;
    int score;

    string fileName = "playerData";
    void Awake()
    {
        if (!instance) //comprueba que instance no tenga informacion
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //si tiene info, ya existe un GM
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }
    private void Update()
    {
        time += Time.deltaTime;
        #region save and load
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }

        void SaveGame()
        {
            List<string> infoList = new List<string>
            {
                player.transform.position.ToString(),
                player.transform.rotation.eulerAngles.ToString()
            };

            TextFileManager.instance.Save(fileName, infoList);
            print("<color=green>Juego guardado !</color>");
        }

        void LoadGame()
        {
            try
            {
                List<string> loadData = TextFileManager.instance.Load(fileName);
                player.transform.position = StringToVector3(loadData[0]);
                player.transform.rotation = Quaternion.Euler(StringToVector3(loadData[1]));
                print("<color=cyan>Juego cargado !</color> ");
            }
            catch { Debug.LogWarning($"No hay ningun archivo de guardado aun \n Presiona G para guardar! "); }
        }

        #endregion
    }

    public void UpdateScore(int scoreToAdd)
    {
        FindObjectOfType<TextController>().StartScoreFade(scoreToAdd);
    }

    public float _time
    {
        get { return time; }
    }
    public int _score
    {
        get { return score; }
        set { score = value; }
    }

    public string _fileName
    {
        get { return fileName; }
    }

    public float MapValues(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }

    public Vector3 StringToVector3(string s)
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
