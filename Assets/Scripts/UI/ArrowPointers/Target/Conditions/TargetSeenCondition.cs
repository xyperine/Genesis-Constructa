using System.Collections;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target.Conditions
{
    public class TargetSeenCondition : ArrowPointerTargetInvalidationCondition
    {
        private const float REQUIRED_ON_SCREEN_PRESENCE_TIME_IN_SECONDS = 1f;
        
        private ArrowPointerTarget _target;


        public override void StartTracking(ArrowPointerTarget target)
        {
            _target = target;
            Object.FindObjectOfType<ArrowPointersTargetsManager>().StartCoroutine(CheckVisibilityCoroutine());
        }


        private IEnumerator CheckVisibilityCoroutine()
        {
            float visibilityDuration = 0f;

            while (visibilityDuration < REQUIRED_ON_SCREEN_PRESENCE_TIME_IN_SECONDS)
            {
                if (_target.OnScreen)
                {
                    visibilityDuration += Time.deltaTime;
                }
                else
                {
                    Debug.Log($"Was on screen for {visibilityDuration}");
                    visibilityDuration = 0f;
                }
                
                yield return null;
            }

            Debug.Log("Was on screen all the time!");
            
            InvokeSatisfied();
        }
    }
}