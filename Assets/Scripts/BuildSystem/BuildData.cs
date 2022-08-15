using System;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.UnlockingSystem;
using ColonizationMobileGame.Utility;
using UnityEditor;
using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    [Serializable]
    public sealed class BuildData : IUnlockable, IDeepCloneable<BuildData>
    {
        [SerializeField] private bool locked = true;
        [SerializeField] private GameObject structurePrefab;
        [SerializeField] private ItemsRequirementsBlock price;
        [SerializeField] private int maxLevel;

        [SerializeField, HideInInspector] private StructureIdentifier identifier;

        private bool _defaultLockedState;
        
        public bool Locked => locked;
        public GameObject StructurePrefab => structurePrefab;
        public ItemsRequirementsBlock Price => price;
        public int MaxLevel => maxLevel;
        public StructureIdentifier Identifier
        {
            get => identifier;
            set => identifier = value;
        }

        public event Action Unlocked;


        public void Unlock()
        {
            _defaultLockedState = locked;
            
            locked = false;
            Unlocked?.Invoke();

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += ResetLockedState;
#endif
        }


#if UNITY_EDITOR
        private void ResetLockedState(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                locked = _defaultLockedState;
            }
        }
#endif


        public BuildData GetDeepCopy()
        {
            BuildData copy = new BuildData
            {
                locked = locked,
                identifier = identifier,
                price = price.GetDeepCopy(),
                structurePrefab = structurePrefab,
            };

            Unlocked += copy.Unlock;

            return copy;
        }
    }
}