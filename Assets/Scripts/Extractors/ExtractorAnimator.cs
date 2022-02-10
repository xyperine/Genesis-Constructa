using UnityEngine;

namespace MoonPioneerClone.Extractors
{
    public class ExtractorAnimator : MonoBehaviour
    {
        private readonly int _startUpHash = Animator.StringToHash("Start_Up");

        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        public void PlayStartUpAnimation()
        {
            _animator.Play(_startUpHash);
        }
    }
}