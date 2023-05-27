using GenesisConstructa.TutorialSystem;

namespace GenesisConstructa.UI.ArrowPointers.Tutorial
{
    public sealed class TutorialArrowPointerTargetGroupValidator : ArrowPointerTargetGroupValidator
    {
        protected override bool TargetIsValid(ArrowPointerTarget target)
        {
            TutorialStepTracker stepTracker = ((TutorialArrowPointerTarget) target).StepTracker;
            
            bool valid = !stepTracker.Complete;
            
            if (!valid)
            {
                targets.Remove(target);
            }
            
            return valid;
        }
    }
}