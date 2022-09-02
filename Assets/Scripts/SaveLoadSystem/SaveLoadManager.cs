using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [SerializeField] private LoadingCoordinator loadingCoordinator;
        
        [SerializeField, HideInInspector] private bool active;
        
        private Dictionary<string, object> _gameState = new Dictionary<string, object>();
        private SaveSerializer<Dictionary<string, object>> _saveSerializer;


        public void SetActive(bool active)
        {
            this.active = active;
        }


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
            List<ISceneSaveable> orderedSaveables =
                loadingCoordinator.OrderData(FindObjectsOfType<MonoBehaviour>(true).OfType<ISceneSaveable>());

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