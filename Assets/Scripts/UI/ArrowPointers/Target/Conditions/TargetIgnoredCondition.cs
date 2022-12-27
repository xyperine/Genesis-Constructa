using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target.Conditions
{
    public class TargetIgnoredCondition : ArrowPointerTargetInvalidationCondition
    {
        private const float REQUIRED_OFF_SCREEN_PRESENCE_TIME_IN_SECONDS = 15f;

        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private ArrowPointerTarget _target;

        
        public override void StartTracking(ArrowPointerTarget target)
        {
            _target = target;
            
            CheckVisibility().Forget();
        }


        private async UniTaskVoid CheckVisibility()
        {
            float offScreenDuration = 0f;

            while (offScreenDuration < REQUIRED_OFF_SCREEN_PRESENCE_TIME_IN_SECONDS)
            {
                if (!_target.OnScreen)
                {
                    offScreenDuration += Time.deltaTime;
                }

                await UniTask.Yield(cancellationToken: _tokenSource.Token);
            }

            Debug.Log($"Was off screen for {offScreenDuration}!");
            
            InvokeSatisfied();
        }


        public override void Dispose()
        {
            _tokenSource.Cancel();
        }
    }
}