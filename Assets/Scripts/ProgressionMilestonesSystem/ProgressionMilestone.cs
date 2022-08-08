using System;
using UnityEngine;

namespace ColonizationMobileGame.ProgressionTracking
{
    public abstract class ProgressionMilestone : MonoBehaviour, IProgressionMilestone
    {
        protected abstract ProgressionMilestoneType Type { get; }
        
        public event Action<ProgressionMilestoneType> Achieved;


        private void OnEnable()
        {
            Subscribe();
        }


        protected abstract void Subscribe();


        private void OnDisable()
        {
            Unsubscribe();
        }


        protected abstract void Unsubscribe();


        protected void InvokeAchieved()
        {
            Achieved?.Invoke(Type);

            gameObject.SetActive(false);
        }
    }
}