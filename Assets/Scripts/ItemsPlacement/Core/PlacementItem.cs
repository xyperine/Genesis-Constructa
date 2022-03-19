using DG.Tweening;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacement.Core
{
    public sealed class PlacementItem : MonoBehaviour
    {
        [SerializeField] private AnimationCurve easingCurve;
        [SerializeField] private float tweenDuration = 0.1f;

        private Tween _movingTween;
        
        public bool Moving => _movingTween is {active: true,};


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


        public void Discard()
        {
            transform.SetParent(null);
            //Rotate(Quaternion.identity);
        }


        public void Kill()
        {
            if (_movingTween != null)
            {
                _movingTween.OnKill(() => Destroy(gameObject));
                return;
            }
            
            Destroy(gameObject);
        }
    }
}