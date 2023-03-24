using ColonizationMobileGame.Hibernation.Area;

namespace ColonizationMobileGame.UI.ArrowPointers.TargetGroupValidators
{
    public class HibernationAreaArrowPointerTargetGroupValidator : ArrowPointerTargetGroupValidator
    {
        private HibernationAreaCollider _hibernationAreaCollider;


        protected override bool TargetIsValid(ArrowPointerTarget target)
        {
            if (!_hibernationAreaCollider)
            {
                _hibernationAreaCollider = target.Transform.GetComponent<HibernationAreaCollider>();
            }

            return _hibernationAreaCollider.RequiresPointing;
        }
    }
}