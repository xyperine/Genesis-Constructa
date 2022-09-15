using System.Collections;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target.Conditions
{
    public class TargetIgnoredCondition : ArrowPointerTargetInvalidationCondition
    {
        private const float REQUIRED_OFF_SCREEN_PRESENCE_TIME_IN_SECONDS = 5f;

        private ArrowPointerTarget _target;

        
        public override void StartTracking(ArrowPointerTarget target)
        {
            _target = target;
            Object.FindObjectOfType<ArrowPointersTargetsManager>().StartCoroutine(CheckVisibilityCoroutine());
        }
        
        
        private IEnumerator CheckVisibilityCoroutine()
        {
            float offScreenDuration = 0f;

            while (offScreenDuration < REQUIRED_OFF_SCREEN_PRESENCE_TIME_IN_SECONDS)
            {
                if (!_target.OnScreen)
                {
                    offScreenDuration += Time.deltaTime;
                }

                yield return null;
            }

            Debug.Log($"Was off screen for {offScreenDuration}!");
            
            InvokeSatisfied();
        }
    }
}