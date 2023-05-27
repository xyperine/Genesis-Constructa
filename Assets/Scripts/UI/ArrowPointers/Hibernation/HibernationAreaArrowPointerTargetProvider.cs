using GenesisConstructa.Hibernation.Area;
using UnityEngine;

namespace GenesisConstructa.UI.ArrowPointers.Hibernation
{
    public sealed class HibernationAreaArrowPointerTargetProvider : ArrowPointerTargetProvider<HibernationAreaArrowPointerTargetGroupValidator>
    {
        [SerializeField] private HibernationAreaCollider areaCollider;


        private void Start()
        {
            Register();
        }


        protected override ArrowPointerTarget GetTarget()
        {
            return new HibernationAreaArrowPointerTarget(areaCollider, transformToPointAt);
        }
    }
}