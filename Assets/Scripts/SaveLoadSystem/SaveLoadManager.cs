using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [SerializeField] private bool active = true;
        
        private Dictionary<string, object> _gameState = new Dictionary<string, object>();
        private SaveSerializer<Dictionary<string, object>> _saveSerializer;


        public void Initialize()
        {
            if (!active)
            {
                return;
            }

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
            ISceneSaveable[] orderedSaveables = FindObjectsOfType<MonoBehaviour>(true).OfType<ISceneSaveable>()
                .OrderBy(s => s.LoadingOrder).ToArray();

            foreach (ISceneSaveable saveable in orderedSaveables)
            {
                if (_gameState.TryGetValue(saveable.Guid.Value, out object value))
                {
                    saveable.Load(value);
                }
            }
        }


        private void OnApplicationQuit()
        {
            if (!active)
            {
                return;
            }

            Save();
        }


        private void Save()
        {
            CaptureState();
            _saveSerializer.SaveFile(_gameState);
        }


        private void CaptureState()
        {
            foreach (ISceneSaveable saveable in FindObjectsOfType<MonoBehaviour>(true).OfType<ISceneSaveable>())
            {
                _gameState[saveable.Guid.Value] = saveable.Save();
            }
        }
    }
}