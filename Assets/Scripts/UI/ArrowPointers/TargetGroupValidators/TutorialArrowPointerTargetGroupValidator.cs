using System.Collections.Generic;
using ColonizationMobileGame.TutorialSystem;

namespace ColonizationMobileGame.UI.ArrowPointers.TargetGroupValidators
{
    public class TutorialArrowPointerTargetGroupValidator : ArrowPointerTargetGroupValidator
    {
        private readonly Dictionary<ArrowPointerTarget, TutorialStepTracker> _targetsStepTrackers =
            new Dictionary<ArrowPointerTarget, TutorialStepTracker>();
        

        protected override bool TargetIsValid(ArrowPointerTarget target)
        {
            if (!_targetsStepTrackers.ContainsKey(target))
            {
                _targetsStepTrackers.Add(target, target.Transform.GetComponent<TutorialStepTracker>());
            }

            bool valid = !_targetsStepTrackers[target].Complete;
            
            if (!valid)
            {
                targets.Remove(target);
                _targetsStepTrackers.Remove(target);
            }
            
            return valid;
        }
    }
}