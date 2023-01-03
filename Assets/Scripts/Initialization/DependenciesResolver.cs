using System.Linq;
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
        [SerializeField] private TutorialBuilder tutorialBuilder;
        
        private LevelData _levelData;
        
        
        public void ResolveBeforeRestoringSave(GameObject[] rootGameObjects)
        {
            _levelData = FindObjectOfType<LevelData>();
            
            SetInteractablesTracker();

            foreach (GameObject rootGameObject in rootGameObjects)
            {
                SetCameraForCanvases(rootGameObject);
                SetLevelData(rootGameObject);
            }

            SetScoreModifier();
        }


        private static void SetInteractablesTracker()
        {
            InteractablesTracker interactablesTracker = FindObjectOfType<InteractablesTracker>();
            foreach (IInteractablesTrackerUser dataUser in FindObjectsOfType<MonoBehaviour>(true)
                         .OfType<IInteractablesTrackerUser>())
            {
                dataUser.SetInteractablesTracker(interactablesTracker);
            }
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


        private void SetScoreModifier()
        {
            ScoreModifier scoreModifier = FindObjectOfType<ScoreModifier>();
            tasksInitializer.SetScoreCounter(scoreModifier);
        }


        public void ResolveAfterRestoringSave()
        {
            SetArrowPointerTargetFactory();
            tutorialBuilder.Completed += SetArrowPointerTargetFactory;
        }


        private void SetArrowPointerTargetFactory()
        {
            TargetsFactory targetsFactory;
            if (tutorialBuilder.Complete)
            {
                targetsFactory = new RegularTargetsFactory();
            }
            else
            {
                targetsFactory = new TutorialTargetsFactory();
            }

            FindObjectOfType<ArrowPointersTargetsManager>().SetFactory(targetsFactory);
        }
    }
}