using System;
using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.UpgradingSystem;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.Upgrader
{
    [Serializable]
    public abstract class UpgraderSetupData<TUpgradeable, TUpgradesChain, TUpgradeData>: UpgraderSetupData
        where TUpgradeData : UpgradeData
        where TUpgradesChain : UpgradesChainSO<TUpgradeData>
        where TUpgradeable : IUpgradeable<TUpgradeData>
    {
        [SerializeField, HideInInspector] private TUpgradesChain chainSO;
        [SerializeField, HideInInspector] private List<TUpgradeable> upgradeables;
        [SerializeField, HideInInspector] private float colliderRadius;

        private UpgradesChain<TUpgradeData> _chain;

        public UpgradesChain<TUpgradeData> Chain
        {
            get
            {
                _chain ??= chainSO.UniqueChain;
                return _chain;
            }
        }

        public List<TUpgradeable> Upgradeables => upgradeables;

        public float ColliderRadius => colliderRadius;


        protected UpgraderSetupData(TUpgradesChain chainSO, IEnumerable<TUpgradeable> upgradeables, float colliderRadius)
        {
            this.chainSO = chainSO;
            this.upgradeables = upgradeables.ToList();
            this.colliderRadius = colliderRadius;
        }
    }
}