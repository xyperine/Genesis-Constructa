using UnityEngine;

namespace ColonizationMobileGame.SceneLoading
{
    public class DependenciesResolver : MonoBehaviour
    {
        private LevelData _levelData;
        
        
        public void Resolve(GameObject[] rootGameObjects)
        {
            _levelData = FindObjectOfType<LevelData>();
            
            foreach (GameObject rootGameObject in rootGameObjects)
            {
                SetCameraForCanvases(rootGameObject);
                SetLevelDataToObjects(rootGameObject);
            }
        }
        
        
        private void SetCameraForCanvases(GameObject rootGameObject)
        {
            foreach (Canvas canvas in rootGameObject.GetComponentsInChildren<Canvas>(true))
            {
                canvas.worldCamera = Camera.main;
            }
        }


        private void SetLevelDataToObjects(GameObject rootGameObject)
        {
            foreach (ILevelDataUser dataUser in rootGameObject.GetComponentsInChildren<ILevelDataUser>(true))
            {
                dataUser.SetLevelData(_levelData);
            }
        }
    }
}