using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target.Conditions
{
    public class TargetSeenCondition : ArrowPointerTargetInvalidationCondition
    {
        private const float REQUIRED_ON_SCREEN_PRESENCE_TIME_IN_SECONDS = 2f;
        
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private ArrowPointerTarget _target;


        public override void StartTracking(ArrowPointerTarget target)
        {
            _target = target;
            
            CheckVisibility().Forget();
        }


        private async UniTaskVoid CheckVisibility()
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
                    //Debug.Log($"Was on screen for {visibilityDuration}");
                    visibilityDuration = 0f;
                }

                await UniTask.Yield(cancellationToken: _tokenSource.Token);
            }

            //Debug.Log("Was on screen all the time!");
            
            InvokeSatisfied();
        }


        public override void Dispose()
        {
            _tokenSource.Cancel();
        }
    }
}