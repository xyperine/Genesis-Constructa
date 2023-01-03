using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.UpgradingSystem;
using UnityEngine;

namespace ColonizationMobileGame.SetupSystem.Upgrader
{
    [Serializable]
    public abstract class UpgraderSetupData<TUpgradeable, TUpgradesChain, TUpgradeData>: UpgraderSetupData
        where TUpgradeData : UpgradeData
        where TUpgradesChain : UpgradesChainSO<TUpgradeData>
        where TUpgradeable : IUpgradeable<TUpgradeData>
    {
        [SerializeField, HideInInspector] private TUpgradesChain chainSO;
        [SerializeField, HideInInspector] private List<TUpgradeable> upgradeables;

        private UpgradesChain<TUpgradeData> _chain;

        public UpgradesChain<TUpgradeData> Chain
        {
            get
            {
                _chain ??= chainSO.ChainCopy;
                return _chain;
            }
        }

        public List<TUpgradeable> Upgradeables => upgradeables;


        protected UpgraderSetupData(TUpgradesChain chainSO, IEnumerable<TUpgradeable> upgradeables)
        {
            this.chainSO = chainSO;
            this.upgradeables = upgradeables.ToList();
        }
    }
}