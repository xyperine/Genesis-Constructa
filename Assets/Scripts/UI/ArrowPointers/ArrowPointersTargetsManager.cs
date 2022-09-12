using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public class ArrowPointersTargetsManager : MonoBehaviour
    {
        [SerializeField] private ArrowPointersManager pointersManager;
        
        private Camera _camera;

        private IArrowPointerTargetProvider[] _targetProviders;
        private readonly List<ArrowPointerTarget> _targets = new List<ArrowPointerTarget>();


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


        private void AddTarget(Transform targetTransform, ArrowPointerTargetCondition condition)
        {
            if (_targets.Exists(t => t.Position == targetTransform.position))
            {
                return;
            }

            ArrowPointerTarget target = new ArrowPointerTarget(targetTransform, condition);
            _targets.Add(target);
            pointersManager.PointTo(target);

            target.Invalidated += RemoveTarget;
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


        private void RemoveTarget()
        {
            _targets.RemoveAll(t => !t.Valid);
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