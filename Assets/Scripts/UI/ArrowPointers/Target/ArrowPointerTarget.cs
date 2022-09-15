using System;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target
{
    public class ArrowPointerTarget
    {
        private readonly Transform _targetTransform;
        private readonly ArrowPointerTargetInvalidationCondition[] _conditions;

        public Vector3 Position => _targetTransform.position;

        public bool Valid => !_conditions.All(c => c.Met);
        public bool OnScreen { get; set; }
        
        public event Action Invalidated;


        public ArrowPointerTarget(Transform targetTransform, ArrowPointerTargetInvalidationCondition[] conditions)
        {
            _targetTransform = targetTransform;
            _conditions = conditions;

            foreach (ArrowPointerTargetInvalidationCondition condition in _conditions)
            {
                condition.StartTracking(this);
                condition.Satisfied += InvokeInvalidated;
            }
        }


        private void InvokeInvalidated()
        {
            Invalidated?.Invoke();

            foreach (ArrowPointerTargetInvalidationCondition condition in _conditions)
            {
                condition.Satisfied -= InvokeInvalidated;
            }
        }
    }
}