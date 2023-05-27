using GenesisConstructa.TutorialSystem;
using UnityEngine;

namespace GenesisConstructa.UI.ArrowPointers.Tutorial
{
    public sealed class TutorialArrowPointerTarget : ArrowPointerTarget
    {
        public override bool RequiresPointing { get; }
        public override Transform Transform { get; }
        public TutorialStepTracker StepTracker { get; }


        public TutorialArrowPointerTarget(TutorialStepTracker stepTracker, Transform targetTransform)
        {
            StepTracker = stepTracker;
            Transform = targetTransform;

            RequiresPointing = true;
        }
    }
}