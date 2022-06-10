using ColonizationMobileGame.Utility.Validating;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.UpgradingSystem
{
    public abstract class UpgradesChainSO<TUpgradeData> : ScriptableObject
        where TUpgradeData : UpgradeData
    {
        [TableList(DefaultMinColumnWidth = 100)]
        [SerializeField] protected Upgrade<TUpgradeData>[] upgrades;

        private readonly Validator _validator = new Validator();
        
        public UpgradesChain<TUpgradeData> Chain { get; private set; }
        public UpgradesChain<TUpgradeData> UniqueChain => new UpgradesChain<TUpgradeData>(upgrades);
        

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
            Chain = new UpgradesChain<TUpgradeData>(upgrades);
            
            _validator.Validate(this);
        }
    }
}