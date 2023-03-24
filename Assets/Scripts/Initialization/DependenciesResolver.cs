using System.Linq;
using ColonizationMobileGame.GameFinalization;
using ColonizationMobileGame.InteractablesTracking;
using UnityEngine;

namespace ColonizationMobileGame.Initialization
{
    public class DependenciesResolver : MonoBehaviour
    {
        [SerializeField] private GameFinalizer gameFinalizer;
        
        private InteractablesTracker _interactablesTracker;

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
        }


        private void SetDependencies()
        {
            SetInteractablesTracker();

            SetCameraForCanvases();
            
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


        private void SetGameOverTargets()
        {
            foreach (IGameFinalizationTarget target in _allComponents.OfType<IGameFinalizationTarget>())
            {
                target.SubscribeToGameOver(gameFinalizer);
            }
        }


        public void ResolveAfterRestoringSave()
        {
            
        }
    }
}