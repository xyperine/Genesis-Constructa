using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Regular
{
    public sealed class RegularArrowPointerTarget : ArrowPointerTarget
    {
        public override bool RequiresPointing { get; }
        public override Transform Transform { get; }


        public RegularArrowPointerTarget(Transform targetTransform)
        {
            Transform = targetTransform;

            RequiresPointing = true;
        }
    }
}