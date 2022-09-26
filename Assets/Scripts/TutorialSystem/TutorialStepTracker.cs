using System;
using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem
{
    public abstract class TutorialStepTracker : MonoBehaviour
    {
        [SerializeField] private TutorialStep step;

        public TutorialStep Step => step;
        
        public event Action<TutorialStep> Completed;


        public void Activate(TutorialItem[] items)
        {
            foreach (TutorialItem item in items)
            {
                item.Activate();
            }
        }


        protected void InvokeCompleted()
        {
            Completed?.Invoke(step);
            enabled = false;
        }
    }
}