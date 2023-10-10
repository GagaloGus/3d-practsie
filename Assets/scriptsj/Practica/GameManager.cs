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
    bool saveDataType;
    string fileName = "playerData";
    List<string> dateList = new List<string>();
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

        saveDataType = FindObjectOfType<SwapJSONTXT>().value;

        void SaveGame()
        {
            //añade la fecha en el momento de guardado
            dateList.Add(System.DateTime.Now.ToString("HH:mm:ss"));

            //crea una lista y guarda la posicion y rotacion del jugador
            List<string> PlayerinfoList = new List<string>
            {
                player.transform.position.ToString(),
                player.transform.rotation.eulerAngles.ToString(),
                score.ToString()
            };

            //crea una tercera lista fusionando la del player y la de la fecha
            List<string> dataToSave = new List<string>();
            dataToSave.AddRange(PlayerinfoList);
            dataToSave.AddRange(dateList);

            //guarda los datos
            if (saveDataType) 
            { 
                TextFileManager.instance.Save(fileName, dataToSave);
            }
            else 
            { 
                JSONFileManager.instance.Save(fileName, dataToSave);
            }
            print($"<color=green>Juego Guardado {(saveDataType ? "TXT" : "Json")} !</color> ");
        }

        void LoadGame()
        {
            try
            {
                //se guarda la lista de datos de guardado

                List<string> loadData = new List<string>();
                if (saveDataType)
                {
                    loadData = TextFileManager.instance.Load(fileName);
                }
                else
                {
                    loadData = JSONFileManager.instance.Load(fileName);
                }

                //establece la posicion y rotacion del jugador
                player.transform.position = StringToVector3(loadData[0]);
                player.transform.rotation = Quaternion.Euler(StringToVector3(loadData[1]));
                score = int.Parse(loadData[2]);
                //se guarda las fechas de antes
                for(int i = 3; i < loadData.Count; i++)
                {
                    dateList.Add(loadData[i]);
                }
                print($"<color=cyan>Juego cargado { (saveDataType ? "TXT" :"Json")} !</color> ");
                

            }
            catch { Debug.LogWarning($"No hay ningun archivo de guardado {(saveDataType ? "TXT" : "Json")} aún \n Presiona G para guardar! "); }
        }

        #endregion
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        FindObjectOfType<TextController>().StartScoreFade();
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

    public static Vector3 StringToVector3(string s)
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
