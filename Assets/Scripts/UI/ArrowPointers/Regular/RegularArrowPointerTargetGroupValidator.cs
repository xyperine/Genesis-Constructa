using System.Collections.Generic;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Regular
{
    public sealed class RegularArrowPointerTargetGroupValidator : ArrowPointerTargetGroupValidator
    {
        [SerializeField] private new Camera camera;

        [SerializeField] private float onScreenLifetime = 3.5f;
        [SerializeField] private float offScreenLifetime = 15f;

        private readonly Dictionary<ArrowPointerTarget, RegularTargetValidationData> _targetsValidationData =
            new Dictionary<ArrowPointerTarget, RegularTargetValidationData>();


        protected override bool TargetIsValid(ArrowPointerTarget target)
        {
            if (!_targetsValidationData.ContainsKey(target))
            {
                _targetsValidationData.Add(target, new RegularTargetValidationData());
            }
            
            RegularTargetValidationData targetData = _targetsValidationData[target];

            if (!targetData.Valid)
            {
                return false;
            }
            
            if (IsOnScreen(target))
            {
                targetData.OnScreenDuration += Time.deltaTime;
                targetData.OffScreenDuration = 0f;
            }
            else
            {
                targetData.OffScreenDuration += Time.deltaTime;
                targetData.OnScreenDuration = 0f;
            }

            bool isValid = targetData.OnScreenDuration < onScreenLifetime &&
                           targetData.OffScreenDuration < offScreenLifetime;

            if (!isValid)
            {
                targetData.Valid = false;
            }
            
            return target.RequiresPointing && isValid;
        }


        private bool IsOnScreen(ArrowPointerTarget target)
        {
            Vector3 viewportPosition = camera.WorldToViewportPoint(target.Transform.position);
            bool onScreen = viewportPosition.x is > 0f and < 1f &&
                            viewportPosition.y is > 0f and < 1f;
            return onScreen;
        }
    }
}