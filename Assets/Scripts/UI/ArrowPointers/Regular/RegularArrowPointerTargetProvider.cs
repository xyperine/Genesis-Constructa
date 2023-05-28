using GenesisConstructa.BuildSystem;
using UnityEngine;

namespace GenesisConstructa.UI.ArrowPointers.Regular
{
    public sealed class RegularArrowPointerTargetProvider : ArrowPointerTargetProvider<RegularArrowPointerTargetGroupValidator>
    {
        [SerializeField] private Builder builder;
        
        
        private void OnEnable()
        {
            builder.Unlocked += Register;
            builder.Built += Unregister;
        }


        protected override ArrowPointerTarget GetTarget()
        {
            return new RegularArrowPointerTarget(transformToPointAt);
        }
        
        
        private void OnDisable()
        {
            builder.Unlocked -= Register;
            builder.Built -= Unregister;
        }
    }
}