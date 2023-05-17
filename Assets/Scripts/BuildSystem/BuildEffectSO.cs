using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    [CreateAssetMenu(fileName = "Build_Effect", menuName = "Build Effect", order = 0)]
    public class BuildEffectSO : ScriptableObject
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public AnimationCurve EasingCurve { get; private set; }
    }
}