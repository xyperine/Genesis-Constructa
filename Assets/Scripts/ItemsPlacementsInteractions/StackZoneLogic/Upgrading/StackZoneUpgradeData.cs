using System;
using MoonPioneerClone.UpgradingSystem;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading
{
    [Serializable]
    public class StackZoneUpgradeData : UpgradeData
    {
        [SerializeField] private int capacity;

        public int Capacity => capacity;
    }
}