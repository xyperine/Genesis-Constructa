using ColonizationMobileGame.BuildSystem;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Regular
{
    public sealed class RegularArrowPointerTargetProvider : ArrowPointerTargetProvider<RegularArrowPointerTargetGroupValidator>
    {
        [SerializeField] private Builder builder;
        
        
        private void OnEnable()
        {
            builder.Unlocked += Register;
        }


        protected override ArrowPointerTarget GetTarget()
        {
            return new RegularArrowPointerTarget(transformToPointAt);
        }
        
        
        private void OnDisable()
        {
            builder.Unlocked -= Register;
        }
    }
}