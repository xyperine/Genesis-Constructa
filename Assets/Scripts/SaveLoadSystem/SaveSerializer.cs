using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    public class SaveSerializer<T>
        where T : new()
    {
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        };

        private readonly string _fullPath;


        public SaveSerializer(string fullPath)
        {
            _fullPath = fullPath;
        }
        
        
        public T ReadFile()
        {
            if (!File.Exists(_fullPath))
            {
                return new T();
            }

            string json = File.ReadAllText(_fullPath);
            return JsonConvert.DeserializeObject<T>(json, _serializerSettings);
        }

        
        public void SaveFile(T gameState)
        {
            if (!Directory.Exists(Application.persistentDataPath))
            {
                Directory.CreateDirectory(Application.persistentDataPath);
            }

            string json = JsonConvert.SerializeObject(gameState, _serializerSettings);
            File.WriteAllText(_fullPath, json);
        }
    }
}