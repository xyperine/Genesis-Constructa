using System.Linq;
using ColonizationMobileGame.GameFinalization;
using ColonizationMobileGame.InteractablesTracking;
using ColonizationMobileGame.TutorialSystem;
using ColonizationMobileGame.UI.ArrowPointers.Target;
using ColonizationMobileGame.UI.ArrowPointers.Target.Factories;
using UnityEngine;

namespace ColonizationMobileGame.Initialization
{
    public class DependenciesResolver : MonoBehaviour
    {
        [SerializeField] private TutorialBuilder tutorialBuilder;
        [SerializeField] private GameFinalizer gameFinalizer;
        
        private InteractablesTracker _interactablesTracker;
        private ArrowPointersTargetsManager _arrowPointersTargetsManager;
        
        private GameFinalizationTrigger[] _gameOverTriggers;

        private Component[] _allComponents;


        public void ResolveBeforeRestoringSave()
        {
            GetObjects();
            
            SetDependencies();
        }


        private void GetObjects()
        {
            _allComponents = FindObjectsOfType<Component>(true);
            
            _interactablesTracker = FindObjectOfType<InteractablesTracker>();
            _arrowPointersTargetsManager = FindObjectOfType<ArrowPointersTargetsManager>();

            _gameOverTriggers = FindObjectsOfType<GameFinalizationTrigger>(true);
        }


        private void SetDependencies()
        {
            SetInteractablesTracker();

            SetCameraForCanvases();

            SetGameOverTriggers();
            SetGameOverTargets();
        }


        private void SetInteractablesTracker()
        {
            foreach (IInteractablesTrackerUser dataUser in _allComponents.OfType<IInteractablesTrackerUser>())
            {
                dataUser.SetInteractablesTracker(_interactablesTracker);
            }
        }


        private void SetCameraForCanvases()
        {
            foreach (Canvas canvas in _allComponents.OfType<Canvas>())
            {
                canvas.worldCamera ??= Camera.main;
            }
        }


        private void SetGameOverTriggers()
        {
            gameFinalizer.Initialize(_gameOverTriggers);
        }


        private void SetGameOverTargets()
        {
            foreach (IGameFinalizationTarget target in _allComponents.OfType<IGameFinalizationTarget>())
            {
                target.SubscribeToGameOver(gameFinalizer);
            }
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

            _arrowPointersTargetsManager.SetFactory(targetsFactory);
        }
    }
}