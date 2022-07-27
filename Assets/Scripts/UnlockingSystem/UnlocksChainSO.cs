using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.BuildSystem;
using ColonizationMobileGame.ItemsExtraction.Upgrading;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.Utility.Validating;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    [CreateAssetMenu(fileName = "Unlocks_Chain", menuName = "Unlocks Chain", order = 0)]
    public class UnlocksChainSO : ScriptableObject, IPurchasableChain<Unlock>
    {
        [SerializeField] private List<Unlock> unlocks;
        
        [SerializeField] private BuildDataSO[] builds;
        [SerializeField] private ExtractorUpgradesChainSO[] upgrades;

        private List<IUnlockable> _unlockables;
        private readonly Validator _validator = new Validator();

        public ItemsRequirementsChain RequirementsChain { get; private set; }
        public Unlock Current => unlocks.FirstOrDefault(u => RequirementsChain.Current == u.Price);


        private void OnValidate()
        {
            _validator.Validate(this);

            ManageUnlockablesList();
            ManageUnlocksList();
        }


        private void ManageUnlockablesList()
        {
            ExtractAllUnlockables();
            RemoveInvalidUnlockables();
        }


        private void ExtractAllUnlockables()
        {
            _unlockables = builds.SelectMany(b => b.Unlockables)
                .Concat(upgrades.SelectMany(u => u.Unlockables)).ToList();
        }


        private void RemoveInvalidUnlockables()
        {
            _unlockables.RemoveAll(u => !u.Locked);
        }


        private void ManageUnlocksList()
        {
            PopulateUnlocksList();
            RemoveInvalidUnlocks();
            SortUnlocks();
        }


        private void PopulateUnlocksList()
        {
            foreach (IUnlockable unlockable in _unlockables)
            {
                CreateUnlock(unlockable);
            }
        }


        private void CreateUnlock(IUnlockable unlockable)
        {
            if (!unlockable.Locked)
            {
                return;
            }

            if (unlocks.Any(u => unlockable.Identifier.Equals(u.Identifier)))
            {
                return;
            }

            Unlock unlock = new Unlock(unlockable.Identifier);
            unlocks.Insert(0, unlock);
        }


        private void RemoveInvalidUnlocks()
        {
            StructureIdentifier[] unlockablesCoords = _unlockables.Select(u => u.Identifier).ToArray();
            unlocks.RemoveAll(u => !unlockablesCoords.Contains(u.Identifier));
        }


        private void SortUnlocks()
        {
            StructureType[] distinctStructureTypes = unlocks.Select(u => u.Identifier.StructureType).Distinct().ToArray();

            foreach (StructureType structure in distinctStructureTypes)
            {
                int[] structureUnlocksIndices = Enumerable.Range(0, unlocks.Count)
                    .Where(j => unlocks[j].Identifier.StructureType == structure).ToArray();
                Unlock[] structureUnlocksSortedByLevel = unlocks.Where(u => u.Identifier.StructureType == structure)
                    .OrderBy(u => u.Identifier.Level).ToArray();

                for (int i = 0; i < structureUnlocksSortedByLevel.Length; i++)
                {
                    int index = structureUnlocksIndices[i];
                    unlocks[index] = structureUnlocksSortedByLevel[i];
                }
            }
        }


        private void OnEnable()
        {
#if !UNITY_EDITOR
            ManageUnlockablesList();
#endif
            WireUnlockingWithPrice();
            
            RequirementsChain = new ItemsRequirementsChain(unlocks.Select(u => u.Price).ToArray());
        }


        private void WireUnlockingWithPrice()
        {
            foreach (Unlock unlock in unlocks)
            {
                IUnlockable unlockable = _unlockables.SingleOrDefault(u => unlock.Identifier.Equals(u.Identifier));
                if (unlockable != null)
                {
                    unlock.Price.Fulfilled += unlockable.Unlock;
                }
            }
        }
    }
}