using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.Utility.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target
{
    public class ArrowPointersTargetsManager : MonoBehaviour, ISceneSaveable
    {
        [SerializeField] private ArrowPointersManager pointersManager;
        
        [SerializeField, HideInInspector] private PermanentGuid guid;

        private readonly List<ArrowPointerTarget> _targets = new List<ArrowPointerTarget>();
        
        private Camera _camera;

        private TargetsFactory _targetsFactory;
        private IArrowPointerTargetProvider[] _targetProviders;

        private List<string> _guids = new List<string>();
        
        public PermanentGuid Guid => guid;
        public int LoadingOrder => -2;


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
            if (IsAlreadyShown(targetTransform))
            {
                return;
            }
            
            AddTargetWhenReady(targetTransform).Forget();
        }


        private bool IsAlreadyShown(Transform targetTransform)
        {
            IPermanentGuidIdentifiable identifiable = targetTransform.GetComponent<IPermanentGuidIdentifiable>();

            if (identifiable == null)
            {
                throw new InvalidDataException(
                    $"{targetTransform.gameObject.GetFullName()} has no identifiable component!");
            }

            return _guids.Contains(identifiable.Guid.Value);
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

            IPermanentGuidIdentifiable identifiable = targetTransform.GetComponent<IPermanentGuidIdentifiable>();
            _guids.Add(identifiable?.Guid.Value);
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


        public object Save()
        {
            return new SaveData
            {
                Guids = _guids.ToArray(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            _guids = saveData.Guids.ToList();
        }
        
        
        private struct SaveData
        {
            public string[] Guids { get; set; }
        }
    }
}