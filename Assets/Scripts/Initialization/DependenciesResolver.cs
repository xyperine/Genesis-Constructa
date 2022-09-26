using ColonizationMobileGame.Level;
using ColonizationMobileGame.ScoreSystem;
using ColonizationMobileGame.TasksSystem;
using ColonizationMobileGame.TutorialSystem;
using ColonizationMobileGame.UI.ArrowPointers.Target;
using ColonizationMobileGame.UI.ArrowPointers.Target.Factories;
using UnityEngine;

namespace ColonizationMobileGame.Initialization
{
    public class DependenciesResolver : MonoBehaviour
    {
        [SerializeField] private TasksInitializer tasksInitializer;
        [SerializeField] private TutorialTracker tutorialTracker;
        
        private LevelData _levelData;
        
        
        public void Resolve(GameObject[] rootGameObjects)
        {
            _levelData = FindObjectOfType<LevelData>();
            
            foreach (GameObject rootGameObject in rootGameObjects)
            {
                SetCameraForCanvases(rootGameObject);
                SetLevelData(rootGameObject);
            }

            SetArrowPointerTargetFactory();
            tutorialTracker.Completed += SetArrowPointerTargetFactory;

            SetScoreModifier();
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


        private void SetArrowPointerTargetFactory()
        {
            TargetsFactory targetsFactory;
            if (tutorialTracker.Complete)
            {
                targetsFactory = new RegularTargetsFactory();
            }
            else
            {
                targetsFactory = new TutorialTargetsFactory();
            }

            FindObjectOfType<ArrowPointersTargetsManager>().SetFactory(targetsFactory);
        }


        private void SetScoreModifier()
        {
            ScoreModifier scoreModifier = FindObjectOfType<ScoreModifier>();
            tasksInitializer.SetScoreCounter(scoreModifier);
        }
    }
}