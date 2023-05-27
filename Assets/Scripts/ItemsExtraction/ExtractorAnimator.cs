using GenesisConstructa.ItemsExtraction.ConditionsLogic;
using UnityEngine;

namespace GenesisConstructa.ItemsExtraction
{
    public class ExtractorAnimator : MonoBehaviour
    {
        [SerializeField] private ExtractorConditionsUnit conditionsUnit;
        [SerializeField] private Animator animator;

        private readonly int _idleHash = Animator.StringToHash("Idle");
        private readonly int _startUpHash = Animator.StringToHash("Start_Up");
        private readonly int _productionHash = Animator.StringToHash("Production");
        private readonly int _shutDownHash = Animator.StringToHash("Shut_Down");
        
        
        private void OnEnable()
        {
            if (conditionsUnit.WorkConditionsMet)
            {
                PlayStartUpAnimation();
            }

            conditionsUnit.ConditionsChanged += OnConditionsChanged;
        }


        private void OnConditionsChanged()
        {
            if (conditionsUnit.WorkConditionsMet)
            {
                PlayStartUpAnimation();
                return;
            }
            
            PlayShutDownAnimation();
        }


        private void OnDisable()
        {
            conditionsUnit.ConditionsChanged -= OnConditionsChanged;
        }


        public void PlayIdleAnimation()
        {
            animator.Play(_idleHash);
        }


        private void PlayStartUpAnimation()
        {
            animator.Play(_startUpHash);
        }


        public void PlayProductionAnimation()
        {
            animator.Play(_productionHash);
        }


        private void PlayShutDownAnimation()
        {
            animator.Play(_shutDownHash);
        }
    }
}