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
        [SerializeField, HideInInspector] private TUpgradesChain chain;
        [SerializeField, HideInInspector] private List<TUpgradeable> upgradeables;
        [SerializeField, HideInInspector] private float colliderRadius;
        
        public TUpgradesChain Chain => chain;
        public List<TUpgradeable> Upgradeables => upgradeables;

        public float ColliderRadius => colliderRadius;


        protected UpgraderSetupData(TUpgradesChain chain, IEnumerable<TUpgradeable> upgradeables, float colliderRadius)
        {
            this.chain = chain;
            this.upgradeables = upgradeables.ToList();
            this.colliderRadius = colliderRadius;
        }
    }
}