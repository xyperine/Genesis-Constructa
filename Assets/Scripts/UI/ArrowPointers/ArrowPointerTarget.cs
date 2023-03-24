using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public class ArrowPointerTarget
    {
        private readonly IArrowPointerTarget _target;
        
        public bool RequiresPointing => _target.RequiresPointing;
        public Transform Transform { get; }


        public ArrowPointerTarget(IArrowPointerTarget target, Transform transform)
        {
            _target = target;
            Transform = transform;
        }
    }
}