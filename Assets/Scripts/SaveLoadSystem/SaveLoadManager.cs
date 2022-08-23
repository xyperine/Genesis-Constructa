using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [SerializeField] private string fileName;

        private string _fullPath;
        
        private Dictionary<string, object> _gameState = new Dictionary<string, object>();


        private void Awake()
        {
            _fullPath = string.Join('/', Application.persistentDataPath, fileName);

            LoadFile();
            RestoreState();
        }


        private void OnApplicationQuit()
        {
            CaptureState();
            SaveFile();
        }


        private void CaptureState()
        {
            foreach (ISaveable saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
            {
                _gameState[saveable.Guid] = saveable.Save();
            }
        }


        private void RestoreState()
        {
            foreach (ISaveable saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
            {
                if (_gameState.TryGetValue(saveable.Guid, out object value))
                {
                    saveable.Load(value);
                }
            }
        }
        

        private void SaveFile()
        {
            if (!Directory.Exists(Application.persistentDataPath))
            {
                Directory.CreateDirectory(Application.persistentDataPath);
            }
            
            string json = JsonUtility.ToJson(_gameState);
            File.WriteAllText(_fullPath, json);
        }


        private void LoadFile()
        {
            if (!File.Exists(_fullPath))
            {
                _gameState = new Dictionary<string, object>();
                return;
            }

            string json = File.ReadAllText(_fullPath);
            _gameState = JsonUtility.FromJson<Dictionary<string, object>>(json);
        }
    }
}