using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.ItemsRequirementsSystem;
using MoonPioneerClone.Utility.Validating;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.UpgradingSystem
{
    public abstract class UpgradesChainSO<TUpgradeData> : ScriptableObject
        where TUpgradeData : UpgradeData
    {
        [TableList(DefaultMinColumnWidth = 100)]
        [SerializeField] protected List<Upgrade<TUpgradeData>> upgrades;

        private readonly Validator _validator = new Validator();
        
        public UpgradesStatusTracker<TUpgradeData> Upgrades { get; private set; }
        public ItemsRequirementsChain RequirementsChain { get; private set; }


#if !UNITY_EDITOR
        private void OnEnable()
        {
            Setup();
        }
#endif
        
        
        private void OnValidate()
        {
            Setup();
        }


        private void Setup()
        {
            Upgrades = new UpgradesStatusTracker<TUpgradeData>(upgrades);
            RequirementsChain = new ItemsRequirementsChain(upgrades.Select(u => u.Price).ToArray());
            
            _validator.Validate(this);
        }
    }
}