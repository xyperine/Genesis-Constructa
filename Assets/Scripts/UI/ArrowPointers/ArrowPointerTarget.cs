using System;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public class ArrowPointerTarget
    {
        private readonly Transform _targetTransform;
        private readonly ArrowPointerTargetCondition _condition;

        public Vector3 Position => _targetTransform.position;

        public bool Valid => !_condition.Met;
        public bool OnScreen { get; set; }
        
        public event Action Invalidated;


        public ArrowPointerTarget(Transform targetTransform, ArrowPointerTargetCondition condition)
        {
            _targetTransform = targetTransform;
            _condition = condition;

            _condition.Satisfied += InvokeInvalidated;
        }


        private void InvokeInvalidated()
        {
            Invalidated?.Invoke();
        }
    }
}