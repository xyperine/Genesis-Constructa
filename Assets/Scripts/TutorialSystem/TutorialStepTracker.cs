using System;
using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem
{
    public abstract class TutorialStepTracker : MonoBehaviour
    {
        [SerializeField] private TutorialStep step;

        private TutorialItem[] _items;
        
        public TutorialStep Step => step;

        public event Action Completed;


        public void SetItems(TutorialItem[] items)
        {
            _items = items;
        }
        

        public void Activate()
        {
            foreach (TutorialItem item in _items)
            {
                item.Activate();
            }
        }


        protected void InvokeCompleted()
        {
            Completed?.Invoke();
            
            enabled = false;
        }
    }
}