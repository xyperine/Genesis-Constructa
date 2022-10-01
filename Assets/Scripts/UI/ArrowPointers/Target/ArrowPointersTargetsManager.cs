using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target
{
    public class ArrowPointersTargetsManager : MonoBehaviour
    {
        [SerializeField] private ArrowPointersManager pointersManager;
        
        private readonly List<ArrowPointerTarget> _targets = new List<ArrowPointerTarget>();
        
        private Camera _camera;

        private TargetsFactory _targetsFactory;
        private IArrowPointerTargetProvider[] _targetProviders;


        public void SetFactory(TargetsFactory targetsFactory)
        {
            _targetsFactory = targetsFactory;
        }
        
        
        private void Awake()
        {
            _camera = Camera.main;

            GetTargetProviders();
        }


        private void GetTargetProviders()
        {
            _targetProviders = FindObjectsOfType<MonoBehaviour>(true).OfType<IArrowPointerTargetProvider>().ToArray();

            foreach (IArrowPointerTargetProvider targetProvider in _targetProviders)
            {
                targetProvider.TargetReady += AddTarget;
            }
        }


        private void AddTarget(Transform targetTransform)
        {
            AddTargetWhenReady(targetTransform).Forget();
        }


        private async UniTaskVoid AddTargetWhenReady(Transform targetTransform)
        {
            if (_targetsFactory == null)
            {
                await UniTask.WaitUntil(() => _targetsFactory != null).Timeout(TimeSpan.FromSeconds(2f));
            }
            
            if (_targets.Exists(t => t.TransformEquals(targetTransform)))
            {
                return;
            }

            ArrowPointerTarget target = _targetsFactory!.GetTarget(targetTransform);

            _targets.Add(target);
            pointersManager.PointTo(target);

            target.Invalidated += RemoveTarget;
        }


        private void RemoveTarget()
        {
            _targets.RemoveAll(t => !t.Valid);
        }


        private void OnDisable()
        {
            foreach (IArrowPointerTargetProvider targetProvider in _targetProviders)
            {
                targetProvider.TargetReady -= AddTarget;
            }

            foreach (ArrowPointerTarget target in _targets)
            {
                target.Invalidated -= RemoveTarget;
            }
        }


        private void Update()
        {
            foreach (ArrowPointerTarget target in _targets)
            {
                target.OnScreen = IsOnScreen(target.Position);
            }
        }
        
        
        private bool IsOnScreen(Vector3 targetWorldPosition)
        {
            Vector3 viewportPosition = _camera.WorldToViewportPoint(targetWorldPosition);
            bool onScreen = viewportPosition.x is > 0f and < 1f &&
                            viewportPosition.y is > 0f and < 1f;
            return onScreen;
        }
    }
}