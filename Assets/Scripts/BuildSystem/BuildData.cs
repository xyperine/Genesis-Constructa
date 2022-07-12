using System;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.UnlockingSystem;
using UnityEditor;
using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    [Serializable]
    public class BuildData : IUnlockable, ISerializationCallbackReceiver
    {
        [SerializeField] private bool locked = true;
        [SerializeField] private GameObject structurePrefab;
        [SerializeField] private ItemsRequirementsBlock price;

        [SerializeField, HideInInspector] private UnlockIdentifier identifier;
        
        private bool _defaultLockedState;
        
        public bool Locked => locked;
        public GameObject StructurePrefab => structurePrefab;
        public ItemsRequirementsBlock Price { get; private set; }
        public UnlockIdentifier Identifier
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
        
        
        public void OnBeforeSerialize()
        {
            
        }


        public void OnAfterDeserialize()
        {
            Price = price.GetDeepCopy();
        }
    }
}