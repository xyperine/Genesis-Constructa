using ColonizationMobileGame.TutorialSystem;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target.Conditions
{
    public class TutorialStepCompleteCondition : ArrowPointerTargetInvalidationCondition
    {
        private static TutorialBuilder _tutorialBuilder;
        
        
        public override void StartTracking(ArrowPointerTarget target)
        {
            if (!_tutorialBuilder)
            {
                _tutorialBuilder = Object.FindObjectOfType<TutorialBuilder>();
            }
            
            _tutorialBuilder.SubscribeToCurrentStepCompletion(InvokeSatisfied);
        }


        public override void Dispose()
        {
            
        }
    }
}