using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        private Dictionary<string, object> _gameState = new Dictionary<string, object>();
        private SaveSerializer<Dictionary<string, object>> _saveSerializer;


        public void Initialize()
        {
            _saveSerializer = new SaveSerializer<Dictionary<string, object>>();
            
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
                if (_gameState.TryGetValue(saveable.Guid.Value, out object value))
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
                _gameState[saveable.Guid.Value] = saveable.Save();
            }
        }
    }
}