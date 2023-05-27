using System;
using System.Collections.Generic;
using System.Linq;
using GenesisConstructa.BuildSystem;
using GenesisConstructa.ItemsExtraction.Upgrading;
using GenesisConstructa.ItemsRequirementsSystem;
using GenesisConstructa.SaveLoadSystem;
using GenesisConstructa.Structures;
using GenesisConstructa.Utility.Validating;
using UnityEngine;

namespace GenesisConstructa.UnlockingSystem
{
    [CreateAssetMenu(fileName = "Unlocks_Chain", menuName = "Unlocks Chain", order = 0)]
    public class UnlocksChainSO : ScriptableObject, IPurchasableChain<Unlock>, ISaveable
    {
        [SerializeField] private List<Unlock> unlocks;
        
        [SerializeField] private BuildDataSO[] builds;
        [SerializeField] private ExtractorUpgradesChainSO[] upgrades;

        private List<IUnlockable> _unlockables;

        public List<Unlock> Unlocks => unlocks;
        public ItemsRequirementsChain RequirementsChain { get; private set; }
        public Unlock Current => unlocks.FirstOrDefault(u => RequirementsChain.Current == u.Price);


        private void OnValidate()
        {
            Validator.Validate(this);

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
            ConnectUnlocksWithUnlockables();
            
            RequirementsChain = new ItemsRequirementsChain(unlocks.Select(u => u.Price).ToArray());
        }


        private void ConnectUnlocksWithUnlockables()
        {
            foreach (Unlock unlock in unlocks)
            {
                IUnlockable unlockable = _unlockables.SingleOrDefault(u => unlock.Identifier.Equals(u.Identifier));
                unlock.ConnectWith(unlockable);
            }
        }


        public object Save()
        {
            return new SaveData
            {
                ItemsRequirementsChainData = RequirementsChain.Save(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            RequirementsChain.Load(saveData.ItemsRequirementsChainData);
        }
        
        
        [Serializable]
        private struct SaveData
        {
            public object ItemsRequirementsChainData { get; set; }
        }
    }
}