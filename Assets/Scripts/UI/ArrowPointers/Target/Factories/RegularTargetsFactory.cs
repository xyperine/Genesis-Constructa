using ColonizationMobileGame.UI.ArrowPointers.Target.Conditions;

namespace ColonizationMobileGame.UI.ArrowPointers.Target.Factories
{
    public class RegularTargetsFactory : TargetsFactory
    {
        protected override ArrowPointerTargetInvalidationCondition[] Conditions => new ArrowPointerTargetInvalidationCondition[]
        {
            new TargetSeenCondition(),
            new TargetIgnoredCondition(),
        };
    }
}