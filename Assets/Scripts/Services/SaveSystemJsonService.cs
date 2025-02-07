using System;
using System.IO;
using UnityEngine;

namespace Save
{
    public class SaveSystemJsonService : ISaveSystem
    {
        private string _saveFilePath = "save.json";

        public void Save(object data)
        {
            string jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(_saveFilePath, jsonData);
        }
        
        public T Load<T>()
        {
            if (File.Exists(_saveFilePath))
            {
                string jsonData = File.ReadAllText(_saveFilePath);
                T data = JsonUtility.FromJson<T>(jsonData);
                return data;
            }
            else
            {
                return Activator.CreateInstance<T>();
            }
        }

        public bool HasSave()
        {
            return File.Exists(_saveFilePath);
        }

        public void DeleteSave()
        {
            if (File.Exists(_saveFilePath))
            {
                File.Delete(_saveFilePath);
                Debug.Log("Сохранение удалено.");
            }
            else
            {
                Debug.Log("Сохранение не найдено.");
            }
        }
    }
}