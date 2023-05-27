using System.Collections.Generic;
using GenesisConstructa.Structures;
using GenesisConstructa.UnlockingSystem;
using GenesisConstructa.Utility.Validating;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.UpgradingSystem
{
    public abstract class UpgradesChainSO<TUpgradeData> : ScriptableObject, IUnlockableContainer
        where TUpgradeData : UpgradeData
    {
        [TableList(DefaultMinColumnWidth = 100)]
        [SerializeField] protected Upgrade<TUpgradeData>[] upgrades;
        [SerializeField] private StructureType structureType;

        public UpgradesChain<TUpgradeData> ChainCopy => new UpgradesChain<TUpgradeData>(upgrades);
        public IEnumerable<IUnlockable> Unlockables => upgrades;


#if !UNITY_EDITOR
        private void OnEnable()
        {
            SetIdentifiersForUpgrades();
        }
#endif


        private void OnValidate()
        {
            SetIdentifiersForUpgrades();
            
            Validator.Validate(this);
        }
        

        private void SetIdentifiersForUpgrades()
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].Identifier = new StructureIdentifier(structureType, i + 1);
            }
        }
    }
}