using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    public string filePath;
    public string fileName;

    public Data Data{get;set;}

    private void Awake()
    {
        instance = this; 
    }

    private void Start() {
        this.Data = new Data();
    }

    [ContextMenu("세이브!")]
    public void Save()
    {
        string fullPath = Path.Combine(filePath, fileName);

        try
        {
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            }

            string json = JsonUtility.ToJson(Data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter write = new StreamWriter(stream))
                {
                    write.Write(json);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Save Error error_message: {e.Message}");
        }

        print("Save sucessed!");
    }

    public void NewGame()
    {
        this.Data = new Data();//데이터 새로 만들어서 넣어줌
    }

    [ContextMenu("LoadData")]
    public void Load()
    {
        string fullPath = Path.Combine(filePath, fileName);
        try
        {
            string loadData = "";
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    loadData = reader.ReadToEnd();
                }
            }
            this.Data = JsonUtility.FromJson<Data>(loadData);
        }
        catch (Exception e)
        {
            Debug.LogError($"Load Error error_message: {e.Message}");
        }
    }

    private void OnApplicationQuit()
    {
        Save();//나갈때 세이브해줌
    }
}
