using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [Tooltip("File name without extension")]
        [SerializeField] private string fileName;
        
        private const string FILE_EXTENSION = ".json";

        private Dictionary<string, object> _gameState = new Dictionary<string, object>();
        private SaveSerializer<Dictionary<string, object>> _saveSerializer;


        public void Initialize()
        {
            string fullPath = string.Join('/', Application.persistentDataPath, fileName + FILE_EXTENSION);
            _saveSerializer = new SaveSerializer<Dictionary<string, object>>(fullPath);
            
            Load();
        }


        private void Load()
        {
            _gameState = _saveSerializer.ReadFile();
            RestoreState();
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


        private void OnApplicationQuit()
        {
            Save();
        }


        private void Save()
        {
            CaptureState();
            _saveSerializer.SaveFile(_gameState);
        }


        private void CaptureState()
        {
            foreach (ISaveable saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
            {
                _gameState[saveable.Guid] = saveable.Save();
            }
        }
    }
}