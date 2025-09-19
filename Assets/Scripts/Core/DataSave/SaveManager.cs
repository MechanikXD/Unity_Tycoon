using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.DataSave
{
    public static class SaveManager
    {
        private static Dictionary<string, ISaveAble> _saveData = new();
        private const string SAVE_DATA_FILE_NAME = "SaveData.json";

        public static void Register(string key, ISaveAble obj)
        {
            _saveData[key] = obj;
        }

        public static void SaveToFile()
        {
            var container = new SaveDataContainer();

            foreach (var kvp in _saveData)
            {
                var key = kvp.Key;
                object data = kvp.Value.SaveData();

                var json = JsonUtility.ToJson(data);
                container._entries.Add(new SaveEntry(key, json));
            }

            var fullPath = Path.Combine(Application.persistentDataPath, SAVE_DATA_FILE_NAME);
            File.WriteAllText(fullPath, JsonUtility.ToJson(container, true));
        }

        public static void LoadFromFile()
        {
            var fullPath = Path.Combine(Application.persistentDataPath, SAVE_DATA_FILE_NAME);
            if (!File.Exists(fullPath)) return;

            var json = File.ReadAllText(fullPath);
            if (string.IsNullOrEmpty(json)) return;

            var container = JsonUtility.FromJson<SaveDataContainer>(json);

            foreach (var entry in container._entries)
            {
                if (_saveData.TryGetValue(entry._key, out ISaveAble obj))
                {
                    // Use the type of saved data from the object itself
                    var dataType = obj.SaveData().GetType();
                    var typedData = JsonUtility.FromJson(entry._jsonData, dataType);
                    obj.LoadData(typedData);
                }
            }
        }

        public static void ClearSaveData()
        {
            _saveData = new();
            var path = Path.Combine(Application.persistentDataPath, SAVE_DATA_FILE_NAME);
            File.WriteAllText(path, string.Empty);
        }
    }
    
    
    [Serializable]
    public class SaveDataContainer
    {
        public List<SaveEntry> _entries;

        public SaveDataContainer(List<SaveEntry> entries)
        {
            _entries = entries;
        }
        
        public SaveDataContainer()
        {
            _entries = new List<SaveEntry>();
        }
    }

    [Serializable]
    public class SaveEntry
    {
        public string _key;
        public string _jsonData;

        public SaveEntry(string key, string json)
        {
            _key = key;
            _jsonData = json;
        }
    }
}