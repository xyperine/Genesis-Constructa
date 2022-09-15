using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target
{
    public abstract class TargetsFactory
    {
        protected abstract ArrowPointerTargetInvalidationCondition[] Conditions { get; }


        public ArrowPointerTarget GetTarget(Transform transform)
        {
            return new ArrowPointerTarget(transform, Conditions);
        }
    }
}