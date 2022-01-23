using DG.Tweening;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public sealed class WorldPlacementItem : MonoBehaviour
    {
        [SerializeField] private AnimationCurve easingCurve;
        [SerializeField] private float tweenDuration = 0.2f;

        public float TransformationsDuration => tweenDuration;
        public bool Moving { get; private set; }


        public void Rotate()
        {
            transform.DOLocalRotateQuaternion(Quaternion.identity, tweenDuration).SetEase(easingCurve);
        }


        public void Move(Vector3 position)
        {
            Moving = true;
            transform.DOLocalMove(position, tweenDuration).SetEase(easingCurve).
                OnKill(() => Moving = false);
        }


        public void Discard()
        {
            transform.SetParent(null);
            Rotate();
        }
    }
}