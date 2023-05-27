using GenesisConstructa.TutorialSystem;
using UnityEngine;

namespace GenesisConstructa.UI.ArrowPointers.Tutorial
{
    public class TutorialArrowPointerTargetProvider : ArrowPointerTargetProvider<TutorialArrowPointerTargetGroupValidator>
    {
        [SerializeField] private TutorialStepTracker stepTracker;


        private void OnEnable()
        {
            stepTracker.Activated += Register;
        }


        protected override ArrowPointerTarget GetTarget()
        {
            return new TutorialArrowPointerTarget(stepTracker, transformToPointAt);
        }
        
        
        private void OnDisable()
        {
            stepTracker.Activated -= Register;
        }
    }
}