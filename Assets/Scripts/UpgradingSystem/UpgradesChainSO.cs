using System.Collections.Generic;
using ColonizationMobileGame.UnlockingSystem;
using ColonizationMobileGame.Utility.Validating;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.UpgradingSystem
{
    public abstract class UpgradesChainSO<TUpgradeData> : ScriptableObject, IUnlockableContainer
        where TUpgradeData : UpgradeData
    {
        [TableList(DefaultMinColumnWidth = 100)]
        [SerializeField] protected Upgrade<TUpgradeData>[] upgrades;
        [SerializeField] private StructureType structureType;

        private readonly Validator _validator = new Validator();

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
            
            _validator.Validate(this);
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