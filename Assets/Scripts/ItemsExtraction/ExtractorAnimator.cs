using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public sealed class ExtractorAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private readonly int _idleHash = Animator.StringToHash("Idle");
        private readonly int _startUpHash = Animator.StringToHash("Start_Up");
        private readonly int _productionHash = Animator.StringToHash("Production");
        private readonly int _shutDownHash = Animator.StringToHash("Shut_Down");


        public void PlayIdleAnimation()
        {
            animator.Play(_idleHash);
        }


        public void PlayStartUpAnimation()
        {
            animator.Play(_startUpHash);
        }


        public void PlayProductionAnimation()
        {
            animator.Play(_productionHash);
        }


        public void PlayShutDownAnimation()
        {
            animator.Play(_shutDownHash);
        }
    }
}