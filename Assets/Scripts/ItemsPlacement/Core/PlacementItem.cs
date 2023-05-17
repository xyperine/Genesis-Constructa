using ColonizationMobileGame.ObjectPooling;
using DG.Tweening;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacement.Core
{
    [RequireComponent(typeof(IPoolable))]
    public sealed class PlacementItem : MonoBehaviour
    {
        [SerializeField] private AnimationCurve easingCurve;
        [SerializeField] private float tweenDuration = 0.1f;
        [SerializeField] private ItemAlignment alignment;

        private Tween _movingTween;
        private IPoolable _poolable;

        public bool Moving => _movingTween is {active: true};

        public ItemAlignment Alignment => alignment;


        private void Awake()
        {
            _poolable = GetComponent<IPoolable>();
        }


        public void Rotate(Quaternion rotation)
        {
            transform.DOLocalRotateQuaternion(rotation, tweenDuration).SetEase(easingCurve);
        }
        

        public void MoveToArea(Vector3 position)
        {
            _movingTween = transform.DOLocalMove(position, tweenDuration).SetEase(easingCurve);
        }


        public void MoveLocally(Vector3 localPosition)
        {
            transform.localPosition = localPosition;
        }


        public void Shrink()
        {
            transform.DOScale(0.3f, tweenDuration).SetEase(easingCurve);
        }


        public void Discard()
        {
            transform.SetParent(null);
        }


        public void Kill()
        {
            if (_movingTween != null)
            {
                _movingTween.OnKill(() => _poolable.Return());
                return;
            }
            
            _poolable.Return();
        }
    }
}