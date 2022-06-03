﻿using DG.Tweening;
using MoonPioneerClone.ObjectPooling;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacement.Core
{
    [RequireComponent(typeof(IPoolable))]
    public sealed class PlacementItem : MonoBehaviour
    {
        [SerializeField] private AnimationCurve easingCurve;
        [SerializeField] private float tweenDuration = 0.1f;

        private Tween _movingTween;
        private IPoolable _poolable;

        public bool Moving => _movingTween is {active: true,};


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
            
            Destroy(gameObject);
        }
    }
}