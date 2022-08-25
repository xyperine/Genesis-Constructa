using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [SerializeField] private string fileName;

        private string _fullPath;

        private Dictionary<string, object> _gameState = new Dictionary<string, object>();

        private SaveSerializer<Dictionary<string, object>> _saveSerializer;


        public void Initialize()
        {
            _fullPath = string.Join('/', Application.persistentDataPath, fileName);
            _saveSerializer = new SaveSerializer<Dictionary<string, object>>(_fullPath);
            
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