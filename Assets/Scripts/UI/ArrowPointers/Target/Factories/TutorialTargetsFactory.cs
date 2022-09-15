using ColonizationMobileGame.UI.ArrowPointers.Target.Conditions;

namespace ColonizationMobileGame.UI.ArrowPointers.Target.Factories
{
    public class TutorialTargetsFactory : TargetsFactory
    {
        protected override ArrowPointerTargetInvalidationCondition[] Conditions => new ArrowPointerTargetInvalidationCondition[]
        {
            new TutorialStepCompleteCondition(),
        };
    }
}