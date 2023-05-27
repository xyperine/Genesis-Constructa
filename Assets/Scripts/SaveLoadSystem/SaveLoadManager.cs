using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GenesisConstructa.SaveLoadSystem
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


#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            if (!active)
            {
                return;
            }

            Save();
        }
#else
        private void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
            {
                return;
            }
            
            if (!active)
            {
                return;
            }

            Save();
        }
#endif


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


        public void DisableSavingForThisSession()
        {
            if (!active)
            {
                return;
            }

            active = false;
            _saveSerializer.DeleteFile();
        }
    }
}