using System;
using GenesisConstructa.UpgradingSystem;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic.Upgrading
{
    [Serializable]
    public class StackZoneUpgradeData : UpgradeData
    {
        [SerializeField] private int capacity;

        public int Capacity => capacity;
    }
}