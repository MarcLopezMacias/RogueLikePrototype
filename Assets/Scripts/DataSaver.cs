using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataSaver : MonoBehaviour
{
    static public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/MySharedData.dat");

        MySharedData sharedData = new MySharedData();
        sharedData.lastScores = GameManager.Instance.GetComponent<ScoreManager>().lastScores;
        sharedData.maxScore = GameManager.Instance.GetComponent<ScoreManager>().maxScore;

        bf.Serialize(fs, sharedData);
        fs.Close();
    }

    static public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/MySharedData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/MySharedData.dat", FileMode.Open);
            MySharedData sharedData = bf.Deserialize(fs) as MySharedData;
            fs.Close();

            if (sharedData != null)
            {
                Debug.Log("Loading Save File...");
                GameManager.Instance.GetComponent<ScoreManager>().lastScores = sharedData.lastScores;
                GameManager.Instance.GetComponent<ScoreManager>().maxScore = sharedData.maxScore;
                Debug.Log($"Max Score: {sharedData.maxScore}" +
                    $"lastScores: {sharedData.lastScores}");
            }
            else Debug.Log("Save File not found...");

        }
    }
}