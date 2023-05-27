using System.Linq;
using GenesisConstructa.InteractablesTracking;
using UnityEngine;

namespace GenesisConstructa.Initialization
{
    public class DependenciesResolver : MonoBehaviour
    {
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


        public void ResolveAfterRestoringSave()
        {
            
        }
    }
}