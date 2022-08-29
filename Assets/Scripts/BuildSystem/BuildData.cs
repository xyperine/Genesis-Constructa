using System;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.Structures;
using ColonizationMobileGame.UnlockingSystem;
using ColonizationMobileGame.Utility;
using ColonizationMobileGame.Utility.Validating;
using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    [Serializable]
    public sealed class BuildData : IUnlockable, IDeepCloneable<BuildData>, IValidatable
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
    }
}