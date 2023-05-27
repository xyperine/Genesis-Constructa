using System;
using GenesisConstructa.ItemsRequirementsSystem;
using GenesisConstructa.Structures;
using GenesisConstructa.UnlockingSystem;
using GenesisConstructa.Utility;
using GenesisConstructa.Utility.Validating;
using UnityEngine;

namespace GenesisConstructa.BuildSystem
{
    [Serializable]
    public sealed class BuildData : IUnlockable, IDeepCloneable<BuildData>, IValidatable, ISerializationCallbackReceiver
    {
        [SerializeField] private bool locked = true;
        [SerializeField] private GameObject structurePrefab;
        [SerializeField] private ItemRequirementsBlock price;
        [SerializeField] private int maxLevel;

        [SerializeField, HideInInspector] private StructureIdentifier identifier;
        
        public bool Locked { get; private set; }

        public GameObject StructurePrefab => structurePrefab;
        public ItemRequirementsBlock Price => price;
        public int MaxLevel => maxLevel;
        public StructureIdentifier Identifier
        {
            get => identifier;
            set => identifier = value;
        }

        public event Action Unlocked;

        
        public void OnValidate()
        {
            Locked = locked;
        }


        public void Unlock()
        {
            Locked = false;
            Unlocked?.Invoke();
            
            Debug.Log($"Build data for {identifier.StructureType} was unlocked!");
        }


        public BuildData GetDeepCopy()
        {
            BuildData copy = new BuildData
            {
                Locked = Locked,
                identifier = identifier,
                price = price.GetDeepCopy(),
                structurePrefab = structurePrefab,
            };

            Unlocked += copy.Unlock;

            return copy;
        }


        public void OnBeforeSerialize()
        {
            
        }


        public void OnAfterDeserialize()
        {
            Locked = locked;
        }
    }
}