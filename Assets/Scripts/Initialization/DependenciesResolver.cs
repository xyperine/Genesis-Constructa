using ColonizationMobileGame.Level;
using ColonizationMobileGame.ScoreSystem;
using ColonizationMobileGame.TasksSystem;
using UnityEngine;

namespace ColonizationMobileGame.Initialization
{
    public class DependenciesResolver : MonoBehaviour
    {
        [SerializeField] private TasksInitializer tasksInitializer;
        
        private LevelData _levelData;
        
        
        public void Resolve(GameObject[] rootGameObjects)
        {
            _levelData = FindObjectOfType<LevelData>();
            ScoreModifier scoreModifier = FindObjectOfType<ScoreModifier>();
            
            foreach (GameObject rootGameObject in rootGameObjects)
            {
                SetCameraForCanvases(rootGameObject);
                SetLevelData(rootGameObject);
            }
            
            tasksInitializer.SetScoreCounter(scoreModifier);
        }
        
        
        private void SetCameraForCanvases(GameObject rootGameObject)
        {
            foreach (Canvas canvas in rootGameObject.GetComponentsInChildren<Canvas>(true))
            {
                canvas.worldCamera = Camera.main;
            }
        }


        private void SetLevelData(GameObject rootGameObject)
        {
            tasksInitializer.SetLevelData(_levelData);
            
            foreach (ILevelDataUser dataUser in rootGameObject.GetComponentsInChildren<ILevelDataUser>(true))
            {
                dataUser.SetLevelData(_levelData);
            }
        }
    }
}