using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            player = FindObjectOfType<Player>().gameObject;
        }
        else //si tiene info, ya existe un GM
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadInStartup();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }

    }
    #region save and load
    void SaveGame()
    {
        saveDataType = FindObjectOfType<SwapJSONTXT>().value;
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
        saveDataType = FindObjectOfType<SwapJSONTXT>().value;
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

            //establece la posicion, rotacion y puntaje del jugador
            player.transform.position = StringToVector3(loadData[0]);
            player.transform.rotation = Quaternion.Euler(StringToVector3(loadData[1]));
            
            //cambia el score a 0 y le añade todo el score de golpe
            score = 0;
            UpdateScore(int.Parse(loadData[2]));
            //se guarda las fechas de antes
            for (int i = 3; i < loadData.Count; i++)
            {
                dateList.Add(loadData[i]);
            }
            print($"<color=cyan>Juego cargado {(saveDataType ? "TXT" : "Json")} !</color> ");


        }
        catch { Debug.LogWarning($"No hay ningun archivo de guardado {(saveDataType ? "TXT" : "Json")} aún \n Presiona G para guardar! "); }
    }

    void LoadInStartup()
    {
        string filepath = $"{Application.persistentDataPath}\\{fileName}";
        //si existen ambos txt y json
        if (File.Exists(filepath + ".txt") && File.Exists(filepath + ".json")){
            //se guardan todas las lineas de ambos archivos txt y json
            string[] txtLines = TextFileManager.instance.Load(fileName).ToArray();
            string[] jsonLines = JSONFileManager.instance.Load(fileName).ToArray();

            //cambia la ultima linea de cada uno (fecha mas reciente) a DateTime
            DateTime txtTime = DateTime.ParseExact(txtLines[txtLines.Length-1], "HH:mm:ss",
                                        CultureInfo.InvariantCulture);
            DateTime jsonTime = DateTime.ParseExact(jsonLines[jsonLines.Length-1], "HH:mm:ss",
                                        CultureInfo.InvariantCulture);

            //Carga el archivo segun el que tenga la fecha mas reciente
            FindObjectOfType<SwapJSONTXT>().value = txtTime > jsonTime;
            LoadGame();
        }
        //si solo existe el txt
        else if (File.Exists(filepath + ".txt")){
            FindObjectOfType<SwapJSONTXT>().value = true;
            LoadGame();
        }
        //si solo existe el json
        else if (File.Exists(filepath + ".json")){
            FindObjectOfType<SwapJSONTXT>().value = false;
            LoadGame();
        }
        else {
            Debug.LogWarning($"No hay ningun archivo de guardado");
        }
    }

    #endregion
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
        if (s.StartsWith("(") && s.EndsWith(")"))
            s = s.Substring(1, s.Length - 2);

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
