using DG.Tweening;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public sealed class WorldPlacementItem : MonoBehaviour
    {
        [Header("Tweening")]
        [SerializeField] private AnimationCurve easingCurve;
        [SerializeField] private float tweenDuration = 0.1f;

        public float TransformationsDuration => tweenDuration;
        public bool Moving { get; private set; }


        public void Rotate()
        {
            transform.DOLocalRotateQuaternion(Quaternion.identity, tweenDuration).SetEase(easingCurve);
        }


        public void MoveToArea(Vector3 position)
        {
            Moving = true;
            transform.DOLocalMove(position, tweenDuration).SetEase(easingCurve).
                OnKill(() => Moving = false);
        }


        public void MoveLocally(Vector3 localPosition)
        {
            transform.localPosition = localPosition;
        }


        public void Discard()
        {
            transform.SetParent(null);
            Rotate();
        }
    }
}