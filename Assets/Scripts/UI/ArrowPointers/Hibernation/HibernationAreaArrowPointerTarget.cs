using ColonizationMobileGame.Hibernation.Area;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Hibernation
{
    public sealed class HibernationAreaArrowPointerTarget : ArrowPointerTarget
    {
        private readonly HibernationAreaCollider _areaCollider;
        
        public override bool RequiresPointing => !_areaCollider.ObjectInside;
        public override Transform Transform { get; }


        public HibernationAreaArrowPointerTarget(HibernationAreaCollider areaCollider, Transform targetTransform)
        {
            _areaCollider = areaCollider;
            Transform = targetTransform;
        }
    }
}