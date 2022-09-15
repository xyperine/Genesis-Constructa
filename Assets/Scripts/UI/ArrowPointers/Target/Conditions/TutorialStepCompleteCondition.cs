using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target.Conditions
{
    public class TutorialStepCompleteCondition : ArrowPointerTargetInvalidationCondition
    {
        public override void StartTracking(ArrowPointerTarget target)
        {
            Debug.Log("Tutorial condition");
        }
    }
}