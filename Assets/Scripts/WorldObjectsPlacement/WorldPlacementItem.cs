using DG.Tweening;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public sealed class WorldPlacementItem : MonoBehaviour
    {
        [SerializeField] private AnimationCurve easingCurve;
        [SerializeField] private float tweenDuration = 0.2f;

        public float TransformationsDuration => tweenDuration;


        public void Rotate()
        {
            transform.DOLocalRotateQuaternion(Quaternion.identity, tweenDuration).SetEase(easingCurve);
        }


        public void Move(Vector3 position)
        {
            transform.DOLocalMove(position, tweenDuration).SetEase(easingCurve);
        }


        public void Discard()
        {
            transform.SetParent(null);
            Rotate();
        }
    }
}