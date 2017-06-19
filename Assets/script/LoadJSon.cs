using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadJSon {
    public static GameStatus1 loadJsonFromFile()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (!File.Exists(Application.dataPath + "/script/data.json"))
        {
            return null;
        }

        StreamReader sr = new StreamReader(Application.dataPath + "/script/data.json");

        if (sr == null)
        {
            return null;
        }
        string json = sr.ReadToEnd();

        if (json.Length > 0)
        {
            return JsonUtility.FromJson<GameStatus1>(json);
        }

        return null;
    }
}
