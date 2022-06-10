using System;
using ColonizationMobileGame.UpgradingSystem;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading
{
    [Serializable]
    public class StackZoneUpgradeData : UpgradeData
    {
        [SerializeField] private int capacity;

        public int Capacity => capacity;
    }
}