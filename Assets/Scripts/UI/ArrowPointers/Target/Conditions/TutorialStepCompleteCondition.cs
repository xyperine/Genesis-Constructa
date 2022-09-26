using ColonizationMobileGame.TutorialSystem;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target.Conditions
{
    public class TutorialStepCompleteCondition : ArrowPointerTargetInvalidationCondition
    {
        private static TutorialTracker _tutorialTracker;
        
        
        public override void StartTracking(ArrowPointerTarget target)
        {
            if (!_tutorialTracker)
            {
                _tutorialTracker = Object.FindObjectOfType<TutorialTracker>();
            }
            
            _tutorialTracker.SubscribeToCurrentStepCompletion(InvokeSatisfied);
        }


        public override void Dispose()
        {
            
        }
    }
}