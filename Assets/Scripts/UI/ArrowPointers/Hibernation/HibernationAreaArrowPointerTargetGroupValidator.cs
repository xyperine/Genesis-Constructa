namespace GenesisConstructa.UI.ArrowPointers.Hibernation
{
    public sealed class HibernationAreaArrowPointerTargetGroupValidator : ArrowPointerTargetGroupValidator
    {
        protected override bool TargetIsValid(ArrowPointerTarget target)
        {
            return target.RequiresPointing;
        }
    }
}